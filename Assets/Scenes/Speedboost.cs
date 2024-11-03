using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    public float boostDuration = 5.0f; // Gücün süresi
    public float speedMultiplier = 2.0f; // Hýz çarpaný
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

