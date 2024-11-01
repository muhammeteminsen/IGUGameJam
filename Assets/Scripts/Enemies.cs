using UnityEngine;

public class Enemies : MonoBehaviour
{
    public float health;
    public float damage;
    public void TakeDamage(float damage)
    {
        damage = this.damage;
        health -= damage;
        if(health<=0)
            Die();
    }
    public float DamageAmount => damage;
    private void Die()
    {
        Destroy(gameObject);
    }
}
