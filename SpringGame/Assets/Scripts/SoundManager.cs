using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private AudioSource audioSource;
    private float soundEffectVolume = .1f;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }
    void Start()
    {

        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip, soundEffectVolume);
        
    }
}
