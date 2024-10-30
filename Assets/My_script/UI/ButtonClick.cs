using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    [SerializeField] private PlayerStat stat;
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
        stat.deathUIGroup.alpha = 0f;
        stat.LoseScene.alpha = 0f;
    }
}
