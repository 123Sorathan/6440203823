using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothPatrolState : MothBaseState
{
    bool isStart;
    bool isReach;
    Vector3 destination1;

    public override void EnterState(MothController moth)
    {
        isStart = true;
        destination1 = moth.originPosition.position + new Vector3(-(moth.statOfUnit.unitStats[7].movementRange), 0, 0);

        moth.rd.totalForce = Vector2.zero;
        moth.rd.velocity = Vector2.zero;
    }

    public override void UpdateState(MothController moth)
    {
        if (moth.enemyStat.hp > 0) // not die
        {
            if (moth.isHit == true)
            {
                //moth.animator.SetTrigger("Hit");
                moth.StartKnockBack();
            }
            else
            {
                if (moth.isGoBackToOriginPoint == false)
                {
                    if (isStart)
                    {
                        //moth.animator.SetBool("moth_Run", true);
                        moth.transform.parent.position += new Vector3(-(moth.statOfUnit.unitStats[7].movementRange), 0, 0) * moth.statOfUnit.unitStats[7].movementSpeed * Time.deltaTime;
                        moth.transform.parent.rotation = Quaternion.Euler(0, 180, 0); // Face Left
                        if (Vector3.Distance(moth.transform.parent.position, destination1) <= 2f)
                        {
                            isStart = false;
                            isReach = true;
                        }
                    }

                    if (isReach)
                    {
                        //moth.animator.SetBool("moth_Run", true);
                        moth.transform.parent.position += new Vector3(moth.statOfUnit.unitStats[7].movementRange, 0, 0) * moth.statOfUnit.unitStats[7].movementSpeed * Time.deltaTime;
                        moth.transform.parent.rotation = Quaternion.Euler(0, 0, 0); // Face Right
                        if (Vector3.Distance(moth.transform.parent.position, moth.originPosition.position) <= 2f)
                        {
                            isStart = true;
                            isReach = false;
                        }
                    }
                }
                else
                {
                    //moth.animator.SetBool("moth_Run", true);
                    Vector3 newPosition = Vector3.Lerp(moth.transform.parent.position, moth.originPosition.position, moth.statOfUnit.unitStats[7].movementSpeed * Time.deltaTime);
                    Vector3 directionOfOriginPoint = moth.originPosition.position - moth.transform.parent.position;
                    moth.transform.parent.position = newPosition;
                    moth.transform.parent.rotation = directionOfOriginPoint.x > 0 ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);

                    if (Vector3.Distance(moth.transform.parent.position, moth.originPosition.position) <= 2f)
                    {
                        moth.isGoBackToOriginPoint = false;
                    }
                }
            }
        }

        else if (moth.enemyStat.hp <= 0 && moth.isDead == false)
        {
            //moth.animator.SetBool("moth_Death", true);
            MonoBehaviour.Instantiate(moth.coin, moth.transform.position, Quaternion.identity);
            moth.enemyCount.DecreaseEnemyCount("Moth");
            MonoBehaviour.Destroy(moth.transform.parent.gameObject, 0.5f);
            moth.isDead = true;
        }
    }

    public override void OnTriggerEnter2D(MothController moth, Collider2D other)
    {

        if (other.CompareTag("Player") == true)
        {

            moth.targetPlayer = other.gameObject;
            moth.SwitchState(moth.chasingState);
        }
    }
    public override void OnTriggerExit2D(MothController moth, Collider2D other)
    {

    }
    public override void ExitState(MothController moth)
    {
        Debug.Log("1");
    }
}
