using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    // �ѧ��ѹ�������¡������͡������͡
    public void QuitApplication()
    {
        // ���������� Editor �������
#if UNITY_EDITOR
        // ����Ѻ�Դ� Editor
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // �Դ�ͻ���प������ͷӧҹ��ԧ
            Application.Quit();
#endif
    }
}
