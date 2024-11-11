using System;
using System.Globalization;
using TMPro;
using UnityEngine;

public class ReloadSystem : MonoBehaviour
{
    public bool isReloading;
    [SerializeField] private Sounds _sounds;
    
    [Header("Reload Variables")]
    [SerializeField] public int bullet = 30;
    [SerializeField] public int magazine = 100;
    [SerializeField] private TextMeshPro bulletText;
    [SerializeField] private TextMeshPro magazineText;
    
    private Animator _animator;
    private int _defaultBullet;
    private void Start()
    {
        _defaultBullet = bullet;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && magazine>0 && bullet<30)
        {
            isReloading = true;
            _animator.SetTrigger("Reload");
         _sounds.PlaySound("Reload");
        }
        bulletText.text = bullet.ToString(CultureInfo.InvariantCulture);
        magazineText.text = magazine.ToString(CultureInfo.InvariantCulture);
    }

    public void ReloadSystemFnc()
    {
        if (magazine > 0)
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
        isReloading = false;
    }
}
