using UnityEngine;
using UnityEngine.AI;

public class Boss_Script : MonoBehaviour
{
    public Transform player; // Oyuncunun transformu
    private NavMeshAgent navMeshAgent;
    Animator Bossanimator;
    [Range(0.0f, 10.0f)]
    public float attackRange = 2.0f; // Saldýrý mesafesi

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        Bossanimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= attackRange)
            {
                navMeshAgent.SetDestination(transform.position); // Hareketi durdur
                Bossanimator.SetBool("Boss_Walking", false);
                Bossanimator.SetTrigger("Boss_Attack"); // Saldýrý animasyonunu tetikle
            }
            else
            {
                navMeshAgent.SetDestination(player.position);
                Bossanimator.SetBool("Boss_Walking", true);
            }
        }
    }
}
