using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectKey : MonoBehaviour
{
    [SerializeField] private CheckWinConditionLevel1 checkWinCondition;

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
           checkWinCondition.isKeyCollect = true;
           Destroy(gameObject);
        }
    }
}
