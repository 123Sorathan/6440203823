using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISoundController : MonoBehaviour
{

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private SoundDatabase soundDatabase;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ClickSound()
    {
        audioSource.clip = soundDatabase.clickSound;
        audioSource.Play();
    }

    public void WarningSound()
    {
        audioSource.clip = soundDatabase.warningSound;
        audioSource.Play();
    }

    public void MouseOnUISound()
    {
        audioSource.clip = soundDatabase.mouseOnUISound;
        audioSource.Play();
    }

    public void UpgradeSound()
    {
        audioSource.clip = soundDatabase.upgradeSound;
        audioSource.Play();
    }
}
