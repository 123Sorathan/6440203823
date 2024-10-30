using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonChasingState : SkeletonBaseState
{
    private Rigidbody2D rb;
    private float speed = 5;

    public override void EnterState(SkeletonController skeleton)
    {
        rb = skeleton.gameObject.transform.parent.GetComponent<Rigidbody2D>();
        // Play chasing animation
        //skeleton.animator.SetBool("isChasing", true);
    }

    public override void UpdateState(SkeletonController skeleton)
    {
        if (skeleton.isHit == true)
        {
            skeleton.StartKnockBack();
        }
        else
        {
            Vector3 directionOfPlayer = skeleton.targetPlayer.transform.position - skeleton.transform.parent.position;

            skeleton.transform.parent.rotation = directionOfPlayer.x > 0 ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);

            // Chase the player
            Vector2 newPosition = new Vector2(rb.position.x + directionOfPlayer.normalized.x * speed * Time.deltaTime, rb.position.y + directionOfPlayer.normalized.y * speed * Time.deltaTime);
            skeleton.transform.parent.position = newPosition;

            // Switch to attack state when close enough
            if (Vector3.Distance(skeleton.transform.parent.position, skeleton.targetPlayer.transform.position) <= skeleton.statOfUnit.unitStats[6].attackRange)
            {
                skeleton.SwitchState(skeleton.attackState);
            }

            // Check for death
            if (skeleton.enemyStat.hp <= 0)
            {
                MonoBehaviour.Instantiate(skeleton.coin, skeleton.transform.position, Quaternion.identity);
                skeleton.enemyCount.DecreaseEnemyCount("Skeleton");
                MonoBehaviour.Destroy(skeleton.transform.parent.gameObject);
            }
        }
    }

    public override void OnTriggerEnter2D(SkeletonController skeleton, Collider2D other)
    {
        // Optional: Add behavior for entering triggers if needed
    }

    public override void OnTriggerExit2D(SkeletonController skeleton, Collider2D other)
    {
        if (other.CompareTag("Player") == false)
        {
            skeleton.isGoBackToOriginPoint = true;
            skeleton.SwitchState(skeleton.patrolState); 
            
        }
    }

    public override void ExitState(SkeletonController skeleton)
    {
        // Stop chasing animation
        //skeleton.animator.SetBool("isChasing", false);
        Debug.Log("");
    }
}
