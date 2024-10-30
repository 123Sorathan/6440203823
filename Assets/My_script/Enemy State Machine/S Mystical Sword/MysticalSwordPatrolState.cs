using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysticalSwordPatrolState: MysticalSwordBaseState
{
    bool isStart;
    bool isReach;
    Vector3 destination1;

    public override void EnterState(MysticalSwordController mystical_Sword)
    {
        isStart = true;
        destination1 = mystical_Sword.originPosition.position + new Vector3(-(mystical_Sword.statOfUnit.unitStats[8].movementRange), 0, 0);

        mystical_Sword.rd.totalForce = Vector2.zero;
        mystical_Sword.rd.velocity = Vector2.zero;
    }

    public override void UpdateState(MysticalSwordController mystical_Sword)
    {
        if (mystical_Sword.enemyStat.hp > 0) // not die
        {
            if (mystical_Sword.isHit == true)
            {
                //mystical_Sword.animator.SetTrigger("Hit");
                mystical_Sword.StartKnockBack();
            }
            else
            {
                if (mystical_Sword.isGoBackToOriginPoint == false)
                {
                    if (isStart)
                    {
                        //mystical_Sword.animator.SetBool("mystical_Sword_Run", true);
                        mystical_Sword.transform.parent.position += new Vector3(-(mystical_Sword.statOfUnit.unitStats[8].movementRange), 0, 0) * mystical_Sword.statOfUnit.unitStats[8].movementSpeed * Time.deltaTime;
                        mystical_Sword.transform.parent.rotation = Quaternion.Euler(0, 180, 0); // Face Left
                        if (Vector3.Distance(mystical_Sword.transform.parent.position, destination1) <= 2f)
                        {
                            isStart = false;
                            isReach = true;
                        }
                    }

                    if (isReach)
                    {
                        //mystical_Sword.animator.SetBool("mystical_Sword_Run", true);
                        mystical_Sword.transform.parent.position += new Vector3(mystical_Sword.statOfUnit.unitStats[8].movementRange, 0, 0) * mystical_Sword.statOfUnit.unitStats[8].movementSpeed * Time.deltaTime;
                        mystical_Sword.transform.parent.rotation = Quaternion.Euler(0, 0, 0); // Face Right
                        if (Vector3.Distance(mystical_Sword.transform.parent.position, mystical_Sword.originPosition.position) <= 2f)
                        {
                            isStart = true;
                            isReach = false;
                        }
                    }
                }
                else
                {
                    //mystical_Sword.animator.SetBool("mystical_Sword_Run", true);
                    Vector3 newPosition = Vector3.Lerp(mystical_Sword.transform.parent.position, mystical_Sword.originPosition.position, mystical_Sword.statOfUnit.unitStats[8].movementSpeed * Time.deltaTime);
                    Vector3 directionOfOriginPoint = mystical_Sword.originPosition.position - mystical_Sword.transform.parent.position;
                    mystical_Sword.transform.parent.position = newPosition;
                    mystical_Sword.transform.parent.rotation = directionOfOriginPoint.x > 0 ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);

                    if (Vector3.Distance(mystical_Sword.transform.parent.position, mystical_Sword.originPosition.position) <= 2f)
                    {
                        mystical_Sword.isGoBackToOriginPoint = false;
                    }
                }
            }
        }

        else if (mystical_Sword.enemyStat.hp <= 0 && mystical_Sword.isDead == false)
        {
            //mystical_Sword.animator.SetBool("mystical_Sword_Death", true);
            MonoBehaviour.Instantiate(mystical_Sword.coin, mystical_Sword.transform.position, Quaternion.identity);
            mystical_Sword.enemyCount.DecreaseEnemyCount("Mystical sword");
            MonoBehaviour.Destroy(mystical_Sword.transform.parent.gameObject, 0.5f);
            mystical_Sword.isDead = true;
        }
    }

    public override void OnTriggerEnter2D(MysticalSwordController mystical_Sword, Collider2D other)
    {

        if (other.CompareTag("Player") == true)
        {

            mystical_Sword.targetPlayer = other.gameObject;
            mystical_Sword.SwitchState(mystical_Sword.chasingState);
        }
    }
    public override void OnTriggerExit2D(MysticalSwordController mystical_Sword, Collider2D other)
    {

    }
    public override void ExitState(MysticalSwordController mystical_Sword)
    {
        Debug.Log("1");
    }
}
