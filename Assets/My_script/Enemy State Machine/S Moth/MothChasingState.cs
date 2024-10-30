using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothChasingState : MothBaseState
{
    private Rigidbody2D rb;
    private float speed = 5;

    public override void EnterState(MothController moth)
    {
        rb = moth.gameObject.transform.parent.GetComponent<Rigidbody2D>();
    }

    public override void UpdateState(MothController moth)
    {
        Debug.Log("Moth is chasing");
        if (moth.enemyStat.hp > 0) // not die
        {
            if (moth.isHit == true)
            {
                //Moth.animator.SetTrigger("Hit");
                moth.StartKnockBack();
            }
            else
            {
                Vector3 directionOfPlayer = moth.targetPlayer.transform.position - moth.transform.parent.position;

                // หันหน้าเข้าหาผู้เล่น
                if (directionOfPlayer.x > 0)
                {
                    moth.transform.parent.rotation = Quaternion.Euler(0, 0, 0); // Face Right
                }
                else
                {
                    moth.transform.parent.rotation = Quaternion.Euler(0, 180, 0); // Face Left
                }

                // ไล่ตามผู้เล่น
                Vector2 newPosition = new Vector2(rb.position.x + directionOfPlayer.normalized.x * speed * Time.deltaTime, rb.position.y + directionOfPlayer.normalized.y * speed * Time.deltaTime);
                moth.transform.parent.position = newPosition;

                // เปลี่ยนไปยังสถานะโจมตีเมื่อใกล้พอ
                if (Vector3.Distance(moth.transform.parent.position, moth.targetPlayer.transform.position) <= moth.statOfUnit.unitStats[7].attackRange)
                {
                    moth.SwitchState(moth.attackState);
                }
            }

            // ตรวจสอบการตาย
            if (moth.enemyStat.hp <= 0 && moth.isDead == false)
            {
                //Moth.animator.SetBool("Moth_Death", true);
                MonoBehaviour.Instantiate(moth.coin, moth.transform.position, Quaternion.identity);
                moth.enemyCount.DecreaseEnemyCount("Moth");
                MonoBehaviour.Destroy(moth.transform.parent.gameObject, 0.5f);
                moth.isDead = true;
            }
        }
    }

    public override void OnTriggerEnter2D(MothController moth, Collider2D other)
    {
        //Moth.animator.SetTrigger("Hit");
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
        Debug.Log("1");
    }
}
