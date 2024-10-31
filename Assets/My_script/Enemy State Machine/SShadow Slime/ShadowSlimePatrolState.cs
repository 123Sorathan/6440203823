using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSlimePatrolState : ShadowSlimeBaseState
{
    bool isStart;
    bool isReach;
    Vector3 destination1;

    public LayerMask groundLayer;

    public override void EnterState(ShadowSlimeController shadowSlime)
    {
        isStart = true;
        shadowSlime.isJumping = false;  // เริ่มต้นให้ไม่กระโดด
        destination1 = shadowSlime.originPosition.transform.position + new Vector3(-(shadowSlime.statOfUnit.unitStats[0].movementRange), 0, 0);
        shadowSlime.rb = shadowSlime.gameObject.GetComponentInParent<Rigidbody2D>();
        shadowSlime.jumpTimer = 0f; // ตั้งค่าตัวจับเวลาการกระโดด

        shadowSlime.enemySoundEffetController.enemyPatroSound();
    }

    public override void UpdateState(ShadowSlimeController shadowSlime)
    {
        if (shadowSlime.isHit == false)
        {
            MonoBehaviour.Destroy(shadowSlime.currentHitEffect);
        }

        if (shadowSlime.isHit == true)
        {
            shadowSlime.StartKnockBack();
        }
        else
        {
            shadowSlime.jumpTimer -= Time.deltaTime; // นับถอยหลังเวลาสำหรับการกระโดด
            

            if (shadowSlime.jumpTimer <= 0f) // ถ้าถึงเวลาที่จะกระโดดใหม่
            {
                if (shadowSlime.isGoBackToOriginPoint == false)
                {
                    Debug.Log("Distance " + Vector3.Distance(shadowSlime.transform.parent.position, destination1));
                    if (isStart == true)
                    {
                        MoveAndJump(shadowSlime, new Vector3(-(shadowSlime.statOfUnit.unitStats[0].movementRange), 0, 0));  // เคลื่อนที่พร้อมกับกระโดด
                        shadowSlime.transform.parent.rotation = Quaternion.Euler(0, 180, 0);  // หันหน้าไปทางซ้าย

                        if (Vector3.Distance(shadowSlime.transform.parent.position, destination1) <= 1f)
                        {
                            isStart = false;
                            isReach = true;
                        }
                    }
                    if (isReach == true)
                    {
                        MoveAndJump(shadowSlime, new Vector3(shadowSlime.statOfUnit.unitStats[0].movementRange, 0, 0));  // เคลื่อนที่พร้อมกระโดดไปขวา
                        shadowSlime.transform.parent.rotation = Quaternion.Euler(0, 0, 0);  // หันหน้าไปทางขวา

                        if (Vector3.Distance(shadowSlime.transform.parent.position, shadowSlime.originPosition.position) <= 1f)
                        {
                            isStart = true;
                            isReach = false;
                        }
                    }
                }

                shadowSlime.jumpTimer = shadowSlime.jumpCooldown;
            }

        }
        if (shadowSlime.isGoBackToOriginPoint == true)
        {
             //Vector3 newPosition = Vector3.Lerp(shadowSlime.transform.parent.position,shadowSlime.originPosition.transform.position,shadowSlime.statOfUnit.unitStats[0].movementSpeed * Time.deltaTime); 
             Vector3 directionOfOriginPoint = shadowSlime.originPosition.transform.position - shadowSlime.transform.parent.position;
            //shadowSlime.transform.parent.position = newPosition;

            shadowSlime.jumpTimer -= Time.deltaTime;

            if (shadowSlime.jumpTimer <= 0f)
            {
                if (directionOfOriginPoint.x > 0)
                {
                    shadowSlime.transform.parent.rotation = Quaternion.Euler(0, 0, 0);  // หันหน้าไปทางขวา
                    MoveAndJump(shadowSlime, new Vector3(shadowSlime.statOfUnit.unitStats[0].movementRange, 0, 0));         //Debug.Log("หยุดทำไหม");
                                                                                                                            //shadowSlime.isGoBackToOriginPoint = false;
                }
                else if (directionOfOriginPoint.x < 0)
                {
                    shadowSlime.transform.parent.rotation = Quaternion.Euler(0, 180, 0);  // หันหน้าไปทางซ้าย
                    MoveAndJump(shadowSlime, new Vector3(-(shadowSlime.statOfUnit.unitStats[0].movementRange), 0, 0));     //shadowSlime.isGoBackToOriginPoint = false;
                }

                // เมื่อถึงตำแหน่ง originPosition แล้ว ให้หยุดการเคลื่อนที่
                if (Vector3.Distance(shadowSlime.transform.parent.position, shadowSlime.originPosition.transform.position) <= 2f)
                {
                    shadowSlime.isGoBackToOriginPoint = false;
                    //Debug.Log("Reached Origin, stopping movement");
                }
                // รีเซ็ตตัวจับเวลาการกระโดด
                //Debug.Log("กลับไปจุดเริ่มต้น");
                shadowSlime.jumpTimer = shadowSlime.jumpCooldown;
            }
        }

        if (shadowSlime.enemyStat.hp <= 0)
        {
            MonoBehaviour.Destroy(shadowSlime.currentHitEffect);
            MonoBehaviour.Instantiate(shadowSlime.coin, shadowSlime.transform.position, Quaternion.identity);
            shadowSlime.enemyCount.DecreaseEnemyCount("ShadowSlime");
            MonoBehaviour.Destroy(shadowSlime.transform.parent.gameObject);
        }
    }

    private void MoveAndJump(ShadowSlimeController shadowSlime, Vector3 direction)
    {
        // เคลื่อนที่และกระโดดในแนวทิศทางที่กำหนด
        shadowSlime.rb.velocity = new Vector2(direction.x * shadowSlime.statOfUnit.unitStats[0].movementSpeed, shadowSlime.jumpForce);

        // เมื่อกระโดดแล้วตั้งค่าสถานะการกระโดด
        shadowSlime.isJumping = true;
    }

    public override void OnTriggerEnter2D(ShadowSlimeController shadowSlime, Collider2D other)
    {
        // ตรวจสอบว่ากระโดดลงถึงพื้นหรือไม่
        if ((groundLayer & (1 << other.gameObject.layer)) != 0)
        {
            shadowSlime.isJumping = false;  // เมื่อถึงพื้น ให้สามารถกระโดดใหม่ได้
        }

        if (other.CompareTag("Player") == true)
        {
            MonoBehaviour.Destroy(shadowSlime.currentHitEffect);
            shadowSlime.targetPlayer = other.gameObject;
            shadowSlime.SwitchState(shadowSlime.chasingState);
        }
    }

    public override void OnTriggerExit2D(ShadowSlimeController shadowSlime, Collider2D other)
    {
        Debug.Log("1");
    }

    public override void ExitState(ShadowSlimeController shadowSlime)
    {
        Debug.Log("1");
    }
}
