using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    [SerializeField] private PlayerStat stat;
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
        stat.deathUIGroup.alpha = 0f;
        stat.LoseScene.alpha = 0f;
    }
}
