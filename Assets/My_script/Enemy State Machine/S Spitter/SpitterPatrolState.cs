using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitterPatrolState : SpitterBaseState
{
    bool isStart;
    bool isReach;
    Vector3 destination1;

    public override void EnterState(SpitterController spitter)
    {
        isStart = true;
        destination1 = spitter.originPosition.position + new Vector3(-(spitter.statOfUnit.unitStats[3].movementRange), 0, 0);

        spitter.rd.totalForce = Vector2.zero;
        spitter.rd.velocity = Vector2.zero;

        //spitter.enemySoundEffetController.enemyPatroSound();
    }

    public override void UpdateState(SpitterController spitter)
    {
        if(spitter.enemyStat.hp > 0) // not die
        {
            if (spitter.isHit == true)
            {
                spitter.animator.SetTrigger("Hit");
                spitter.StartKnockBack();
            }
            else
            {
                if (spitter.isGoBackToOriginPoint == false)
                {
                    if (isStart)
                    {
                        spitter.animator.SetBool("Spitter_Run", true);
                        spitter.transform.parent.position += new Vector3(-(spitter.statOfUnit.unitStats[3].movementRange), 0, 0) * spitter.statOfUnit.unitStats[4].movementSpeed * Time.deltaTime;
                        spitter.transform.parent.rotation = Quaternion.Euler(0, 180, 0); // Face Left
                        if (Vector3.Distance(spitter.transform.parent.position, destination1) <= 2f)
                        {
                            isStart = false;
                            isReach = true;
                        }
                    }

                    if (isReach)
                    {
                        spitter.animator.SetBool("Spitter_Run", true);
                        spitter.transform.parent.position += new Vector3(spitter.statOfUnit.unitStats[3].movementRange, 0, 0) * spitter.statOfUnit.unitStats[2].movementSpeed * Time.deltaTime;
                        spitter.transform.parent.rotation = Quaternion.Euler(0, 0, 0); // Face Right
                        if (Vector3.Distance(spitter.transform.parent.position, spitter.originPosition.position) <= 2f)
                        {
                            isStart = true;
                            isReach = false;
                        }
                    }
                }
                else
                {
                    spitter.animator.SetBool("Spitter_Run", true);
                    Vector3 newPosition = Vector3.Lerp(spitter.transform.parent.position, spitter.originPosition.position, spitter.statOfUnit.unitStats[3].movementSpeed * Time.deltaTime);
                    Vector3 directionOfOriginPoint = spitter.originPosition.position - spitter.transform.parent.position;
                    spitter.transform.parent.position = newPosition;
                    spitter.transform.parent.rotation = directionOfOriginPoint.x > 0 ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);

                    if (Vector3.Distance(spitter.transform.parent.position, spitter.originPosition.position) <= 2f)
                    {
                        spitter.isGoBackToOriginPoint = false;
                    }
                }
            }
        }
       
        else if (spitter.enemyStat.hp <= 0 && spitter.isDead == false)
        {
            spitter.animator.SetBool("Spitter_Death", true);
            MonoBehaviour.Instantiate(spitter.coin, spitter.transform.position, Quaternion.identity);
            spitter.enemyCount.DecreaseEnemyCount("Spitter");
            MonoBehaviour.Destroy(spitter.transform.parent.gameObject, 3f);
            spitter.isDead = true;
        }
    }

    public override void OnTriggerEnter2D(SpitterController spitter, Collider2D other)
    {

        if (other.CompareTag("Player") == true)
        {

            spitter.targetPlayer = other.gameObject;
            spitter.SwitchState(spitter.chasingState);
        }
    }
    public override void OnTriggerExit2D(SpitterController spitter, Collider2D other)
    {
        
    }
    public override void ExitState(SpitterController spitter)
    {
        Debug.Log("1");
    }

}
