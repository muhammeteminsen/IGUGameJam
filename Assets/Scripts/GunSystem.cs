using System.Globalization;
using TMPro;
using UnityEngine;

// ReSharper disable Unity.PerformanceCriticalCodeInvocation

public class GunSystem : MonoBehaviour
{
    // [SerializeField] private float knockBackForce = 500f;
    [Range(0f, 5f)] [SerializeField] private float hitTime = 1f;
    [SerializeField] private int bullet = 30;
    [SerializeField] private int magazine = 100;
    [SerializeField] private TextMeshProUGUI bulletText;
    [SerializeField] private TextMeshProUGUI magazineText;

    private float _hitCounter;
    private int _defaultBullet;


    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Start()
    {
        _defaultBullet = bullet;
    }

    void Update()
    {
        ShootingRaycast();
        ReloadSystem();
    }

    private void ShootingRaycast()
    {
        RaycastHit hit;
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if ((Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) && Time.time > _hitCounter && bullet>0)
            {
                bullet--;
                _hitCounter = Time.time + hitTime;
                Debug.Log("Shooting");
                // KnockBack(hit);
            }

            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red);
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * 1000, Color.green);
        }
    }

    private void ReloadSystem()
    {
        if (Input.GetKeyDown(KeyCode.R) && magazine > 0)
        {
            int spentBullet = Mathf.Abs(bullet - _defaultBullet);
            if (bullet == 0)
            {
                magazine -= _defaultBullet;
                bullet = _defaultBullet;
                if (magazine < spentBullet)
                {
                    bullet += magazine;
                    magazine = 0;
                }
            }

            else if (magazine < spentBullet)
            {
                bullet += magazine;
                magazine = 0;
            }
            else if (bullet < _defaultBullet)
            {
                magazine -= spentBullet;
                bullet = _defaultBullet;
            }
        }

        bulletText.text = bullet.ToString(CultureInfo.InvariantCulture);
        magazineText.text = magazine.ToString(CultureInfo.InvariantCulture);
    }

    // private void KnockBack(RaycastHit hit)
    // {
    //     hit.transform.GetComponent<Rigidbody>().AddForce(_camera.transform.forward * knockBackForce);
    // }
}