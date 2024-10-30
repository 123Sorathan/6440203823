using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    public StatOfUnit statOfUnit;
    public Transform originPosition;
    public GameObject targetPlayer;
    public bool isGoBackToOriginPoint;
    public bool isHit;
    public CircleCollider2D attackRange;
    public CircleCollider2D Range;
    public EnemyStat enemyStat;
    public GameObject fireballPrefab;  
    public ObjectToClone objectToClone;
    public EnemyCount enemyCount;
    public Rigidbody2D rd;

    public HP_Player hpPlayer;
    public GameObject coin;

    private GhostBaseState currentState;
    public GhostPatrolState patrolState = new();
    public GhostChasingState chasingState = new();
    public GhostAttackState attackState = new();


    private void Awake()
    {
        rd = GetComponentInParent<Rigidbody2D>();
        attackRange.radius = statOfUnit.unitStats[1].attackRange;
        Range.radius = statOfUnit.unitStats[1].movementRange;
        hpPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<HP_Player>();
        enemyStat = GetComponentInParent<EnemyStat>();
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

    public void SwitchState(GhostBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    public void StartKnockBack()
    {
        StartCoroutine(CoolDownKnockBack(this));
    }

    IEnumerator CoolDownKnockBack(GhostController ghost)
    {
        yield return new WaitForSeconds(3);
        ghost.isHit = false;
        StopCoroutine(CoolDownKnockBack(this));
    }
}
