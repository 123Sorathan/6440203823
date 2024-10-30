using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonController : MonoBehaviour
{
    public Button ButtonDown; // ���������
    public Button ButtonOff;
    [SerializeField] private GameObject panelUI; // UI ἧ����ͧ�Դ/�Դ
    public Animator anim_ButtonUI_stack_upgrade; // ����� Animator
    

    void Start()
    {
        anim_ButtonUI_stack_upgrade = ButtonDown.GetComponent<Animator>(); // �Ѻ Animator �ͧ����
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
