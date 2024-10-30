using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyController : MonoBehaviour
{
    public StatOfUnit statOfUnit;
    public Transform originPosition;
    public GameObject targetPlayer;
    public bool isGoBackToOriginPoint;
    public bool isHit;
    public bool isDead;
    public CircleCollider2D attackRange;
    public EnemyStat enemyStat;
    public GameObject fireballPrefab;
    public ObjectToClone objectToClone;
    public EnemyCount enemyCount;
    public Rigidbody2D rd;
    public GameObject ray;

    public HP_Player hpPlayer;
    public GameObject coin;

    public Animator animator;

    private FireflyBaseState currentState;
    public FireflyPatrolState patrolState = new();
    public FireflyChasingState chasingState = new();
    public FireflyAttackState attackState = new();


    private void Awake()
    {
        rd = GetComponentInParent<Rigidbody2D>();
        attackRange.radius = statOfUnit.unitStats[9].attackRange;
        hpPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<HP_Player>();
        enemyStat = GetComponentInParent<EnemyStat>();
        animator = GetComponentInParent<Animator>();
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

    public void SwitchState(FireflyBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    public void StartKnockBack()
    {
        //animator.SetTrigger("Hit");
        StartCoroutine(CoolDownKnockBack(this));
    }

    IEnumerator CoolDownKnockBack(FireflyController firefly)
    {
        yield return new WaitForSeconds(0.2f);
        firefly.isHit = false;
        StopCoroutine(CoolDownKnockBack(this));
    }
}
