using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickWin : MonoBehaviour
{
    [SerializeField] private CheckWinConditionLevel1 Win_UI;
    public Button Click;
    public GameObject objectToShow; // �ѵ�ط����ҵ�ͧ�������Դ�������¹�ŧ����͡�����

    void Start()
    {
        // �����ѧ��ѹ����ͧ��÷�������ա�ä�ԡ����
        Click.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        objectToShow.SetActive(true);
        Win_UI.Win_UIGroup.alpha = 0f;
        Win_UI.Win_UIGroup.alpha = 0f;
    }
}
