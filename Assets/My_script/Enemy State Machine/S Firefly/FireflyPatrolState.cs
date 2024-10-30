using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyPatrolState : FireflyBaseState
{
    bool isStart;
    bool isReach;
    Vector3 destination1;

    public override void EnterState(FireflyController firefly)
    {
        isStart = true;
        destination1 = firefly.originPosition.position + new Vector3(-(firefly.statOfUnit.unitStats[2].movementRange), 0, 0);

        firefly.rd.totalForce = Vector2.zero;
        firefly.rd.velocity = Vector2.zero;
    }

    public override void UpdateState(FireflyController firefly)
    {
        if (firefly.enemyStat.hp > 0) // not die
        {
            if (firefly.isHit == true)
            {
                firefly.animator.SetTrigger("Hit");
                firefly.StartKnockBack();
            }
            else
            {
                if (firefly.isGoBackToOriginPoint == false)
                {
                    if (isStart)
                    {
                        //firefly.animator.SetBool("firefly_Run", true);
                        firefly.transform.parent.position += new Vector3(-(firefly.statOfUnit.unitStats[2].movementRange), 0, 0) * firefly.statOfUnit.unitStats[2].movementSpeed * Time.deltaTime;
                        firefly.transform.parent.rotation = Quaternion.Euler(0, 180, 0); // Face Left
                        if (Vector3.Distance(firefly.transform.parent.position, destination1) <= 2f)
                        {
                            isStart = false;
                            isReach = true;
                        }
                    }

                    if (isReach)
                    {
                        //firefly.animator.SetBool("firefly_Run", true);
                        firefly.transform.parent.position += new Vector3(firefly.statOfUnit.unitStats[2].movementRange, 0, 0) * firefly.statOfUnit.unitStats[2].movementSpeed * Time.deltaTime;
                        firefly.transform.parent.rotation = Quaternion.Euler(0, 0, 0); // Face Right
                        if (Vector3.Distance(firefly.transform.parent.position, firefly.originPosition.position) <= 2f)
                        {
                            isStart = true;
                            isReach = false;
                        }
                    }
                }
                else
                {
                    //firefly.animator.SetBool("firefly_Run", true);
                    Vector3 newPosition = Vector3.Lerp(firefly.transform.parent.position, firefly.originPosition.position, firefly.statOfUnit.unitStats[2].movementSpeed * Time.deltaTime);
                    Vector3 directionOfOriginPoint = firefly.originPosition.position - firefly.transform.parent.position;
                    firefly.transform.parent.position = newPosition;
                    firefly.transform.parent.rotation = directionOfOriginPoint.x > 0 ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);

                    if (Vector3.Distance(firefly.transform.parent.position, firefly.originPosition.position) <= 2f)
                    {
                        firefly.isGoBackToOriginPoint = false;
                    }
                }
            }
        }

        else if (firefly.enemyStat.hp <= 0 && firefly.isDead == false)
        {
            //firefly.animator.SetBool("firefly_Death", true);
            MonoBehaviour.Instantiate(firefly.coin, firefly.transform.position, Quaternion.identity);
            firefly.enemyCount.DecreaseEnemyCount("Firefly");
            MonoBehaviour.Destroy(firefly.transform.parent.gameObject, 0.5f);
            firefly.isDead = true;
        }
    }

    public override void OnTriggerEnter2D(FireflyController firefly, Collider2D other)
    {

        if (other.CompareTag("Player") == true)
        {

            firefly.targetPlayer = other.gameObject;
            firefly.SwitchState(firefly.chasingState);
        }
    }
    public override void OnTriggerExit2D(FireflyController firefly, Collider2D other)
    {

    }
    public override void ExitState(FireflyController firefly)
    {
        Debug.Log("1");
    }

}
