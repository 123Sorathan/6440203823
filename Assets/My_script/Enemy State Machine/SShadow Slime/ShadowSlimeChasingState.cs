using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShadowSlimeChasingState : ShadowSlimeBaseState
{
    Rigidbody2D rb;
    float speed = 10;

    public override void EnterState(ShadowSlimeController shadowSlime){
        rb = shadowSlime.gameObject.transform.parent.GetComponent<Rigidbody2D>();
    }
    public override void UpdateState(ShadowSlimeController shadowSlime){

        if(shadowSlime.isHit == false)
        {
            MonoBehaviour.Destroy(shadowSlime.currentHitEffect);
        }

        if(shadowSlime.isHit == true) {

            shadowSlime.currentHitEffect = MonoBehaviour.Instantiate(shadowSlime.HitEffect, shadowSlime.transform.position, Quaternion.identity);
            //hitText.GetComponent<TextMesh>().text = "Hit!";
            shadowSlime.StartKnockBack();
        }
        else
        {
         float distance = Mathf.Abs(shadowSlime.transform.parent.position.x - shadowSlime.transform.parent.position.x);
         Vector2 direction = new Vector2(shadowSlime.targetPlayer.transform.position.x - shadowSlime.transform.parent.position.x, 0).normalized;
         Vector2 newPosition = new Vector2(rb.position.x + direction.x * speed * Time.deltaTime, rb.position.y);
         shadowSlime.gameObject.transform.parent.position = newPosition;
        }

        Vector3 directionOfPlayer = shadowSlime.targetPlayer.transform.position - shadowSlime.transform.parent.position;
        if(directionOfPlayer.x > 0)
        {
          //Face Righ
          shadowSlime.transform.parent.rotation = Quaternion.Euler(0,0,0);//Face RIght
        }
        else if(directionOfPlayer.x < 0){
          //Face Left
          shadowSlime.transform.parent.rotation = Quaternion.Euler(0,180,0);//Face Left
        }




         // SWitch to Attack State
         if(Vector3.Distance(shadowSlime.transform.parent.position, shadowSlime.targetPlayer.transform.position) <= shadowSlime.statOfUnit.unitStats[0].attackRange
            && shadowSlime.isHit == false)
            {
               //Stop moving
               shadowSlime.transform.parent.position = shadowSlime.transform.parent.position;
               //Switch state
               shadowSlime.SwitchState(shadowSlime.attackState);
         }
         if(shadowSlime.enemyStat.hp <= 0){

             MonoBehaviour.Destroy(shadowSlime.currentHitEffect);
             MonoBehaviour.Instantiate(shadowSlime.coin, shadowSlime.transform.position, Quaternion.identity);
             shadowSlime.enemyCount.DecreaseEnemyCount("ShadowSlime");
             MonoBehaviour.Destroy(shadowSlime.transform.parent.gameObject);
         }
    }
    public override void OnTriggerEnter2D(ShadowSlimeController shadowSlime, Collider2D other){
        Debug.Log("1");
    }
     public override void OnTriggerExit2D(ShadowSlimeController shadowSlime, Collider2D other){
        if(other.CompareTag("Player")){
            shadowSlime.isGoBackToOriginPoint = true;
            shadowSlime.SwitchState(shadowSlime.patrolState);
        }
    }
    public override void ExitState(ShadowSlimeController shadowSlime){
        Debug.Log("1");
    }
}
