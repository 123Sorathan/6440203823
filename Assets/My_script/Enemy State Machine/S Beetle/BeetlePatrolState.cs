using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetlePatrolState : BeetleBaseState
{
    bool isStart;
    bool isReach;
    Vector3 destination1;

    public override void EnterState(BeetleController beetle)
    {
        isStart = true;
        destination1 = beetle.originPosition.position + new Vector3(-(beetle.statOfUnit.unitStats[5].movementRange), 0, 0);

        beetle.rd.totalForce = Vector2.zero;
        beetle.rd.velocity = Vector2.zero;
    }

    public override void UpdateState(BeetleController beetle)
    {
        if (beetle.enemyStat.hp > 0) // not die
        {
            if (beetle.isHit == true)
            {
                beetle.animator.SetTrigger("Hit");
                beetle.StartKnockBack();
            }
            else
            {
                if (beetle.isGoBackToOriginPoint == false)
                {
                    if (isStart)
                    {
                        beetle.animator.SetBool("beetle_Run", true);
                        beetle.transform.parent.position += new Vector3(-(beetle.statOfUnit.unitStats[5].movementRange), 0, 0) * beetle.statOfUnit.unitStats[5].movementSpeed * Time.deltaTime;
                        beetle.transform.parent.rotation = Quaternion.Euler(0, 180, 0); // Face Left
                        if (Vector3.Distance(beetle.transform.parent.position, destination1) <= 2f)
                        {
                            isStart = false;
                            isReach = true;
                        }
                    }

                    if (isReach)
                    {
                        beetle.animator.SetBool("beetle_Run", true);
                        beetle.transform.parent.position += new Vector3(beetle.statOfUnit.unitStats[5].movementRange, 0, 0) * beetle.statOfUnit.unitStats[5].movementSpeed * Time.deltaTime;
                        beetle.transform.parent.rotation = Quaternion.Euler(0, 0, 0); // Face Right
                        if (Vector3.Distance(beetle.transform.parent.position, beetle.originPosition.position) <= 2f)
                        {
                            isStart = true;
                            isReach = false;
                        }
                    }
                }
                else
                {
                    beetle.animator.SetBool("beetle_Run", true);
                    Vector3 newPosition = Vector3.Lerp(beetle.transform.parent.position, beetle.originPosition.position, beetle.statOfUnit.unitStats[5].movementSpeed * Time.deltaTime);
                    Vector3 directionOfOriginPoint = beetle.originPosition.position - beetle.transform.parent.position;
                    beetle.transform.parent.position = newPosition;
                    beetle.transform.parent.rotation = directionOfOriginPoint.x > 0 ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);

                    if (Vector3.Distance(beetle.transform.parent.position, beetle.originPosition.position) <= 2f)
                    {
                        beetle.isGoBackToOriginPoint = false;
                    }
                }
            }
        }

        else if (beetle.enemyStat.hp <= 0 && beetle.isDead == false)
        {
            beetle.animator.SetBool("beetle_Death", true);
            MonoBehaviour.Instantiate(beetle.coin, beetle.transform.position, Quaternion.identity);
            beetle.enemyCount.DecreaseEnemyCount("Beetle");
            MonoBehaviour.Destroy(beetle.transform.parent.gameObject, 3f);
            beetle.isDead = true;
        }
    }

    public override void OnTriggerEnter2D(BeetleController beetle, Collider2D other)
    {

        if (other.CompareTag("Player") == true)
        {

            beetle.targetPlayer = other.gameObject;
            beetle.SwitchState(beetle.chasingState);
        }
    }
    public override void OnTriggerExit2D(BeetleController beetle, Collider2D other)
    {

    }
    public override void ExitState(BeetleController beetle)
    {
        Debug.Log("1");
    }

}
