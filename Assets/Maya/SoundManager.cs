using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    EXPLOSION,
    FLARE,
    FOOTSTEPS,
    GUNSHOT,
    HEARTBEAT,
    BREATHING,
    METAL,
    RADIO,
    UNLOCK,
    WAR,
    LOCK

}
[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundList;
    private static SoundManager instance;
    private AudioSource audioSource;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public static void PlaySound(SoundType sound, float volume = 1)
    {
        instance.audioSource.PlayOneShot(instance.soundList[(int)sound], volume);
    }
}
