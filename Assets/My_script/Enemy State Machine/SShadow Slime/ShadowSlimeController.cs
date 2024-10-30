using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShadowSlimeController : MonoBehaviour
{
    public StatOfUnit statOfUnit;
    public Transform originPosition;
    public GameObject targetPlayer;
    public bool isGoBackToOriginPoint;
    public bool isHit;
    public CircleCollider2D attackRange;
    public EnemyStat enemyStat;
    public GameObject coin;
    public ObjectToClone objectToClone;
    public EnemyCount enemyCount;

    public HP_Player hpPlayer;
    public ReSponse respawn;
    public PlayerStat playerStat;

    public bool isJumping;  // ตัวแปรสำหรับตรวจสอบการกระโดด
    public float jumpForce = 5f;  // ความแรงของการกระโดด
    public Rigidbody2D rb;  // ตัวแปร Rigidbody2D สำหรับฟิสิกส์
    public float jumpCooldown = 1f; // ระยะเวลารอระหว่างการกระโดดแต่ละครั้ง
    public float jumpTimer; // ตัวจับเวลาการกระโดด

    [SerializeField] public GameObject HitEffect;
    [SerializeField] GameObject TextPrefab;

    public GameObject currentHitEffect;

    private ShadowSlimeBaseState currentState;
    public ShadowSlimePatrolState patrolState = new ShadowSlimePatrolState();
    public ShadowSlimeChasingState chasingState = new ShadowSlimeChasingState();
    public ShadowSlimeAttackState attackState = new ShadowSlimeAttackState();

   private void Awake(){
        //originPosition = gameObject.transform.parent.position;

      attackRange.radius = statOfUnit.unitStats[0].attackRange;
      hpPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<HP_Player>();
      enemyStat = GetComponentInParent<EnemyStat>();
      coin = objectToClone.coin;
      enemyCount = GameObject.FindGameObjectWithTag("EnemyCount").GetComponent<EnemyCount>();
    }

    private void Start()
    {
       
   
       currentState = patrolState;
       currentState.EnterState(this);
    }

    private void Update()
    {
        Debug.Log(originPosition);
        Debug.Log(currentState+"hihh");

       currentState.UpdateState(this);

        //if(playerStat.isDead == false)
        //{
        //    GoBackWhenPlayerDie();
        //}

    }

    public void  OnTriggerEnter2D(Collider2D other) {

       currentState.OnTriggerEnter2D(this,other);
    }

    public void  OnTriggerExit2D(Collider2D other) {

       currentState.OnTriggerExit2D(this,other);
    }

    public void SwitchState(ShadowSlimeBaseState stateName){
        currentState = stateName;
        currentState.EnterState(this);
    }


   public void StartKnockBack()
   {
        StartCoroutine(CoolDownKnockBack(this));
        SwitchState(patrolState);
        // if(isHit == false){
        //   StopCoroutine(CoolDownKnockBack(this));
        // }
    }

   IEnumerator CoolDownKnockBack(ShadowSlimeController shadowSlime)
   {
       yield return new WaitForSeconds(1);
       shadowSlime.isHit = false;
       StopCoroutine(CoolDownKnockBack(this));
   }

    //private void GoBackWhenPlayerDie()
    //{
    //    respawn.BringAllToOriginPosition(gameObject.transform.parent.gameObject, originPosition);   
    //}
}


