using UnityEngine;
using System.Collections;

public class FreezeEnemies : MonoBehaviour
{
    public EnemyMovement[] enemies;  // D��manlar� tutacak dizi
    public float freezeDuration = 5f;  // Dondurma s�resi (5 saniye)
    private bool isFrozenActive = false;  // Freeze skill'in aktif olup olmad���n� takip eder

    void Update()
    {
        // 3 tu�una bas�ld���nda, dondurma i�lemi ba�lat
        if (Input.GetKeyDown(KeyCode.Alpha3) && !isFrozenActive)
        {
            // Freeze i�lemini ba�lat
            StartCoroutine(FreezeEnemiesMovement());
        }
    }

    // D��manlar� 5 saniyeli�ine dondur
    private IEnumerator FreezeEnemiesMovement()
    {
        // D��manlar� dondur
        foreach (EnemyMovement enemy in enemies)
        {
            enemy.Freeze();
        }

        // Freeze aktif hale getir
        isFrozenActive = true;
        Debug.Log("Enemies' movement is frozen for " + freezeDuration + " seconds!");

        // 5 saniye bekle
        yield return new WaitForSeconds(freezeDuration);

        // D��manlar�n dondurmas�n� kald�r
        foreach (EnemyMovement enemy in enemies)
        {
            enemy.Unfreeze();
        }

        // Freeze deaktif hale getir
        isFrozenActive = false;
        Debug.Log("Enemies' movement is unfrozen after " + freezeDuration + " seconds!");
    }
}
