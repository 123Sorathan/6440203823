using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    // �ѧ��ѹ�������¡������͡����������������
    public void RestartCurrentScene()
    {
        // ��Ŵ Scene �Ѩ�غѹ����
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
