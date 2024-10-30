using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
   [SerializeField] private CheckWinConditionLevel1 checkWinCondition;
   [SerializeField] private PlayerStat Dead;
    private void Update()
    {
        if(checkWinCondition.isWin == true && checkWinCondition.Win_UIGroup.alpha == 1)
        {
            Time.timeScale = 0;
        }
        if(checkWinCondition.isWin == false)
        {
            Time.timeScale = 1;
        }
    }

    public void Pause(){
        Time.timeScale = 0;
    }

    public void Continue(){
         Time.timeScale = 1;
    }
}
