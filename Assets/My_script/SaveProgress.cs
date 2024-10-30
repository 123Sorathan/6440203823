using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SaveProgress : MonoBehaviour // สคริปนี้จะทำการบันทึกจำนวนเหรียญหลังจากแพ้้
{
    public CoinCount currentCoin;
    public Button restartButton;
    public TextMeshProUGUI messageText; 


    private void Start()
    {
      
    }

    private void Update()
    {
        IsDesdSave();
    }

    private void IsDesdSave()
    {
        //if(Stat.isDead == false)
       /* {
            IsRestartButtonClicked();
        }*/
    }

    public void IsRestartButtonClicked(string sceneName)
    {
      //Save coin
      currentCoin.coinCount = 0;
      //PlayerPrefs.SetInt("Save Coin", currentCoin.coinCount);
      //PlayerPrefs.Save();
      //Reload Scene
      SceneManager.LoadScene(sceneName);
      //currentCoin.coinCount = PlayerPrefs.GetInt("Save Coin");
      //Debug.Log("Previous coin = " + PlayerPrefs.GetInt("Save Coin"));
    }

    public void DeleteSaveCoin()
    {
        PlayerPrefs.DeleteKey("Save Coin");
        PlayerPrefs.DeleteKey("Savecountdeath");
    }
}
