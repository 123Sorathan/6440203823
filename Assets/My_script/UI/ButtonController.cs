using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonController : MonoBehaviour
{
    public Button ButtonDown; // ปุ่มที่ใช้
    public Button ButtonOff;
    [SerializeField] private GameObject panelUI; // UI แผงที่ต้องเปิด/ปิด
    public Animator anim_ButtonUI_stack_upgrade; // ตัวแปร Animator
    

    void Start()
    {
        anim_ButtonUI_stack_upgrade = ButtonDown.GetComponent<Animator>(); // รับ Animator ของปุ่ม
        panelUI.SetActive(false);
    }

    void Update()
    {
      
    }
    public void IsButtonDown()
    {
        if (ButtonDown == true)
        {
            anim_ButtonUI_stack_upgrade.Play("Selected");
        }
    }

    public void ClickButton()
    {
        if (ButtonDown == true)
        {
            anim_ButtonUI_stack_upgrade.Rebind();
            anim_ButtonUI_stack_upgrade.Play("Normal");
        }
    }
}
