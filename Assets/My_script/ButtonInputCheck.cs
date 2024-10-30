using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInputCheck : MonoBehaviour
{
    void Update()
    {
        // ตรวจสอบการกดปุ่ม "Jump" จาก Input Manager
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("ปุ่ม Jump ถูกกดแล้ว");
        }

        // ตรวจสอบการปล่อยปุ่ม "Jump" จาก Input Manager
        if (Input.GetButtonUp("Jump"))
        {
            Debug.Log("ปุ่ม Jump ถูกปล่อยแล้ว");
        }

        // ตรวจสอบการกดปุ่ม "Fire1" จาก Input Manager
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("ปุ่ม Fire1 ถูกกดแล้ว");
        }
    }
}