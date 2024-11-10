using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemies : MonoBehaviour
{
    public float health;
    public float damage;

     
    [SerializeField] private float slowSkillSpeed = 0.5f;
    [Header("Health Bar Variables")] [SerializeField]
    private Image healthBar;
    [SerializeField] private Image backgroundHealthBar;
    [SerializeField] private Transform healthBarCanvas;
    [SerializeField] private float healthBarSmooth;

    [Header("Body Part Variables")] [SerializeField]
    private float headDamage;

    [SerializeField] private float bodyDamage;
    [SerializeField] private float armDamage;
    [SerializeField] private float legDamage;

    [Header("AI Variables")] [SerializeField]
    private float stoppingDistance;
    [SerializeField] private float hitDamage;

    private float _defaultHealth;
    private float _defaultDamage;
    private float _defaultSpeed;
    private Camera _camera;
    private NavMeshAgent _navMeshAgent;
    private Transform _destination;
    private ManaSystem _manaSystem;
    private bool _isDead;
    private Animator _animator;
    private PlayerHealth _playerHealth;
    

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _destination = GameObject.FindWithTag("Player").transform;
        _manaSystem = FindObjectOfType<ManaSystem>();
        _camera = Camera.main;
        _playerHealth = _destination.GetComponent<PlayerHealth>();
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _defaultHealth = health;
        _defaultDamage = damage;
        _defaultSpeed = _navMeshAgent.speed;
        _navMeshAgent.stoppingDistance = stoppingDistance;
    }

    private void Update()
    {
        HealthBar();
        if (!_isDead)
        {
            FreezeSkill();
            AIController();
            SlowDownSkill();
        }
       
    }

    public void TakeDamage(float damage, RaycastHit hit)
    {
        switch (hit.transform.gameObject.tag)
        {
            case "Head":
                this.damage = headDamage;
                break;
            case "Body":
                this.damage = bodyDamage;
                break;
            case "Arm":
                this.damage = armDamage;
                break;
            case "Leg":
                this.damage = legDamage;
                break;
            default:
                this.damage = _defaultDamage;
                break;
        }

        damage = this.damage;
        health -= damage;
        healthBar.fillAmount = health / _defaultHealth;
    }

    private void HealthBar()
    {
        if (healthBarCanvas != null)
        {
            healthBarCanvas.LookAt(_camera.transform);
            backgroundHealthBar.fillAmount = Mathf.Lerp(backgroundHealthBar.fillAmount, health / _defaultHealth,
                Time.deltaTime * healthBarSmooth);
        }

        if (Mathf.Approximately(backgroundHealthBar.fillAmount, 0))
        {
            _isDead = true;
            _animator.Play("Enemy_Die");
            _navMeshAgent.isStopped = true;
        }
            
    }

    private void FreezeSkill()
    {
        if (_manaSystem.isFreeze)
        {
            _navMeshAgent.isStopped = true;
        }
        else
        {
            _navMeshAgent.isStopped = false;
        }
    }

    private void SlowDownSkill()
    {
        _navMeshAgent.speed = _manaSystem.isSlowSkill ? slowSkillSpeed : _defaultSpeed;
    }
    private void AIController()
    {
        _navMeshAgent.SetDestination(_destination.position);
        if (_navMeshAgent.remainingDistance<=stoppingDistance)
        {
            _animator.SetBool("isPunch",true);
            _animator.SetBool("isRun",false);
            _navMeshAgent.isStopped = true;
        }
        else
        {
            _animator.SetBool("isPunch",false);
            _animator.SetBool("isRun",true);
            _navMeshAgent.isStopped = false;
        }
    }

    public void HitDamage()
    {
        _playerHealth.TakenDamage(hitDamage);
    } 
    public float DamageAmount => damage;

    public void Die()
    {
        _manaSystem.SetMana();
        _playerHealth.OnDestroyVariables();
        Destroy(gameObject);
    }
}