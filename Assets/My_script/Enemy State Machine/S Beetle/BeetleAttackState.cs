using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleAttackState : BeetleBaseState
{
    private float lastShotTime = 0.0f;

    public override void EnterState(BeetleController beetle)
    {
        beetle.animator.SetBool("beetle_Attack", true);
        lastShotTime = 0f; // Reset cooldown timer

    }

    public override void UpdateState(BeetleController beetle)
    {
        if (beetle.enemyStat.hp > 0) // not die
        {
            if (Time.time > lastShotTime + beetle.statOfUnit.unitStats[2].attackCoolDown)
            {
                
                beetle.hpPlayer.TakeDamage(beetle.statOfUnit.unitStats[2].attackPower);
                lastShotTime = Time.time;

                // ตรวจสอบว่าอนิเมชั่นกำลังเล่นอยู่หรือไม่
                /*if (beetle.animator.GetCurrentAnimatorStateInfo(0).IsName("beetle_Attack"))
                {
                    // ตรวจสอบว่าอนิเมชั่นเล่นถึงครึ่งทาง (50%) หรือยัง
                    if (beetle.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.56f)
                    {
                       
                        if (Time.time > lastShotTime + beetle.statOfUnit.unitStats[2].attackCoolDown)
                        {
                            
                            beetle.hpPlayer.TakeDamage(beetle.statOfUnit.unitStats[2].attackPower);
                            lastShotTime = Time.time;
                        }
                    }
                }*/

            }

            if ((Vector3.Distance(beetle.targetPlayer.transform.position, beetle.transform.parent.position) > beetle.statOfUnit.unitStats[2].attackRange) || beetle.isHit)
            {
                beetle.animator.SetBool("beetle_Attack", false);
                beetle.SwitchState(beetle.chasingState);
            }
        }
       
        if (beetle.enemyStat.hp <= 0 && beetle.isDead == false)
        {
            beetle.animator.SetBool("beetle_Death", true);
            MonoBehaviour.Instantiate(beetle.coin, beetle.transform.position, Quaternion.identity);
            beetle.enemyCount.DecreaseEnemyCount("Beetle");
            MonoBehaviour.Destroy(beetle.transform.parent.gameObject, 3f);
            beetle.isDead = true;
        }
    }

    public override void OnTriggerEnter2D(BeetleController beetle, Collider2D other)
    {
        
    }
    public override void OnTriggerExit2D(BeetleController beetle, Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            beetle.isGoBackToOriginPoint = true;
            beetle.SwitchState(beetle.patrolState);
        }
    }
    public override void ExitState(BeetleController beetle)
    {
        Debug.Log("");
    }
}
