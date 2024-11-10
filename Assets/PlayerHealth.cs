using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public float health = 100f;
    [SerializeField] private Volume defaultVolume;
    [SerializeField] private float vignetteIntensity = 0.3f;

    private Vignette _vignette;
    private float _defaultVignetteIntensity;
    private bool _isDamaged;
    private float _onDestroyHealth = 5f;
    private int _onDestroyMagazine = 20;
    private ReloadSystem _reloadSystem;

    private void Awake()
    {
        _reloadSystem = FindObjectOfType<ReloadSystem>();
    }

    private void Start()
    {
        if (defaultVolume.profile.TryGet(out _vignette))
        {
            _defaultVignetteIntensity = _vignette.intensity.value;
        }
    }

    public void TakenDamage(float damage)
    {
        health -= damage;
        Debug.Log(health);
        if (_vignette != null)
        {
            _vignette.intensity.value = vignetteIntensity;
            _isDamaged = true;
        }
    }

    public void OnDestroyVariables()
    {
        health -= _onDestroyHealth;
        _reloadSystem.magazine += _onDestroyMagazine;
    }

    private void Update()
    {
        if (_isDamaged && _vignette != null)
        {
            _vignette.intensity.value =
                Mathf.Lerp(_vignette.intensity.value, _defaultVignetteIntensity, Time.deltaTime * 2);

            if (Mathf.Abs(_vignette.intensity.value - _defaultVignetteIntensity) < 0.01f)
            {
                _vignette.intensity.value = _defaultVignetteIntensity;
                _isDamaged = false;
            }
        }
    }
}