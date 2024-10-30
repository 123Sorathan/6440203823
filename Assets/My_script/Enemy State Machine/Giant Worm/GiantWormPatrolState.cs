using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantWormPatrolState : GiantWormBaseState
{
    bool isStart;
    bool isReach;
    Vector3 destination1;

    public override void EnterState(GiantWormController giant_worm)
    {
        isStart = true;
        destination1 = giant_worm.originPosition.position + new Vector3(-(giant_worm.statOfUnit.unitStats[6].movementRange), 0, 0);

        giant_worm.rd.totalForce = Vector2.zero;
        giant_worm.rd.velocity = Vector2.zero;

        //giant_worm.animator.SetBool("isPatrolling", true);
    }

    public override void UpdateState(GiantWormController giant_worm)
    {
        if (giant_worm.isHit == true)
        {
            giant_worm.StartKnockBack();
        }
        else
        {
            if (!giant_worm.isGoBackToOriginPoint)
            {
                if (isStart)
                {
                    giant_worm.transform.parent.position += new Vector3(-(giant_worm.statOfUnit.unitStats[6].movementRange), 0, 0) * giant_worm.statOfUnit.unitStats[6].movementSpeed * Time.deltaTime;
                    giant_worm.transform.parent.rotation = Quaternion.Euler(0, 180, 0); // Face Left
                    if (Vector3.Distance(giant_worm.transform.parent.position, destination1) <= 2f)
                    {
                        isStart = false;
                        isReach = true;
                    }
                }

                if (isReach)
                {
                    giant_worm.transform.parent.position += new Vector3(giant_worm.statOfUnit.unitStats[6].movementRange, 0, 0) * giant_worm.statOfUnit.unitStats[6].movementSpeed * Time.deltaTime;
                    giant_worm.transform.parent.rotation = Quaternion.Euler(0, 0, 0); // Face Right
                    if (Vector3.Distance(giant_worm.transform.parent.position, giant_worm.originPosition.position) <= 2f)
                    {
                        isStart = true;
                        isReach = false;
                    }
                }
            }
            else
            {
                Vector3 newPosition = Vector3.Lerp(giant_worm.transform.parent.position, giant_worm.originPosition.position, giant_worm.statOfUnit.unitStats[6].movementSpeed * Time.deltaTime);
                Vector3 directionOfOriginPoint = giant_worm.originPosition.position - giant_worm.transform.parent.position;
                giant_worm.transform.parent.position = newPosition;
                giant_worm.transform.parent.rotation = directionOfOriginPoint.x > 0 ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);

                if (Vector3.Distance(giant_worm.transform.parent.position, giant_worm.originPosition.position) <= 2f)
                {
                    giant_worm.isGoBackToOriginPoint = false;
                }
            }
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
        if (other.CompareTag("Player") == true)
        {
            giant_worm.targetPlayer = other.gameObject;
            giant_worm.SwitchState(giant_worm.chasingState);
        }
    }

    public override void OnTriggerExit2D(GiantWormController giant_worm, Collider2D other)
    {
        // Optional: Add exit behavior if needed
        Debug.Log("");
    }

    public override void ExitState(GiantWormController giant_worm)
    {
        // Stop patrol animation
        //giant_worm.animator.SetBool("isPatrolling", false);
        Debug.Log("");
    }
}