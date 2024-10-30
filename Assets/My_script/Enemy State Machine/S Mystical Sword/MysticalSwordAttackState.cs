using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysticalSwordAttackState: MysticalSwordBaseState
{
    private float lastShotTime = 0.0f;

    public override void EnterState(MysticalSwordController mystical_Sword)
    {
        //mystical_Sword.animator.SetBool("mystical_Sword_Attack", true);
        lastShotTime = 0f; // Reset cooldown timer

    }

    public override void UpdateState(MysticalSwordController mystical_Sword)
    {
        if (mystical_Sword.enemyStat.hp > 0) // not die
        {
            if (Time.time > lastShotTime + mystical_Sword.statOfUnit.unitStats[8].attackCoolDown)
            {
                // Add particle effect for fireball attack here
                // MonoBehaviour.Instantiate(ghost.fireballPrefab, ghost.transform.position, Quaternion.identity);
                //mystical_Sword.animator.SetBool("mystical_Sword_Attack", true);
                //mystical_Sword.hpPlayer.TakeDamage(mystical_Sword.statOfUnit.unitStats[2].attackPower);
                //lastShotTime = Time.time;

                // ตรวจสอบว่าอนิเมชั่นกำลังเล่นอยู่หรือไม่
                //if (mystical_Sword.animator.GetCurrentAnimatorStateInfo(0).IsName("mystical_Sword_Attack"))
                //{
                //    // ตรวจสอบว่าอนิเมชั่นเล่นถึงครึ่งทาง (50%) หรือยัง
                //    if (mystical_Sword.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.56f)
                //    {
                //        // ตรวจสอบคูลดาวน์ก่อนโจมตี
                if (Time.time > lastShotTime + mystical_Sword.statOfUnit.unitStats[8].attackCoolDown)
                {
                    // ทำการโจมตี

                    //Particle
                    GameObject attackParticle = MonoBehaviour.Instantiate(mystical_Sword.ray, mystical_Sword.transform.position, Quaternion.identity);
                    Vector3 attackParticleRotation = mystical_Sword.transform.position - mystical_Sword.targetPlayer.transform.position;
                    Vector3 oppositeDirection = mystical_Sword.transform.position + attackParticleRotation;
                    attackParticle.transform.LookAt(oppositeDirection);



                    MonoBehaviour.Destroy(attackParticle, 2f);


                    //
                    mystical_Sword.hpPlayer.TakeDamage(mystical_Sword.statOfUnit.unitStats[8].attackPower);
                    lastShotTime = Time.time;
                    //  }
                    //}
                }
            }

            if ((Vector3.Distance(mystical_Sword.targetPlayer.transform.position, mystical_Sword.transform.parent.position) > mystical_Sword.statOfUnit.unitStats[8].attackRange) || mystical_Sword.isHit)
            {
                //mystical_Sword.animator.SetBool("mystical_Sword_Attack", false);
                mystical_Sword.SwitchState(mystical_Sword.chasingState);
            }
        }

        if (mystical_Sword.enemyStat.hp <= 0 && mystical_Sword.isDead == false)
        {
            //mystical_Sword.animator.SetBool("mystical_Sword_Death", true);
            MonoBehaviour.Instantiate(mystical_Sword.coin, mystical_Sword.transform.position, Quaternion.identity);
            mystical_Sword.enemyCount.DecreaseEnemyCount("Mystical sword");
            MonoBehaviour.Destroy(mystical_Sword.transform.parent.gameObject, 0.5f);
            mystical_Sword.isDead = true;
        }
    }

    public override void OnTriggerEnter2D(MysticalSwordController mystical_Sword, Collider2D other)
    {

    }
    public override void OnTriggerExit2D(MysticalSwordController mystical_Sword, Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            mystical_Sword.isGoBackToOriginPoint = true;
            mystical_Sword.SwitchState(mystical_Sword.patrolState);
        }
    }
    public override void ExitState(MysticalSwordController mystical_Sword)
    {
        Debug.Log("");
    }
}
