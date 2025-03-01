using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    // ฟังก์ชันนี้จะเรียกใช้เมื่อกดปุ่มออก
    public void QuitApplication()
    {
        // เช็คว่าอยู่ใน Editor หรือไม่
#if UNITY_EDITOR
        // สำหรับปิดใน Editor
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // ปิดแอปพลิเคชั่นเมื่อทำงานจริง
            Application.Quit();
#endif
    }
}
