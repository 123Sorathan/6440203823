using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAttackState : GhostBaseState
{
    private float lastShotTime = 0.0f;

    public override void EnterState(GhostController ghost)
    {
        // Trigger attack animation here
        
    }

    public override void UpdateState(GhostController ghost)
    {
        if (Time.time > lastShotTime + ghost.statOfUnit.unitStats[1].attackCoolDown)
        {
            // Add particle effect for fireball attack here
            // MonoBehaviour.Instantiate(ghost.fireballPrefab, ghost.transform.position, Quaternion.identity);
            ghost.hpPlayer.TakeDamage(ghost.statOfUnit.unitStats[1].attackPower);
            lastShotTime = Time.time;
        }

        if ((Vector3.Distance(ghost.targetPlayer.transform.position, ghost.transform.parent.position) > ghost.statOfUnit.unitStats[1].attackRange) || ghost.isHit == true)
        {
            ghost.SwitchState(ghost.chasingState);
        }

            if (ghost.enemyStat.hp <= 0)
        {
            MonoBehaviour.Instantiate(ghost.coin, ghost.transform.position, Quaternion.identity);
            ghost.enemyCount.DecreaseEnemyCount("The Ghost");
            MonoBehaviour.Destroy(ghost.transform.parent.gameObject);
        }
    }

    public override void OnTriggerEnter2D(GhostController ghost, Collider2D other) {
        Debug.Log("");
     }
    public override void OnTriggerExit2D(GhostController ghost, Collider2D other) {

        if (other.CompareTag("Player"))
        {
            ghost.isGoBackToOriginPoint = true;
            ghost.SwitchState(ghost.patrolState);
        }
    }
    public override void ExitState(GhostController ghost) { 
        Debug.Log("");
    }
}
