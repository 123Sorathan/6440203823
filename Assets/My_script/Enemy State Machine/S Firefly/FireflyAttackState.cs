using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyAttackState : FireflyBaseState
{
    private float lastShotTime = 0.0f;

    public override void EnterState(FireflyController firefly)
    {
        //firefly.animator.SetBool("firefly_Attack", true);
        lastShotTime = 0f; // Reset cooldown timer

    }

    public override void UpdateState(FireflyController firefly)
    {
        if (firefly.enemyStat.hp > 0) // not die
        {
            if (Time.time > lastShotTime + firefly.statOfUnit.unitStats[9].attackCoolDown)
            {
                // Add particle effect for fireball attack here
                // MonoBehaviour.Instantiate(ghost.fireballPrefab, ghost.transform.position, Quaternion.identity);
                //firefly.animator.SetBool("firefly_Attack", true);
                //firefly.hpPlayer.TakeDamage(firefly.statOfUnit.unitStats[2].attackPower);
                //lastShotTime = Time.time;

                // ตรวจสอบว่าอนิเมชั่นกำลังเล่นอยู่หรือไม่
                //if (firefly.animator.GetCurrentAnimatorStateInfo(0).IsName("firefly_Attack"))
                //{
                //    // ตรวจสอบว่าอนิเมชั่นเล่นถึงครึ่งทาง (50%) หรือยัง
                //    if (firefly.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.56f)
                //    {
                //        // ตรวจสอบคูลดาวน์ก่อนโจมตี
                        if (Time.time > lastShotTime + firefly.statOfUnit.unitStats[9].attackCoolDown)
                        {
                    // ทำการโจมตี

                    //Particle
                    GameObject attackParticle = MonoBehaviour.Instantiate(firefly.ray, firefly.transform.position, Quaternion.identity);
                    Vector3 attackParticleRotation = firefly.transform.position - firefly.targetPlayer.transform.position;
                    Vector3 oppositeDirection = firefly.transform.position + attackParticleRotation;
                    attackParticle.transform.LookAt(oppositeDirection);
                    
                    
                    
                    MonoBehaviour.Destroy(attackParticle, 2f);
                    
                    
                    //
                            firefly.hpPlayer.TakeDamage(firefly.statOfUnit.unitStats[9].attackPower);
                            lastShotTime = Time.time;
                    //  }
                    //}
                }
            }

            if ((Vector3.Distance(firefly.targetPlayer.transform.position, firefly.transform.parent.position) > firefly.statOfUnit.unitStats[9].attackRange) || firefly.isHit)
            {
                //firefly.animator.SetBool("firefly_Attack", false);
                firefly.SwitchState(firefly.chasingState);
            }
        }

        if (firefly.enemyStat.hp <= 0 && firefly.isDead == false)
        {
            //firefly.animator.SetBool("firefly_Death", true);
            MonoBehaviour.Instantiate(firefly.coin, firefly.transform.position, Quaternion.identity);
            firefly.enemyCount.DecreaseEnemyCount("Firefly");
            MonoBehaviour.Destroy(firefly.transform.parent.gameObject, 0.5f);
            firefly.isDead = true;
        }
    }

    public override void OnTriggerEnter2D(FireflyController firefly, Collider2D other)
    {

    }
    public override void OnTriggerExit2D(FireflyController firefly, Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            firefly.isGoBackToOriginPoint = true;
            firefly.SwitchState(firefly.patrolState);
        }
    }
    public override void ExitState(FireflyController firefly)
    {
        Debug.Log("");
    }
}
