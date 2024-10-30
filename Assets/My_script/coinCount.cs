using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCount : MonoBehaviour
{
    // [SerializeField] int maxCoinCount = 10; 
    [SerializeField] TextMeshProUGUI coinText; 
    // [SerializeField] GameObject panelToShow;
    // public Camera playerCamera;
    public int coinCount;

    private void Awake() {

        /*if (PlayerPrefs.GetInt("Save Coin") != null) 
        {
            coinCount = PlayerPrefs.GetInt("Save Coin");
        }*/

        if (PlayerPrefs.HasKey("Save Coin"))
        {
            coinCount = PlayerPrefs.GetInt("Save Coin");
        }
        else
        {    
            coinCount = 0;
            PlayerPrefs.SetInt("Save Coin", coinCount);
            PlayerPrefs.Save();
        }
    }

    void Update()
    {
        UpdateCoinText();
    }


    void UpdateCoinText()
    {
        // coinText.text = "Coins: " + coinCount.ToString();
        coinText.text = coinCount.ToString();
    }

}
