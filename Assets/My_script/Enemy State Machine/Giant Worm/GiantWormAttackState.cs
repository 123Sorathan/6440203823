using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantWormAttackState : GiantWormBaseState
{
    private float lastShotTime = 0.0f;

    public override void EnterState(GiantWormController giant_worm)
    {
        // Trigger attack animation
        //giant_worm.animator.SetTrigger("attack");
    }

    public override void UpdateState(GiantWormController giant_worm)
    {
        if (Time.time > lastShotTime + giant_worm.statOfUnit.unitStats[6].attackCoolDown)
        {
            // Fireball attack effect (optional)
            // MonoBehaviour.Instantiate(giant_worm.fireballPrefab, giant_worm.transform.position, Quaternion.identity);
            giant_worm.hpPlayer.TakeDamage(giant_worm.statOfUnit.unitStats[6].attackPower);
            lastShotTime = Time.time;
        }

        if (Vector3.Distance(giant_worm.targetPlayer.transform.position, giant_worm.transform.parent.position) > giant_worm.statOfUnit.unitStats[6].attackRange || giant_worm.isHit)
        {
            giant_worm.SwitchState(giant_worm.chasingState);
        }

        if (giant_worm.enemyStat.hp <= 0)
        {
            MonoBehaviour.Instantiate(giant_worm.coin, giant_worm.transform.position, Quaternion.identity);
            giant_worm.enemyCount.DecreaseEnemyCount("Giant Worm");
            MonoBehaviour.Destroy(giant_worm.transform.parent.gameObject);
        }
    }

    public override void OnTriggerEnter2D(GiantWormController giant_worm, Collider2D other)
    {
        // Optional: Add behavior for entering triggers if needed
    }

    public override void OnTriggerExit2D(GiantWormController giant_worm, Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            giant_worm.isGoBackToOriginPoint = true;
            giant_worm.SwitchState(giant_worm.patrolState);
        }
    }

    public override void ExitState(GiantWormController giant_worm)
    {
        // Stop attack animation (if needed)
        Debug.Log("");
    }
}
