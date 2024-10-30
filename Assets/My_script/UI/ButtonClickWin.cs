using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickWin : MonoBehaviour
{
    [SerializeField] private CheckWinConditionLevel1 Win_UI;
    public Button Click;
    public GameObject objectToShow; // วัตถุที่เราต้องการให้เกิดการเปลี่ยนแปลงเมื่อกดปุ่ม

    void Start()
    {
        // เพิ่มฟังก์ชันที่ต้องการทำเมื่อมีการคลิกปุ่ม
        Click.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        objectToShow.SetActive(true);
        Win_UI.Win_UIGroup.alpha = 0f;
        Win_UI.Win_UIGroup.alpha = 0f;
    }
}
