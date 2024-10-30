using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantWormChasingState : GiantWormBaseState
{
    private Rigidbody2D rb;
    private float speed = 5;

    public override void EnterState(GiantWormController giant_worm)
    {
        rb = giant_worm.gameObject.transform.parent.GetComponent<Rigidbody2D>();
        // Play chasing animation
        //giant_worm.animator.SetBool("isChasing", true);
    }

    public override void UpdateState(GiantWormController giant_worm)
    {
        if (giant_worm.isHit == true)
        {
            giant_worm.StartKnockBack();
        }
        else
        {
            Vector3 directionOfPlayer = giant_worm.targetPlayer.transform.position - giant_worm.transform.parent.position;

            giant_worm.transform.parent.rotation = directionOfPlayer.x > 0 ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);

            // Chase the player
            Vector2 newPosition = new Vector2(rb.position.x + directionOfPlayer.normalized.x * speed * Time.deltaTime, rb.position.y + directionOfPlayer.normalized.y * speed * Time.deltaTime);
            giant_worm.transform.parent.position = newPosition;

            // Switch to attack state when close enough
            if (Vector3.Distance(giant_worm.transform.parent.position, giant_worm.targetPlayer.transform.position) <= giant_worm.statOfUnit.unitStats[6].attackRange)
            {
                giant_worm.SwitchState(giant_worm.attackState);
            }

            // Check for death
            if (giant_worm.enemyStat.hp <= 0)
            {
                MonoBehaviour.Instantiate(giant_worm.coin, giant_worm.transform.position, Quaternion.identity);
                giant_worm.enemyCount.DecreaseEnemyCount("Giant Worm");
                MonoBehaviour.Destroy(giant_worm.transform.parent.gameObject);
            }
        }
    }

    public override void OnTriggerEnter2D(GiantWormController giant_worm, Collider2D other)
    {
        // Optional: Add behavior for entering triggers if needed
    }

    public override void OnTriggerExit2D(GiantWormController giant_worm, Collider2D other)
    {
        if (other.CompareTag("Player") == false)
        {
            giant_worm.isGoBackToOriginPoint = true;
            giant_worm.SwitchState(giant_worm.patrolState); 
            
        }
    }

    public override void ExitState(GiantWormController giant_worm)
    {
        // Stop chasing animation
        //giant_worm.animator.SetBool("isChasing", false);
        Debug.Log("");
    }
}
