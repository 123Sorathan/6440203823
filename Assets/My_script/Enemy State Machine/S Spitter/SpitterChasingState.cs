using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitterChasingState : SpitterBaseState
{
    private Rigidbody2D rb;
    private float speed = 5;

    public override void EnterState(SpitterController spitter)
    {
        rb = spitter.gameObject.transform.parent.GetComponent<Rigidbody2D>();
    }

    public override void UpdateState(SpitterController spitter)
    {
        if (spitter.enemyStat.hp > 0) // not die
        {
            if (spitter.isHit == true)
            {
                spitter.animator.SetTrigger("Hit");
                spitter.StartKnockBack();
            }
            else
            {
                Vector3 directionOfPlayer = spitter.targetPlayer.transform.position - spitter.transform.parent.position;

                // หันหน้าเข้าหาผู้เล่น
                if (directionOfPlayer.x > 0)
                {
                    spitter.transform.parent.rotation = Quaternion.Euler(0, 0, 0); // Face Right
                }
                else
                {
                    spitter.transform.parent.rotation = Quaternion.Euler(0, 180, 0); // Face Left
                }

                // ไล่ตามผู้เล่น
                Vector2 newPosition = new Vector2(rb.position.x + directionOfPlayer.normalized.x * speed * Time.deltaTime, rb.position.y + directionOfPlayer.normalized.y * speed * Time.deltaTime);
                spitter.transform.parent.position = newPosition;

                // เปลี่ยนไปยังสถานะโจมตีเมื่อใกล้พอ
                if (Vector3.Distance(spitter.transform.parent.position, spitter.targetPlayer.transform.position) <= spitter.statOfUnit.unitStats[2].attackRange)
                {
                    spitter.SwitchState(spitter.attackState);
                }
            }
        
            // ตรวจสอบการตาย
            if (spitter.enemyStat.hp <= 0 && spitter.isDead == false)
            {
                spitter.animator.SetBool("spitter_Death", true);
                MonoBehaviour.Instantiate(spitter.coin, spitter.transform.position, Quaternion.identity);
                spitter.enemyCount.DecreaseEnemyCount("spitter");
                MonoBehaviour.Destroy(spitter.transform.parent.gameObject, 3f);
                spitter.isDead = true;
            }
        }
    }

    public override void OnTriggerEnter2D(SpitterController spitter, Collider2D other)
    {
        //spitter.animator.SetTrigger("Hit");
    }
    public override void OnTriggerExit2D(SpitterController spitter, Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            spitter.isGoBackToOriginPoint = true;
            spitter.SwitchState(spitter.patrolState);
        }
    }
    public override void ExitState(SpitterController spitter)
    {
        Debug.Log("1");
    }
}
