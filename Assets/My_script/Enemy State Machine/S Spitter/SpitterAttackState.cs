using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitterAttackState : SpitterBaseState
{
    private float lastShotTime = 0.0f;
    private float startTime;

    public override void EnterState(SpitterController spitter)
    {
        lastShotTime = 0f; // Reset cooldown timer
        startTime = Time.time;
    }

    public override void UpdateState(SpitterController spitter)
    {
        if (spitter.enemyStat.hp > 0) // not die         
        {
            float elapseTime = Time.time - startTime;


            if (elapseTime > spitter.statOfUnit.unitStats[3].attackCoolDown)
            {
                MonoBehaviour.Instantiate(spitter.objectToClone.fireball, spitter.transform.position, Quaternion.identity);
                startTime = Time.time;
                spitter.animator.SetBool("spitter_Attack", true);

                // ตรวจสอบว่าอนิเมชั่นกำลังเล่นอยู่หรือไม่
                if (spitter.animator.GetCurrentAnimatorStateInfo(0).IsName("spitter_Attack"))
                {
                    // ตรวจสอบว่าอนิเมชั่นเล่นถึงครึ่งทาง (50%) หรือยัง
                    if (spitter.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.56f)
                    {
                        // ตรวจสอบคูลดาวน์ก่อนโจมตี
                        //spitter.hpPlayer.TakeDamage(spitter.statOfUnit.unitStats[2].attackPower);
                        //lastShotTime = Time.time;
                        
                    }
                }
            }

            if ((Vector3.Distance(spitter.targetPlayer.transform.position, spitter.transform.parent.position) > spitter.statOfUnit.unitStats[2].attackRange) || spitter.isHit)
            {
                spitter.animator.SetBool("spitter_Attack", false);
                spitter.SwitchState(spitter.chasingState);
            }
        }
       
        if (spitter.enemyStat.hp <= 0 && spitter.isDead == false)
        {
            spitter.animator.SetBool("spitter_Death", true);
            MonoBehaviour.Instantiate(spitter.coin, spitter.transform.position, Quaternion.identity);
            spitter.enemyCount.DecreaseEnemyCount("spitter");
            MonoBehaviour.Destroy(spitter.transform.parent.gameObject, 3f);
            spitter.isDead = true;
        }
    }

    public override void OnTriggerEnter2D(SpitterController spitter, Collider2D other)
    {
        
    }
    public override void OnTriggerExit2D(SpitterController spitter, Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            spitter.animator.SetBool("spitter_Attack", false);
            spitter.isGoBackToOriginPoint = true;
            spitter.SwitchState(spitter.patrolState);
        }
    }
    public override void ExitState(SpitterController spitter)
    {
        Debug.Log("");
    }
}
