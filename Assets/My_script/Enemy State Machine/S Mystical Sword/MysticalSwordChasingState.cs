using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysticalSwordChasingState: MysticalSwordBaseState
{ 
    private Rigidbody2D rb;
    private float speed = 5;

    public override void EnterState(MysticalSwordController mystical_Sword)
    {
        rb = mystical_Sword.gameObject.transform.parent.GetComponent<Rigidbody2D>();
    }

    public override void UpdateState(MysticalSwordController mystical_Sword)
    {
        Debug.Log("mystical_Sword is chasing");
        if (mystical_Sword.enemyStat.hp > 0) // not die
        {
            if (mystical_Sword.isHit == true)
            {
                //mystical_Sword.animator.SetTrigger("Hit");
                mystical_Sword.StartKnockBack();
            }
            else
            {
                Vector3 directionOfPlayer = mystical_Sword.targetPlayer.transform.position - mystical_Sword.transform.parent.position;

                // หันหน้าเข้าหาผู้เล่น
                if (directionOfPlayer.x > 0)
                {
                    mystical_Sword.transform.parent.rotation = Quaternion.Euler(0, 0, 0); // Face Right
                }
                else
                {
                    mystical_Sword.transform.parent.rotation = Quaternion.Euler(0, 180, 0); // Face Left
                }

                // ไล่ตามผู้เล่น
                Vector2 newPosition = new Vector2(rb.position.x + directionOfPlayer.normalized.x * speed * Time.deltaTime, rb.position.y + directionOfPlayer.normalized.y * speed * Time.deltaTime);
                mystical_Sword.transform.parent.position = newPosition;

                // เปลี่ยนไปยังสถานะโจมตีเมื่อใกล้พอ
                if (Vector3.Distance(mystical_Sword.transform.parent.position, mystical_Sword.targetPlayer.transform.position) <= mystical_Sword.statOfUnit.unitStats[8].attackRange)
                {
                    mystical_Sword.SwitchState(mystical_Sword.attackState);
                }
            }

            // ตรวจสอบการตาย
            if (mystical_Sword.enemyStat.hp <= 0 && mystical_Sword.isDead == false)
            {
                //mystical_Sword.animator.SetBool("mystical_Sword_Death", true);
                MonoBehaviour.Instantiate(mystical_Sword.coin, mystical_Sword.transform.position, Quaternion.identity);
                mystical_Sword.enemyCount.DecreaseEnemyCount("Mystical sword");
                MonoBehaviour.Destroy(mystical_Sword.transform.parent.gameObject, 0.5f);
                mystical_Sword.isDead = true;
            }
        }
    }

    public override void OnTriggerEnter2D(MysticalSwordController mystical_Sword, Collider2D other)
    {
        //mystical_Sword.animator.SetTrigger("Hit");
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
        Debug.Log("1");
    }
}
