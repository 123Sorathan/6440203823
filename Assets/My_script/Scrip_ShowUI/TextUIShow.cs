using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextUI : MonoBehaviour
{
    
    [Header("Scrip")]
    public HP_Player ShowHp;
    public PlayerController py;
    public CoinCount Coint;
    public CheckWinConditionLevel1 Key;

    [Header("Text Show Ui")]
    [SerializeField] private TextMeshProUGUI showText_hp;          // เลือด
    [SerializeField] private TextMeshProUGUI show_damage;          // พลังโจมตี
    [SerializeField] private TextMeshProUGUI showText_Armor;       // เกาะป้องกัน
    [SerializeField] private TextMeshProUGUI showText_key;         // กุญแจ
    // [SerializeField] private TextMeshProUGUI showText_Enemy;

    void Start()
    {
        
    }

    void Update()
    {
        showText_hp.text = $"{ShowHp.currentHealth}";
        show_damage.text = $"{py.damage}";
        showText_Armor.text = $"{ShowHp.armor}";
        UI_isKey();
    }

    private void UI_isKey()
    {
        if (Key.isKeyCollect)
        {
            showText_key.text = "<color=#00FF00>YES</color>"; // สีเขียว 
        }
        else
        {
            showText_key.text = "<color=#FF0000>NO</color>"; // สีแดง 
        }
    }

}
