using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]

[Serializable]
public class SoundDatabase : ScriptableObject
{
    [Header("Music")]
    public AudioClip mainMenuMusic;
    public AudioClip level1Music;
    public AudioClip level2Music;
    public AudioClip level3Music;
    public AudioClip level4Music;
    public AudioClip endCreditMusic;
    public AudioClip winMusic;
    public AudioClip loseMusic;

    [Header("Sound Effect")]
    public AudioClip attackSound;
    public AudioClip deadSound;
    public AudioClip collectCoinSound;
    public AudioClip enterDoorSound;
    public AudioClip enemyHitplayerSound;
    public AudioClip jumpSound;

    public AudioClip EnemyPatroSound;
    public AudioClip EnemySound;

    [Header("UI Sound")]
    public AudioClip clickSound;
    public AudioClip warningSound;
    public AudioClip mouseOnUISound;
    public AudioClip upgradeSound;

    
}