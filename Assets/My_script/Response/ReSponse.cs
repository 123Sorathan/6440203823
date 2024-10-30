using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReSponse : MonoBehaviour
{
    bool isCountEnemyFinish;
    Dictionary<GameObject, Transform> enemyOriginPosition = new();
    [SerializeField] private EnemyCount enemyCount;

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyCount.sumOfEnemy > 0 && isCountEnemyFinish == false) 
        {
            isCountEnemyFinish = true;
            //Add Dict
            foreach(GameObject enemy in enemyCount.shadowSlimeCount)
            {
                enemyOriginPosition.Add(enemy, enemy.transform);
            }
            //foreach (GameObject enemy in enemyCount.shadowSlimeCount)
            //{
            //    enemyOriginPosition.Add(enemy, enemy.transform);
            //}
        }
    }

    public void BringAllToOriginPosition(GameObject enemyType, Transform enemyPosition)
    {
        enemyType.transform.position = enemyPosition.position; 
    }

    public void RemoveDictionary(GameObject enemyType)
    {
        if(enemyOriginPosition.ContainsKey(enemyType))
        enemyOriginPosition.Remove(enemyType);
    }
}
