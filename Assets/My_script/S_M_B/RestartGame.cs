using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    // ฟังก์ชันนี้จะเรียกใช้เมื่อกดปุ่มเริ่มเกมใหม่
    public void RestartCurrentScene()
    {
        // โหลด Scene ปัจจุบันใหม่
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
