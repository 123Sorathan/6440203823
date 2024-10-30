using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    public void SaveLevel1()
    {
        PlayerPrefs.SetInt("Save Level 1", 1);
        PlayerPrefs.Save();
    }

    public void SaveLevel2()
    {
        PlayerPrefs.SetInt("Save Level 2", 1);
        PlayerPrefs.Save();
    }

    public void SaveLevel3()
    {
        PlayerPrefs.SetInt("Save Level 3", 1);
        PlayerPrefs.Save();
    }
}
