using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    public int hp;
    public int attackPower;
    public StatOfUnit statOfUnit;

    private void Start(){
        if(gameObject.CompareTag("ShadowSlime")){
            hp = statOfUnit.unitStats[0].hp;
            attackPower = statOfUnit.unitStats[0].attackPower;
        }
        if (gameObject.CompareTag("The Ghost"))
        {
            hp = statOfUnit.unitStats[1].hp;
            attackPower = statOfUnit.unitStats[1].attackPower;
        }
        if (gameObject.CompareTag("Spitter"))
        {
            hp = statOfUnit.unitStats[2].hp;
            attackPower = statOfUnit.unitStats[2].attackPower;
        }
        if (gameObject.CompareTag("Giant Worm"))
        {
            hp = statOfUnit.unitStats[4].hp;
            attackPower = statOfUnit.unitStats[4].attackPower;
        }
        if (gameObject.CompareTag("Beetle"))
        {
            hp = statOfUnit.unitStats[5].hp;
            attackPower = statOfUnit.unitStats[5].attackPower;
        }
        if (gameObject.CompareTag("Skeleton"))
        {
            hp = statOfUnit.unitStats[6].hp;
            attackPower = statOfUnit.unitStats[6].attackPower;
        }
        if (gameObject.CompareTag("Moth"))
        {
            hp = statOfUnit.unitStats[7].hp;
            attackPower = statOfUnit.unitStats[7].attackPower;
        }
        if (gameObject.CompareTag("Mystical sword"))
        {
            hp = statOfUnit.unitStats[8].hp;
            attackPower = statOfUnit.unitStats[8].attackPower;
        }
        if (gameObject.CompareTag("Firefly"))
        {
            hp = statOfUnit.unitStats[9].hp;
            attackPower = statOfUnit.unitStats[9].attackPower;
        }
    }
}
