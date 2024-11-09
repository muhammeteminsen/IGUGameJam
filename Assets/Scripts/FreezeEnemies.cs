using UnityEngine;
using System.Collections;

public class FreezeEnemies : MonoBehaviour
{
    public EnemyMovement[] enemies;  // Düþmanlarý tutacak dizi
    public float freezeDuration = 5f;  // Dondurma süresi (5 saniye)
    private bool isFrozenActive = false;  // Freeze skill'in aktif olup olmadýðýný takip eder

    void Update()
    {
        // 3 tuþuna basýldýðýnda, dondurma iþlemi baþlat
        if (Input.GetKeyDown(KeyCode.Alpha3) && !isFrozenActive)
        {
            // Freeze iþlemini baþlat
            StartCoroutine(FreezeEnemiesMovement());
        }
    }

    // Düþmanlarý 5 saniyeliðine dondur
    private IEnumerator FreezeEnemiesMovement()
    {
        // Düþmanlarý dondur
        foreach (EnemyMovement enemy in enemies)
        {
            enemy.Freeze();
        }

        // Freeze aktif hale getir
        isFrozenActive = true;
        Debug.Log("Enemies' movement is frozen for " + freezeDuration + " seconds!");

        // 5 saniye bekle
        yield return new WaitForSeconds(freezeDuration);

        // Düþmanlarýn dondurmasýný kaldýr
        foreach (EnemyMovement enemy in enemies)
        {
            enemy.Unfreeze();
        }

        // Freeze deaktif hale getir
        isFrozenActive = false;
        Debug.Log("Enemies' movement is unfrozen after " + freezeDuration + " seconds!");
    }
}
