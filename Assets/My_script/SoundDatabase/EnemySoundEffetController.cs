using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundEffetController : MonoBehaviour
{
    [SerializeField] private SoundDatabase _soundDatabase;
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void enemyPatroSound()
    {
        audioSource.clip = _soundDatabase.EnemyPatroSound;
        audioSource.Play();
    }

    public void EnemySound()
    {
        audioSource.clip = _soundDatabase.EnemySound;
        audioSource.Play();
    }
}
