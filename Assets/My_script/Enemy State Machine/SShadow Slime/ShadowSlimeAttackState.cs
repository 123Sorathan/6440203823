using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSlimeAttackState : ShadowSlimeBaseState
{
    private float lastShotTime = 0.0f;
    public override void EnterState(ShadowSlimeController shadowSlime){
        Debug.Log("");
    }
    public override void UpdateState(ShadowSlimeController shadowSlime){

        if (shadowSlime.isHit == false)
        {
            MonoBehaviour.Destroy(shadowSlime.currentHitEffect);
        }

        if (Time.time > lastShotTime + shadowSlime.statOfUnit.unitStats[0].attackCoolDown){
            //shadowSlime.hpPlayer.currentHealth -= shadowSlime.statOfUnit.unitStats[0].attackPower;
            MonoBehaviour.Destroy(shadowSlime.currentHitEffect);
            shadowSlime.hpPlayer.TakeDamage(shadowSlime.statOfUnit.unitStats[0].attackPower);
            lastShotTime = Time.time;
        }

        if((Vector3.Distance(shadowSlime.targetPlayer.transform.position, shadowSlime.transform.parent.position) > shadowSlime.statOfUnit.unitStats[0].attackRange)
        || shadowSlime.isHit == true) {
           shadowSlime.SwitchState(shadowSlime.chasingState);
        }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     
        if(shadowSlime.enemyStat.hp <= 0){

            MonoBehaviour.Destroy(shadowSlime.currentHitEffect);
            MonoBehaviour.Instantiate(shadowSlime.coin, shadowSlime.transform.position, Quaternion.identity);
            shadowSlime.enemyCount.DecreaseEnemyCount("ShadowSlime");
            MonoBehaviour.Destroy(shadowSlime.transform.parent.gameObject);
        }

        if(shadowSlime.hpPlayer.currentHealth <= 0) 
        {
            MonoBehaviour.Destroy(shadowSlime.currentHitEffect);
            shadowSlime.isGoBackToOriginPoint = true;
            shadowSlime.SwitchState(shadowSlime.patrolState);
        }
    }
    public override void OnTriggerEnter2D(ShadowSlimeController shadowSlime, Collider2D other){
        Debug.Log("");
    }
    public override void OnTriggerExit2D(ShadowSlimeController shadowSlime, Collider2D other){
        if(other.CompareTag("Player"))
        {
            shadowSlime.isGoBackToOriginPoint = true;
            shadowSlime.SwitchState(shadowSlime.patrolState);
        }
    }
    public override void ExitState(ShadowSlimeController shadowSlime){
        Debug.Log("");
    }

    
}
