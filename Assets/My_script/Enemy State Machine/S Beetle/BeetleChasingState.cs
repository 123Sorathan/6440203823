using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleChasingState : BeetleBaseState
{
    private Rigidbody2D rb;
    private float speed = 5;

    public override void EnterState(BeetleController beetle)
    {
        rb = beetle.gameObject.transform.parent.GetComponent<Rigidbody2D>();
    }

    public override void UpdateState(BeetleController beetle)
    {
        if (beetle.enemyStat.hp > 0) // not die
        {
            if (beetle.isHit == true)
            {
                beetle.animator.SetTrigger("Hit");
                beetle.StartKnockBack();
            }
            else
            {
                Vector3 directionOfPlayer = beetle.targetPlayer.transform.position - beetle.transform.parent.position;

                // หันหน้าเข้าหาผู้เล่น
                if (directionOfPlayer.x > 0)
                {
                    beetle.transform.parent.rotation = Quaternion.Euler(0, 0, 0); // Face Right
                }
                else
                {
                    beetle.transform.parent.rotation = Quaternion.Euler(0, 180, 0); // Face Left
                }

                // ไล่ตามผู้เล่น
                Vector2 newPosition = new Vector2(rb.position.x + directionOfPlayer.normalized.x * speed * Time.deltaTime, rb.position.y + directionOfPlayer.normalized.y * speed * Time.deltaTime);
                beetle.transform.parent.position = newPosition;

                // เปลี่ยนไปยังสถานะโจมตีเมื่อใกล้พอ
                if (Vector3.Distance(beetle.transform.parent.position, beetle.targetPlayer.transform.position) <= beetle.statOfUnit.unitStats[5].attackRange)
                {
                    beetle.SwitchState(beetle.attackState);
                }
            }
        
            // ตรวจสอบการตาย
            if (beetle.enemyStat.hp <= 0 && beetle.isDead == false)
            {
                beetle.animator.SetBool("beetle_Death", true);
                MonoBehaviour.Instantiate(beetle.coin, beetle.transform.position, Quaternion.identity);
                beetle.enemyCount.DecreaseEnemyCount("Beetle");
                MonoBehaviour.Destroy(beetle.transform.parent.gameObject, 3f);
                beetle.isDead = true;
            }
        }
    }

    public override void OnTriggerEnter2D(BeetleController beetle, Collider2D other)
    {
        //beetle.animator.SetTrigger("Hit");
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
        Debug.Log("1");
    }
}
