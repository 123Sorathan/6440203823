using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonPatrolState : SkeletonBaseState
{
    bool isStart;
    bool isReach;
    Vector3 destination1;

    public override void EnterState(SkeletonController skeleton)
    {
        isStart = true;
        destination1 = skeleton.originPosition.position + new Vector3(-(skeleton.statOfUnit.unitStats[6].movementRange), 0, 0);

        skeleton.rd.totalForce = Vector2.zero;
        skeleton.rd.velocity = Vector2.zero;

        //skeleton.animator.SetBool("isPatrolling", true);
    }

    public override void UpdateState(SkeletonController skeleton)
    {
        if (skeleton.isHit == true)
        {
            skeleton.StartKnockBack();
        }
        else
        {
            if (!skeleton.isGoBackToOriginPoint)
            {
                if (isStart)
                {
                    skeleton.transform.parent.position += new Vector3(-(skeleton.statOfUnit.unitStats[6].movementRange), 0, 0) * skeleton.statOfUnit.unitStats[6].movementSpeed * Time.deltaTime;
                    skeleton.transform.parent.rotation = Quaternion.Euler(0, 180, 0); // Face Left
                    if (Vector3.Distance(skeleton.transform.parent.position, destination1) <= 2f)
                    {
                        isStart = false;
                        isReach = true;
                    }
                }

                if (isReach)
                {
                    skeleton.transform.parent.position += new Vector3(skeleton.statOfUnit.unitStats[6].movementRange, 0, 0) * skeleton.statOfUnit.unitStats[6].movementSpeed * Time.deltaTime;
                    skeleton.transform.parent.rotation = Quaternion.Euler(0, 0, 0); // Face Right
                    if (Vector3.Distance(skeleton.transform.parent.position, skeleton.originPosition.position) <= 2f)
                    {
                        isStart = true;
                        isReach = false;
                    }
                }
            }
            else
            {
                Vector3 newPosition = Vector3.Lerp(skeleton.transform.parent.position, skeleton.originPosition.position, skeleton.statOfUnit.unitStats[6].movementSpeed * Time.deltaTime);
                Vector3 directionOfOriginPoint = skeleton.originPosition.position - skeleton.transform.parent.position;
                skeleton.transform.parent.position = newPosition;
                skeleton.transform.parent.rotation = directionOfOriginPoint.x > 0 ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);

                if (Vector3.Distance(skeleton.transform.parent.position, skeleton.originPosition.position) <= 2f)
                {
                    skeleton.isGoBackToOriginPoint = false;
                }
            }
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
        if (other.CompareTag("Player") == true)
        {
            skeleton.targetPlayer = other.gameObject;
            skeleton.SwitchState(skeleton.chasingState);
        }
    }

    public override void OnTriggerExit2D(SkeletonController skeleton, Collider2D other)
    {
        // Optional: Add exit behavior if needed
        Debug.Log("");
    }

    public override void ExitState(SkeletonController skeleton)
    {
        // Stop patrol animation
        //skeleton.animator.SetBool("isPatrolling", false);
        Debug.Log("");
    }
}