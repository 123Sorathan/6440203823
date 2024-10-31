using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedIconController : MonoBehaviour
{
    [SerializeField] private GameObject lockedIconLevel2;
    [SerializeField] private GameObject lockedIconLevel3;
    [SerializeField] private GameObject lockedIconLevel4;

    private void Start()
    {
        CheckLevel2();
        CheckLevel3();
        CheckLevel4();
    }

    private void CheckLevel2()
    {
        if (PlayerPrefs.HasKey("Level_1"))
        {
            lockedIconLevel2.SetActive(false);
        }
        else
        {
            lockedIconLevel2.SetActive(true);
        }
    }

    private void CheckLevel3()
    {
        if (PlayerPrefs.HasKey("Level_2"))
        {
            lockedIconLevel3.SetActive(false);
        }
        else
        {
            lockedIconLevel3.SetActive(true);
        }
    }

    private void CheckLevel4()
    {
        if (PlayerPrefs.HasKey("Level_3"))
        {
            lockedIconLevel4.SetActive(false);
        }
        else
        {
            lockedIconLevel4.SetActive(true);
        }
    }
}
