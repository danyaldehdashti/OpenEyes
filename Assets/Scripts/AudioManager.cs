using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    
    [SerializeField] private List<AudioClip> sounds;

    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayAudio(string audioName)
    {
        AudioClip clip = sounds.Find(x => x.name == audioName);
        audioSource.clip = clip;
        audioSource.Play();
    }
}
