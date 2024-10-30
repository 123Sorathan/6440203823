using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyChasingState : FireflyBaseState
{
    private Rigidbody2D rb;
    private float speed = 5;

    public override void EnterState(FireflyController firefly)
    {
        rb = firefly.gameObject.transform.parent.GetComponent<Rigidbody2D>();
    }

    public override void UpdateState(FireflyController firefly)
    {
        Debug.Log("Firefly is chasing");
        if (firefly.enemyStat.hp > 0) // not die
        {
            if (firefly.isHit == true)
            {
                //firefly.animator.SetTrigger("Hit");
                firefly.StartKnockBack();
            }
            else
            {
                Vector3 directionOfPlayer = firefly.targetPlayer.transform.position - firefly.transform.parent.position;

                // หันหน้าเข้าหาผู้เล่น
                if (directionOfPlayer.x > 0)
                {
                    firefly.transform.parent.rotation = Quaternion.Euler(0, 0, 0); // Face Right
                }
                else
                {
                    firefly.transform.parent.rotation = Quaternion.Euler(0, 180, 0); // Face Left
                }

                // ไล่ตามผู้เล่น
                Vector2 newPosition = new Vector2(rb.position.x + directionOfPlayer.normalized.x * speed * Time.deltaTime, rb.position.y + directionOfPlayer.normalized.y * speed * Time.deltaTime);
                firefly.transform.parent.position = newPosition;

                // เปลี่ยนไปยังสถานะโจมตีเมื่อใกล้พอ
                if (Vector3.Distance(firefly.transform.parent.position, firefly.targetPlayer.transform.position) <= firefly.statOfUnit.unitStats[9].attackRange)
                {
                    firefly.SwitchState(firefly.attackState);
                }
            }

            // ตรวจสอบการตาย
            if (firefly.enemyStat.hp <= 0 && firefly.isDead == false)
            {
                //firefly.animator.SetBool("firefly_Death", true);
                MonoBehaviour.Instantiate(firefly.coin, firefly.transform.position, Quaternion.identity);
                firefly.enemyCount.DecreaseEnemyCount("Firefly");
                MonoBehaviour.Destroy(firefly.transform.parent.gameObject, 0.5f);
                firefly.isDead = true;
            }
        }
    }

    public override void OnTriggerEnter2D(FireflyController firefly, Collider2D other)
    {
        //firefly.animator.SetTrigger("Hit");
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
        Debug.Log("1");
    }
}
