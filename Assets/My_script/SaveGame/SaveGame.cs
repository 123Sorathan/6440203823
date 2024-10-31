using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    public void SaveLevel(string levelName)
    {
        PlayerPrefs.SetInt(levelName, 1);
        PlayerPrefs.Save();
    }
}
