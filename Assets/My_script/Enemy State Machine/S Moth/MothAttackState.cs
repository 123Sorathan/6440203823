using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothAttackState : MothBaseState
{
    private float lastShotTime = 0.0f;

    public override void EnterState(MothController moth)
    {
        //moth.animator.SetBool("moth_Attack", true);
        lastShotTime = 0f; // Reset cooldown timer

    }

    public override void UpdateState(MothController moth)
    {
        if (moth.enemyStat.hp > 0) // not die
        {
            if (Time.time > lastShotTime + moth.statOfUnit.unitStats[7].attackCoolDown)
            {
                // Add particle effect for fireball attack here
                // MonoBehaviour.Instantiate(ghost.fireballPrefab, ghost.transform.position, Quaternion.identity);
                //moth.animator.SetBool("moth_Attack", true);
                //moth.hpPlayer.TakeDamage(moth.statOfUnit.unitStats[2].attackPower);
                //lastShotTime = Time.time;

                // ตรวจสอบว่าอนิเมชั่นกำลังเล่นอยู่หรือไม่
                //if (moth.animator.GetCurrentAnimatorStateInfo(0).IsName("moth_Attack"))
                //{
                //    // ตรวจสอบว่าอนิเมชั่นเล่นถึงครึ่งทาง (50%) หรือยัง
                //    if (moth.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.56f)
                //    {
                //        // ตรวจสอบคูลดาวน์ก่อนโจมตี
                if (Time.time > lastShotTime + moth.statOfUnit.unitStats[7].attackCoolDown)
                {
                    // ทำการโจมตี

                    //Particle
                    GameObject attackParticle = MonoBehaviour.Instantiate(moth.ray, moth.transform.position, Quaternion.identity);
                    Vector3 attackParticleRotation = moth.transform.position - moth.targetPlayer.transform.position;
                    Vector3 oppositeDirection = moth.transform.position + attackParticleRotation;
                    attackParticle.transform.LookAt(oppositeDirection);



                    MonoBehaviour.Destroy(attackParticle, 2f);


                    //
                    moth.hpPlayer.TakeDamage(moth.statOfUnit.unitStats[7].attackPower);
                    lastShotTime = Time.time;
                    //  }
                    //}
                }
            }

            if ((Vector3.Distance(moth.targetPlayer.transform.position, moth.transform.parent.position) > moth.statOfUnit.unitStats[7].attackRange) || moth.isHit)
            {
                //moth.animator.SetBool("moth_Attack", false);
                moth.SwitchState(moth.chasingState);
            }
        }

        if (moth.enemyStat.hp <= 0 && moth.isDead == false)
        {
            //moth.animator.SetBool("moth_Death", true);
            MonoBehaviour.Instantiate(moth.coin, moth.transform.position, Quaternion.identity);
            moth.enemyCount.DecreaseEnemyCount("Moth");
            MonoBehaviour.Destroy(moth.transform.parent.gameObject, 0.5f);
            moth.isDead = true;
        }
    }

    public override void OnTriggerEnter2D(MothController moth, Collider2D other)
    {

    }
    public override void OnTriggerExit2D(MothController moth, Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            moth.isGoBackToOriginPoint = true;
            moth.SwitchState(moth.patrolState);
        }
    }
    public override void ExitState(MothController moth)
    {
        Debug.Log("");
    }
}
