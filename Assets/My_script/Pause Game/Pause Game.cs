using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
   [SerializeField] private CheckWinConditionLevel1 checkWinCondition;
   [SerializeField] private PlayerStat Dead;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private BoxCollider2D playerCollider;

    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        if (checkWinCondition.isWin == true)
        {
            playerController.enabled = false;
            playerController.tag = "Untagged";
            playerController.transform.position = checkWinCondition.transform.position;
           // playerCollider.enabled = false;
            
            
        }
        if (checkWinCondition.isWin == false)
        {
            Continue();
        }
    }

    public void Pause(){
        Time.timeScale = 0;
    }

    public void Continue(){
         Time.timeScale = 1;
    }
}
