using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSlimePatrolState1: ShadowSlimeBaseState
{
    bool isStart;
    bool isReach;
    Vector3 destination1;
    bool isJumping;  // ���������Ѻ��Ǩ�ͺ��á��ⴴ
    public float jumpForce = 5f;  // �����ç�ͧ��á��ⴴ
    private Rigidbody2D rb;  // ����� Rigidbody2D ����Ѻ���ԡ��

    public LayerMask groundLayer;
    public override void EnterState(ShadowSlimeController shadowSlime){
        
        isStart = true;
        isJumping = false;  // ���������������ⴴ
        rb = shadowSlime.gameObject.GetComponentInParent<Rigidbody2D>();  // �֧ Rigidbody2D �ҡ ShadowSlime
        destination1 = shadowSlime.originPosition.transform.position + new Vector3(-(shadowSlime.statOfUnit.unitStats[0].movementRange), 0, 0);
     }
    public override void UpdateState(ShadowSlimeController shadowSlime){

        if(shadowSlime.isHit == true)
        {
            shadowSlime.StartKnockBack();
        }

        else 
        {

            if(shadowSlime.isGoBackToOriginPoint == false){

                if(isStart == true){
                    //shadowSlime.transform.parent.position += new Vector3(-(shadowSlime.statOfUnit.unitStats[0].movementRange), 0, 0) * shadowSlime.statOfUnit.unitStats[0].movementSpeed * Time.deltaTime;
                    MoveAndJump(shadowSlime, new Vector3(-(shadowSlime.statOfUnit.unitStats[0].movementRange), 0,0));  // ����͹��������Ѻ���ⴴ
                    shadowSlime.transform.parent.rotation = Quaternion.Euler(0,180,0);//Face Left
                    //Debug.Log("Go Left");
            
                    if(Vector3.Distance(shadowSlime.transform.parent.position, destination1) <= 2f && isStart == true){
                    isStart = false;
                    isReach = true;
                    //Debug.Log("Go Destination");
                    }
                }

                if(isReach == true){
                    //shadowSlime.transform.parent.position += new Vector3(shadowSlime.statOfUnit.unitStats[0].movementRange, 0, 0) * shadowSlime.statOfUnit.unitStats[0].movementSpeed * Time.deltaTime;
                    MoveAndJump(shadowSlime, new Vector3(shadowSlime.statOfUnit.unitStats[0].movementRange, 0,0));  // ����͹����������ⴴ仢��
                    shadowSlime.transform.parent.rotation = Quaternion.Euler(0,0,0);//Face RIght
                    //Debug.Log("Go Right");

                    if (Vector3.Distance(shadowSlime.transform.parent.position, shadowSlime.originPosition.position) <= 2f && isReach == true){
                    isStart = true;
                    isReach = false;
                    //Debug.Log("Go Back");
                    }
                }
            }
            else{
                // shadowSlime.transform.parent.position -= shadowSlime.originPosition.transform.position * shadowSlime.statOfUnit.unitStats[0].movementSpeed * Time.deltaTime;
                //Vector3 newPosition = Vector3.Lerp(shadowSlime.transform.parent.position, shadowSlime.originPosition.transform.position,shadowSlime.statOfUnit.unitStats[0].movementSpeed * Time.deltaTime);
                //Vector3 directionOfOriginPoint = shadowSlime.originPosition.transform.position - shadowSlime.transform.parent.position;
                //shadowSlime.transform.parent.position = newPosition;

                // �ӹǳ���˹�����
                Vector3 targetPosition = shadowSlime.originPosition.transform.position;
                Vector3 currentPosition = shadowSlime.transform.parent.position;
                float moveSpeed = shadowSlime.statOfUnit.unitStats[0].movementSpeed * Time.deltaTime;

                // �ӹǳ���˹�������¡�������
                Vector3 newPosition = Vector3.Lerp(currentPosition, targetPosition, moveSpeed);
                shadowSlime.transform.parent.position = newPosition;

                // ����͹��������Ѻ���ⴴ
                Vector3 moveDirection = targetPosition - currentPosition;
                MoveAndJump(shadowSlime, moveDirection);

                // ��ع����ѹ��ѧ��ȷҧ���١��ͧ
                Vector3 directionOfOriginPoint = targetPosition - shadowSlime.transform.parent.position;

                if (directionOfOriginPoint.x > 0)
                {
                    //Face Righ
                    shadowSlime.transform.parent.rotation = Quaternion.Euler(0,0,0);//Face RIght
                }

                else if(directionOfOriginPoint.x < 0){
                    //Face Left
                    shadowSlime.transform.parent.rotation = Quaternion.Euler(0,180,0);//Face Left
                }

                if(Vector3.Distance(shadowSlime.transform.parent.position, shadowSlime.originPosition.transform.position) <= 2f){
                    shadowSlime.isGoBackToOriginPoint = false;
                }
            }
        }

        if(shadowSlime.enemyStat.hp <= 0){
            MonoBehaviour.Instantiate(shadowSlime.coin, shadowSlime.transform.position, Quaternion.identity);
            shadowSlime.enemyCount.DecreaseEnemyCount("ShadowSlime");
            MonoBehaviour.Destroy(shadowSlime.transform.parent.gameObject);
        }
    }
    /*private void MoveAndJump(ShadowSlimeController shadowSlime, Vector3 direction)
    {
        shadowSlime.transform.parent.position += direction * shadowSlime.statOfUnit.unitStats[0].movementSpeed * Time.deltaTime;

        if (!isJumping)  // ��Ǩ�ͺ��ҡ��ѧ���躹��鹴Թ
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);  // �����ç���ⴴ�᡹ Y
            isJumping = true;
        }
    }*/
    private void MoveAndJump(ShadowSlimeController shadowSlime, Vector3 direction)
    {
        // �Ǻ����������͹���㹷�ȷҧ
        rb.velocity = new Vector2(direction.x * shadowSlime.statOfUnit.unitStats[0].movementSpeed, rb.velocity.y);

        if (!isJumping)  // ��Ǩ�ͺ��ҡ��ѧ���躹��鹴Թ
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);  // �����ç���ⴴ�᡹ Y
            isJumping = true;
        }
    }

    public override void OnTriggerEnter2D(ShadowSlimeController shadowSlime, Collider2D other){

        // ��Ǩ�ͺ��ҡ��ⴴŧ�֧����������
        if ((groundLayer & (1 << other.gameObject.layer)) != 0)
        {
            isJumping = false;
        }

        if (other.CompareTag("Player") == true){

            shadowSlime.targetPlayer = other.gameObject;
            shadowSlime.SwitchState(shadowSlime.chasingState);
        }
    }
     public override void OnTriggerExit2D(ShadowSlimeController shadowSlime, Collider2D other){
        Debug.Log("1");
    }
    public override void ExitState(ShadowSlimeController shadowSlime){
        Debug.Log("1");
    }

}
