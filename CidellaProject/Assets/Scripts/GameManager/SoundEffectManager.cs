using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundEffectManager : StaticMonoBehaviour<SoundEffectManager>
{
    [SerializeField] private List<AudioClip> soundEffectList = new();
    private AudioSource audioSource;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundEffect(int clipIndex, float volume)
    {
        audioSource.pitch = Random.Range(.75f, 1.1f);
        audioSource.PlayOneShot(soundEffectList[clipIndex], volume);
    }
}
