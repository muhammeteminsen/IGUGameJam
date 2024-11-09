using UnityEngine;

public class EnemyFreeze : MonoBehaviour
{
    private Vector3 lastPosition;  // Düþmanýn önceki pozisyonu
    private Vector3 targetPosition;  // Hedef pozisyon (dondurulmuþken deðiþmez)
    private float freezeDuration = 3f;  // Dondurma süresi
    private float freezeTimer = 0f;  // Zamanlayýcý
    private bool isFrozen = false;  // Dondurma durumu

    private void Start()
    {
        // Düþmanýn baþlangýç pozisyonunu kaydet
        lastPosition = transform.position;
        targetPosition = lastPosition;
    }

    // Düþmaný dondur
    public void FreezeEnemy()
    {
        // Dondurma sýrasýnda düþmanýn pozisyonunu sabitle
        isFrozen = true;
        freezeTimer = freezeDuration;  // Dondurmanýn süresini baþlat
        targetPosition = transform.position;  // Dondurulan pozisyonu kaydet
    }

    // Zaman ilerledikçe, dondurmanýn süresi biterse çözülmesini saðla
    private void Update()
    {
        // Dondurma süresi devam ediyorsa, hareketi durdur
        if (isFrozen)
        {
            freezeTimer -= Time.deltaTime;  // Zamanlayýcýyý azalt

            // Dondurma süresi bittiðinde, hareketi serbest býrak
            if (freezeTimer <= 0)
            {
                isFrozen = false;
            }
        }

        // Eðer dondurulmamýþsa, düþmaný hareket ettir
        if (!isFrozen)
        {
            // Normal hareket fonksiyonunu buraya ekleyebilirsiniz
            // Örneðin:
            // transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
        else
        {
            // Dondurulduðunda düþmanýn pozisyonu sabit kalýr
            transform.position = targetPosition;
        }
    }
}
