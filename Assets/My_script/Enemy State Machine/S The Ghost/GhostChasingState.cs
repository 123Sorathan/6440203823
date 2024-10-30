using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostChasingState : GhostBaseState
{
    private Rigidbody2D rb;
    private float speed = 5;

    public override void EnterState(GhostController ghost)
    {
        rb = ghost.gameObject.transform.parent.GetComponent<Rigidbody2D>();
    }

    public override void UpdateState(GhostController ghost)
    {
        if (ghost.isHit == true)
        {
            ghost.StartKnockBack();
        }
        else
        {
            Vector3 directionOfPlayer = ghost.targetPlayer.transform.position - ghost.transform.parent.position;

            // หันหน้าเข้าหาผู้เล่น
            if (directionOfPlayer.x > 0)
            {
                ghost.transform.parent.rotation = Quaternion.Euler(0, 0, 0); // Face Right
            }
            else
            {
                ghost.transform.parent.rotation = Quaternion.Euler(0, 180, 0); // Face Left
            }

            // ไล่ตามผู้เล่น
            Vector2 newPosition = new Vector2(rb.position.x + directionOfPlayer.normalized.x * speed * Time.deltaTime, rb.position.y + directionOfPlayer.normalized.y * speed * Time.deltaTime);
            ghost.transform.parent.position = newPosition;

            // เปลี่ยนไปยังสถานะโจมตีเมื่อใกล้พอ
            if (Vector3.Distance(ghost.transform.parent.position, ghost.targetPlayer.transform.position) <= ghost.statOfUnit.unitStats[1].attackRange)
            {
                ghost.SwitchState(ghost.attackState);
            }

            // ตรวจสอบการตาย
            if (ghost.enemyStat.hp <= 0)
            {
                MonoBehaviour.Instantiate(ghost.coin, ghost.transform.position, Quaternion.identity);
                ghost.enemyCount.DecreaseEnemyCount("The Ghost");
                MonoBehaviour.Destroy(ghost.transform.parent.gameObject);
            }
        }
    }

    public override void OnTriggerEnter2D(GhostController ghost, Collider2D other) {
        Debug.Log("1");
     }
    public override void OnTriggerExit2D(GhostController ghost, Collider2D other) {

        if (other.CompareTag("Player"))
        {
            ghost.isGoBackToOriginPoint = true;
            ghost.SwitchState(ghost.patrolState);
        }
    }
    public override void ExitState(GhostController ghost) {
        Debug.Log("1");
     }
}

