using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endcredit : MonoBehaviour
{
    //[SerializeField] private GameObject isCheckwinUI;
    [SerializeField] private CheckWinConditionLevel1 CheckWin;

    private void Update()
    {
        CheckWin_Active();
    }

    private void CheckWin_Active()
    {
        if(CheckWin.isShowingWinUI == true)
        {
            StartCoroutine(WinUI_isTrue());
            //Debug.Log("เปิดและปิด");
        }
    }

    IEnumerator WinUI_isTrue()
    {
        yield return new WaitForSeconds(1f); // รอ 1 วินาทีหรือตามต้องการ

        while (CheckWin.Win_UIGroup.alpha > 1f)
        {
            CheckWin.Win_UIGroup.alpha -= Time.deltaTime * 1f;
            yield return null;
        }
        CheckWin.Win_UIGroup.alpha = 0f;
    }
}
