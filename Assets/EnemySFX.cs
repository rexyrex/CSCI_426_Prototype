using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class EnemySFX : MonoBehaviour {
    AudioSource audioSource;

    public AudioClip[] popEffects;
    public AudioClip[] spawnEffects;

    public float PopDelay { get { return popEffect.length; } }
    public float SpawnDelay { get { return spawnEffect.length; } }

    AudioClip popEffect;
    AudioClip spawnEffect;

    bool hasPopped = false;
    bool hasSpawned = false;

    void Awake()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        var randClip = Random.Range(0, popEffects.Length - 1);
        popEffect = popEffects[randClip];

        randClip = Random.Range(0, spawnEffects.Length - 1);
        spawnEffect = spawnEffects[randClip];
    }

    internal void PlayPop()
    {
        audioSource.enabled = true;

        if (!hasPopped)
        {
            audioSource.PlayOneShot(popEffect);
            hasPopped = true;
        }
    }

    internal void PlaySpawn()
    {
        if (!hasSpawned)
        {
            Debug.Log(audioSource);
            Debug.Log(spawnEffect);
            audioSource.clip = spawnEffect;
            audioSource.Play();
            hasSpawned = true;
        }
    }
}
