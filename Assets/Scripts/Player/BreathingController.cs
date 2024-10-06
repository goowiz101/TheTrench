using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class BreathingController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    //[SerializeField] private List<AudioClip> potentialBreaths;
    [SerializeField] private AudioClip breathLoop;
    [SerializeField] private AudioClip finalBreath;

    //private float BPM = 35f;

    //private float maxPitchVariation = 0.05f;

    //private float timer;
    private bool tired = false;
    private bool running = false;

    void Start()
    {
        audioSource.clip = breathLoop;
    }

    void Update()
    {
        if (!tired && FirstPersonController.Instance._speed > FirstPersonController.Instance.MoveSpeed-1)
        {
            running = true;
            StartLoop();
        }
        else if (tired && FirstPersonController.Instance._speed < 1f)
        {
            running = false;
            StopLoop();
        }

        if (running) { tired = true; }

        if(!running && tired && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(finalBreath);
            tired = false;
        }


        /*
        if (!running && FirstPersonController.Instance._speed > 2.0f)
        {
            running = true;
            StartLoop();
        }
        else if (running && FirstPersonController.Instance._speed <= 2.0f)
        {
            running = false;
            StopLoop();
        }

        if (audioSource.loop && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(finalBreath);
        }
        */
    }

    void StartLoop()
    {
        Debug.Log("start breathe");
        audioSource.loop = true;
        audioSource.Play();
    }
    void StopLoop()
    {
        audioSource.loop = false;
    }

}
