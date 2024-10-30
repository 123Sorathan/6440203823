using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPatrolState : GhostBaseState
{
    bool isStart;
    bool isReach;
    Vector3 destination1;

    public override void EnterState(GhostController ghost)
    {
        isStart = true;
        destination1 = ghost.originPosition.position + new Vector3(-(ghost.statOfUnit.unitStats[1].movementRange), 0, 0);

        ghost.rd.totalForce = Vector2.zero;
        ghost.rd.velocity = Vector2.zero;
    }

    public override void UpdateState(GhostController ghost)
    {
        if (ghost.isHit == true)
        {
            ghost.StartKnockBack();
        }
        else
        {
            if (ghost.isGoBackToOriginPoint == false)
            {
                if (isStart)
                {
                    ghost.transform.parent.position += new Vector3(-(ghost.statOfUnit.unitStats[1].movementRange), 0, 0) * ghost.statOfUnit.unitStats[1].movementSpeed * Time.deltaTime;
                    ghost.transform.parent.rotation = Quaternion.Euler(0, 180, 0); // Face Left
                    if (Vector3.Distance(ghost.transform.parent.position, destination1) <= 2f)
                    {
                        isStart = false;
                        isReach = true;
                    }
                }

                if (isReach)
                {
                    ghost.transform.parent.position += new Vector3(ghost.statOfUnit.unitStats[1].movementRange, 0, 0) * ghost.statOfUnit.unitStats[1].movementSpeed * Time.deltaTime;
                    ghost.transform.parent.rotation = Quaternion.Euler(0, 0, 0); // Face Right
                    if (Vector3.Distance(ghost.transform.parent.position, ghost.originPosition.position) <= 2f)
                    {
                        isStart = true;
                        isReach = false;
                    }
                }
            }
            else
            {
                Vector3 newPosition = Vector3.Lerp(ghost.transform.parent.position, ghost.originPosition.position, ghost.statOfUnit.unitStats[1].movementSpeed * Time.deltaTime);
                Vector3 directionOfOriginPoint = ghost.originPosition.position - ghost.transform.parent.position;
                ghost.transform.parent.position = newPosition;
                ghost.transform.parent.rotation = directionOfOriginPoint.x > 0 ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);

                if (Vector3.Distance(ghost.transform.parent.position, ghost.originPosition.position) <= 2f)
                {
                    ghost.isGoBackToOriginPoint = false;
                }
            }
        }

        if (ghost.enemyStat.hp <= 0)
        {
            MonoBehaviour.Instantiate(ghost.coin, ghost.transform.position, Quaternion.identity);
            ghost.enemyCount.DecreaseEnemyCount("The Ghost");
            MonoBehaviour.Destroy(ghost.transform.parent.gameObject);
        }
    }

    public override void OnTriggerEnter2D(GhostController ghost, Collider2D other)
    {
        if(other.CompareTag("Player") == true){

            ghost.targetPlayer = other.gameObject;
            ghost.SwitchState(ghost.chasingState);
        }
    }
    public override void OnTriggerExit2D(GhostController ghost, Collider2D other) {
        Debug.Log("1");
     }
    public override void ExitState(GhostController ghost) { 
        Debug.Log("1");
    }
}
