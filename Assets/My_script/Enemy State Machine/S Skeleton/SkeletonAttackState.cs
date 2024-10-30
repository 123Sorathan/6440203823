using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttackState : SkeletonBaseState
{
    private float lastShotTime = 0.0f;

    public override void EnterState(SkeletonController skeleton)
    {
        // Trigger attack animation
        //skeleton.animator.SetTrigger("attack");
    }

    public override void UpdateState(SkeletonController skeleton)
    {
        if (Time.time > lastShotTime + skeleton.statOfUnit.unitStats[6].attackCoolDown)
        {
            // Fireball attack effect (optional)
            // MonoBehaviour.Instantiate(skeleton.fireballPrefab, skeleton.transform.position, Quaternion.identity);
            skeleton.hpPlayer.TakeDamage(skeleton.statOfUnit.unitStats[6].attackPower);
            lastShotTime = Time.time;
        }

        if (Vector3.Distance(skeleton.targetPlayer.transform.position, skeleton.transform.parent.position) > skeleton.statOfUnit.unitStats[6].attackRange || skeleton.isHit)
        {
            skeleton.SwitchState(skeleton.chasingState);
        }

        if (skeleton.enemyStat.hp <= 0)
        {
            MonoBehaviour.Instantiate(skeleton.coin, skeleton.transform.position, Quaternion.identity);
            skeleton.enemyCount.DecreaseEnemyCount("Skeleton");
            MonoBehaviour.Destroy(skeleton.transform.parent.gameObject);
        }
    }

    public override void OnTriggerEnter2D(SkeletonController skeleton, Collider2D other)
    {
        // Optional: Add behavior for entering triggers if needed
    }

    public override void OnTriggerExit2D(SkeletonController skeleton, Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            skeleton.isGoBackToOriginPoint = true;
            skeleton.SwitchState(skeleton.patrolState);
        }
    }

    public override void ExitState(SkeletonController skeleton)
    {
        // Stop attack animation (if needed)
        Debug.Log("");
    }
}
