using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Store : MonoBehaviour
{
    public CoinCount coin;
    public HP_Player HpPlayer;
    public PlayerStat stat;
    public Button Up_Demage;
    public Button Up_LP;
    public Button Up_Defense;
    public Button Up_AirJumps;
    public Button Up_Movment;

    private bool isUpDamageBuy;
    private bool isUpLpBuy;
    private bool isUpDefenseBuy;
    private bool isUpAirJumpsBuy;
    private bool isUpMovmentBuy;
    public PlayerController playerDamage;

    [SerializeField] private TextMeshProUGUI AddDamage;
    [SerializeField] private TextMeshProUGUI AddDefense;

    // Start is called before the first frame update
    void Start()
    {
        
        Up_Demage.enabled = false;
        Up_LP.enabled = false;
        Up_Defense.enabled = false;
        Up_AirJumps.enabled = false;
        Up_Movment.enabled = false; 

    }

    // Update is called once per frame
    void Update()
    {
        if(coin.coinCount >= 3)
        {
           Up_Demage.enabled = true;
           
        }
        if(coin.coinCount >= 4)
        {
            Up_LP.enabled = true;
            Up_Defense.enabled = true;
        }
        if(coin.coinCount >= 5)
        {
            Up_AirJumps.enabled = true;
            Up_Movment.enabled = true;
        }
    }

    public void IsUpAttackButtonclicked()
    {

        if(isUpDamageBuy == false)
        {
            isUpDamageBuy = true;
            Up_Demage.enabled = false;
            Debug.Log("ซื้อแล้ว");
            coin.coinCount = coin.coinCount - 3;
            playerDamage.damage = 5;

            showUI_TextDemage();

            //IsUpLPButtonclicked();
            //IsUpAirJumpsButtonclicked();
            //IsUpMovButtonclicked();
        }
    }
    public void IsUpLPButtonclicked()
    {
        if(isUpLpBuy == false)
        {
            isUpLpBuy = true;
            Up_LP.enabled = false;
            Debug.Log("ซื้อแล้ว");
            coin.coinCount = coin.coinCount - 8;
            
            if(stat.countdeath <= 5)
            {
                stat.countdeath++;
                PlayerPrefs.SetInt("Savecountdeath", stat.countdeath);
                PlayerPrefs.Save();
            }
            else
            {
                Debug.Log("Nooooooo");
            }
        }
    }

    public void IsUpDefenseButtonclicked()
    {

        if (isUpDefenseBuy == false)
        {
            isUpDefenseBuy = true;
            Up_Defense.enabled = false;
            Debug.Log("ซื้อแล้ว");
            coin.coinCount = coin.coinCount - 3;
            HpPlayer.armor = 3;

            showUI_TextDefense();
        }
    }

    public void IsUpAirJumpsButtonclicked()
    {

        if (isUpAirJumpsBuy == false)
        {
            isUpAirJumpsBuy = true;
            Up_AirJumps.enabled = false;
            Debug.Log("ซื้อแล้วกระโดดแล้ว");
            coin.coinCount = coin.coinCount - 5;
            playerDamage.maxAirJumps = 2;

        }
    }

    public void IsUpMovButtonclicked()
    {

        if (isUpMovmentBuy == false)
        {
            isUpMovmentBuy = true;
            Up_Movment.enabled = false;
            Debug.Log("ซื้อแล้ววิ่งไวแล้ว");
            coin.coinCount = coin.coinCount - 5;
            playerDamage.walkSpeed = 10;

        }
    }







    public void showUI_TextDemage(){
        AddDamage.text = $"{playerDamage.damage}";
    }
    public void showUI_TextDefense() { 
        AddDefense.text = $"{HpPlayer.armor}";
    }
}
