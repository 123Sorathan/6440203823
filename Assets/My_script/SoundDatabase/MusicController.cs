using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    private enum selectedLevel { mainMenu,level1, level2, level3, level4};
    [SerializeField] private selectedLevel levelSelected;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private SoundDatabase soundDatabase;
    private void Start()
    {
       audioSource = GetComponent<AudioSource>();
       StartPlayMainMusic(); 
    }

    public void StartPlayMainMusic() // work with UI and automate
    {
        audioSource.volume = 1; //default volume

        if (levelSelected.ToString() == "mainMenu") 
        {
            audioSource.clip = soundDatabase.mainMenuMusic;
            audioSource.Play();
            audioSource.loop = true;
        }

        else if(levelSelected.ToString() == "level1")
        {
            audioSource.clip = soundDatabase.level1Music;
            audioSource.Play();
            audioSource.loop = true;
        }

        else if (levelSelected.ToString() == "level2")
        {
            audioSource.clip = soundDatabase.level2Music;
            audioSource.Play();
            audioSource.loop = true;
        }
        else if (levelSelected.ToString() == "level3")
        {
            audioSource.clip = soundDatabase.level3Music;
            audioSource.Play();
            audioSource.loop = true;
        }

        else if (levelSelected.ToString() == "level4")
        {
            audioSource.clip = soundDatabase.level4Music;
            audioSource.Play();
            audioSource.loop = true;
        }
    }

    public void ChangeToTemporalMusic(string musicName)
    {
        if(musicName == "winMusic")
        {
            audioSource.clip = soundDatabase.winMusic;
            audioSource.volume = 0.4f;
            audioSource.Play();
            audioSource.loop = false;
        }

        else if(musicName == "loseMusic")
        {
            audioSource.clip = soundDatabase.loseMusic;
            audioSource.volume = 0.4f;
            audioSource.Play();
            audioSource.loop = false;
        }

        else if(musicName == "endCreditMusic")
        {
            audioSource.clip = soundDatabase.endCreditMusic;
            audioSource.Play();
            audioSource.loop = false;
        }
    }
}
