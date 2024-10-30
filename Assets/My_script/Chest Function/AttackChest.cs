using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackChest : MonoBehaviour
{
   // private Collider2D chestCollider;
   [SerializeField] private int chestHP; 
   [SerializeField] private LayerMask playerLayer;
   [SerializeField] private GameObject key;
   private bool isChestDestroy;
   
   private void Update() 
   {
      if(Input.GetMouseButtonDown(0))
      {
         DetectHitFromPlayer(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), new Vector2(gameObject.transform.localScale.x,gameObject.transform.localScale.y));
         Debug.Log("Chest Size : x = " + gameObject.transform.localScale.x + " y = " + gameObject.transform.localScale.y);
      }

      if(chestHP <= 0 & isChestDestroy == false)
      {
          key.SetActive(true);
          isChestDestroy = true;
          Destroy(gameObject);
      }
   }

   private void DetectHitFromPlayer(Vector2 chestPosition, Vector2 chestSize)
   {
        Collider2D chestCollider = Physics2D.OverlapBox(chestPosition, chestSize, 0, playerLayer);

        if(chestCollider != null)
        {
            chestHP = chestHP - 1;
        }
   }
}
