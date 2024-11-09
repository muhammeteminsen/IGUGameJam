using UnityEngine;

public class EnemyFreeze : MonoBehaviour
{
    private Vector3 lastPosition;  // D��man�n �nceki pozisyonu
    private Vector3 targetPosition;  // Hedef pozisyon (dondurulmu�ken de�i�mez)
    private float freezeDuration = 3f;  // Dondurma s�resi
    private float freezeTimer = 0f;  // Zamanlay�c�
    private bool isFrozen = false;  // Dondurma durumu

    private void Start()
    {
        // D��man�n ba�lang�� pozisyonunu kaydet
        lastPosition = transform.position;
        targetPosition = lastPosition;
    }

    // D��man� dondur
    public void FreezeEnemy()
    {
        // Dondurma s�ras�nda d��man�n pozisyonunu sabitle
        isFrozen = true;
        freezeTimer = freezeDuration;  // Dondurman�n s�resini ba�lat
        targetPosition = transform.position;  // Dondurulan pozisyonu kaydet
    }

    // Zaman ilerledik�e, dondurman�n s�resi biterse ��z�lmesini sa�la
    private void Update()
    {
        // Dondurma s�resi devam ediyorsa, hareketi durdur
        if (isFrozen)
        {
            freezeTimer -= Time.deltaTime;  // Zamanlay�c�y� azalt

            // Dondurma s�resi bitti�inde, hareketi serbest b�rak
            if (freezeTimer <= 0)
            {
                isFrozen = false;
            }
        }

        // E�er dondurulmam��sa, d��man� hareket ettir
        if (!isFrozen)
        {
            // Normal hareket fonksiyonunu buraya ekleyebilirsiniz
            // �rne�in:
            // transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
        else
        {
            // Donduruldu�unda d��man�n pozisyonu sabit kal�r
            transform.position = targetPosition;
        }
    }
}
