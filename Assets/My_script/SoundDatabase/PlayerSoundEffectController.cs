using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundEffectController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private SoundDatabase soundDatabase;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void StartPlayAttackSound()
    {
        audioSource.clip = soundDatabase.attackSound;
        audioSource.Play();
    }

    public void StartPlayJumpSound()
    {
        audioSource.clip = soundDatabase.jumpSound;
        audioSource.Play();
    }

    public void StartPlayCollectCoinSound()
    {
        audioSource.clip = soundDatabase.collectCoinSound;
        audioSource.Play();
    }

    public void StartPlayEnemyHitPlayerSound()
    {
        audioSource.clip = soundDatabase.enemyHitplayerSound;
        audioSource.Play();
    }

    public void StartPlayDeadSound()
    {
        audioSource.clip = soundDatabase.deadSound;
        audioSource.Play();
    }

    public void StartPlayEnterDoorSound()
    {
        audioSource.clip = soundDatabase.enterDoorSound;
        audioSource.Play();
    }
}
