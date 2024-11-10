using System.Globalization;
using TMPro;
using UnityEngine;

// ReSharper disable Unity.PerformanceCriticalCodeInvocation

public class GunSystem : MonoBehaviour
{
    // [SerializeField] private float knockBackForce = 500f;
    [Range(0f, 5f)] [SerializeField] private float hitTime = 1f;

    [Header("Gun Recoil Variables")] [SerializeField]
    private float recoilStrength = 0.5f;

    [SerializeField] private float recoilSmoothness = 0.5f;
    [SerializeField] private float recoilReturnSpeed = 4f;
    [SerializeField] private Transform weapon;

    [Header("Bullet Recoil Variables")] [SerializeField]
    private float recoilMinX = -1;

    [SerializeField] private float recoilMaxX = 1;
    [SerializeField] private float recoilMinY = -1;
    [SerializeField] private float recoilMaxY = 1;

    [Header("Particle Effects")] [SerializeField]
    private ParticleSystem muzzleFlash;

    [SerializeField] private ParticleSystem bloodEffect;
    [SerializeField] private ParticleSystem metalEffect;

    [Header("PopUp Variables")] [SerializeField]
    private TextMeshPro damagePopUpText;

    [SerializeField] private float popUpOffsetY = 0.7f;
    [SerializeField] private float popUpOffsetZ = 0.7f;
    [SerializeField] private float popUpRandomizeY = 0.5f;
    [SerializeField] private float popUpRandomizeZ = 0.3f;

    [Header("Layer Mask Settings")] [SerializeField]
    private LayerMask targetLayerMask;


    private Vector3 _damagePopUpOffset;
    private Vector3 _recoilRot;
    private Vector3 _initialPosition;
    private float _hitCounter;
    private Camera _camera;
    private ReloadSystem _reloadSystem;
    private PlayerMovement _playerMovement;


    private void Awake()
    {
        _initialPosition = weapon.localPosition;
        _camera = Camera.main;
        _reloadSystem = weapon.GetComponent<ReloadSystem>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    void LateUpdate()
    {
        ShootingRaycast();
        _recoilRot = _camera.transform.localRotation.eulerAngles;
        if (_recoilRot.x != 0 || _recoilRot.y != 0)
        {
            _camera.transform.localRotation = Quaternion.Slerp(_camera.transform.localRotation,
                Quaternion.Euler(0, 0, 0), Time.deltaTime);
        }
    }

    private void ShootingRaycast()
    {
        RaycastHit hit;
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        int layerIndex = LayerMask.NameToLayer("AnomalyFast") | LayerMask.NameToLayer("AnomalySlow");
        int layerMask = ~(1 << layerIndex);
        if (Physics.Raycast(ray, out hit, 1000f, layerMask))
        {
            if ((Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) && Time.time > _hitCounter &&
                _reloadSystem.bullet > 0 && !_reloadSystem.isReloading && !_playerMovement.isSpring)
            {
                IDamageable damageable = hit.transform.GetComponent<IDamageable>();
                Shooting();
                if (damageable != null)
                {
                    hit.transform.GetComponent<Animator>().SetTrigger("Hit");
                    float damage = damageable.DamageAmount;
                    damageable.TakeDamage(damage);
                    damagePopUpText.text = damage.ToString(CultureInfo.InvariantCulture);
                    Instantiate(damagePopUpText, hit.point + _damagePopUpOffset, Quaternion.LookRotation(-hit.normal));
                    ParticleSystem bloodEffectInstance =
                        Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    bloodEffectInstance.transform.parent = hit.transform;
                    float popupRandomZ = Random.Range(-popUpRandomizeZ, popUpRandomizeZ);
                    float popupRandomY = Random.Range(-popUpRandomizeY, popUpRandomizeY);
                    _damagePopUpOffset = new Vector3(0, popupRandomY + popUpOffsetY, popupRandomZ - popUpOffsetZ);
                }
                else
                {
                    Instantiate(metalEffect, hit.point, Quaternion.LookRotation(hit.normal));
                }
            }

            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red);
        }

        else
        {
            if ((Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) && Time.time > _hitCounter &&
                _reloadSystem.bullet > 0)
            {
                Shooting();
            }

            Debug.DrawRay(ray.origin, ray.direction * 1000, Color.green);
        }
    }

    private void Shooting()
    {
        muzzleFlash.Play();
        _reloadSystem.bullet--;
        _hitCounter = Time.time + hitTime;
        Debug.Log("Shooting");
        Recoil();
    }


    private void Recoil()
    {
        //Geri Tepme
        Vector3 recoilPosition = new Vector3(weapon.transform.localPosition.x, weapon.transform.localPosition.y,
            -weapon.transform.localPosition.z * recoilStrength);
        weapon.transform.localPosition = Vector3.Lerp(weapon.transform.localPosition, _initialPosition + recoilPosition,
            Time.deltaTime * recoilSmoothness);
        weapon.transform.localPosition = Vector3.Lerp(weapon.transform.localPosition, _initialPosition,
            Time.deltaTime * recoilReturnSpeed);

        //Sekme
        float randomX = Random.Range(recoilMinX, recoilMaxX);
        float randomY = Random.Range(recoilMinY, recoilMaxY);
        _camera.transform.localRotation =
            Quaternion.Euler(_recoilRot.x - randomY, _recoilRot.y + randomX, _recoilRot.z);
    }
}