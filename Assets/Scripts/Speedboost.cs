using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    public float boostDuration = 5.0f; // G�c�n s�resi
    public float speedMultiplier = 2.0f; // H�z �arpan�
    private float originalSpeed;

    private void Start()
    {
        originalSpeed = GetComponent<Movement>().movementSpeed;
    }

    public void ActivateSpeedBoost()
    {
        GetComponent<Movement>().movementSpeed = speedMultiplier;
        Invoke("DeactivateSpeedBoost", boostDuration);
    }

    private void DeactivateSpeedBoost()
    {
        GetComponent<Movement>().movementSpeed = originalSpeed;
    }
}

