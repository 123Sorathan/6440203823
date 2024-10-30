using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantWormController: MonoBehaviour
{
    public StatOfUnit statOfUnit;
    public Transform originPosition;
    public GameObject targetPlayer;
    public bool isGoBackToOriginPoint;
    public bool isHit;
    public CircleCollider2D attackRange;
    public EnemyStat enemyStat;
    public GameObject fireballPrefab;  
    public ObjectToClone objectToClone;
    public EnemyCount enemyCount;
    public Rigidbody2D rd;

    public HP_Player hpPlayer;
    public GameObject coin;

    private GiantWormBaseState currentState;
    public  GiantWormPatrolState patrolState = new();
    public  GiantWormChasingState chasingState = new();
    public  GiantWormAttackState attackState = new();

   public Animator animator; // Add an animator for handling animations

    private void Awake()
    {
        rd = GetComponentInParent<Rigidbody2D>();
        attackRange.radius = statOfUnit.unitStats[5].attackRange;
        hpPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<HP_Player>();
        enemyStat = GetComponentInParent<EnemyStat>();
        animator = GetComponentInParent<Animator>(); // Initialize the animator
    }

    private void Start()
    {
        currentState = patrolState;
        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        currentState.OnTriggerEnter2D(this, other);
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        currentState.OnTriggerExit2D(this, other);
    }

    public void SwitchState(GiantWormBaseState state)
    {
        currentState.ExitState(this); // Exit the current state before switching
        currentState = state;
        currentState.EnterState(this);
    }

    public void StartKnockBack()
    {
        StartCoroutine(CoolDownKnockBack(this));
    }

    IEnumerator CoolDownKnockBack(GiantWormController giant_worm)
    {
        yield return new WaitForSeconds(3);
        giant_worm.isHit = false;
        StopCoroutine(CoolDownKnockBack(this));
    }
}