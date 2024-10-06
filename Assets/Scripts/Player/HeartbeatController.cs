using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum HeartRate
{
    Normal,
    Fast,
    Fastest
}
public class HeartbeatController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip heartbeatNormal;
    [SerializeField] private AudioClip heartbeatFast;
    [SerializeField] private AudioClip heartbeatFastest;

    [SerializeField] private float BPM = 25f;
    private HeartRate heartRate;

    private float BPMFastCutoff = 45f;
    private float BPMFastestCutoff = 60f;

    private float maxPitchVariation = 0.15f;

    private float timer;

    void Start()
    {
        timer = 0f;
        heartRate = HeartRate.Normal;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > (60f/BPM))
        {
            UpdateHeartRate();
            AudioClip clip = heartbeatNormal;
            switch (heartRate)
            {
                case HeartRate.Normal:
                    clip = heartbeatNormal;
                    break;
                case HeartRate.Fast:
                    clip = heartbeatFast;
                    break;
                case HeartRate.Fastest:
                    clip = heartbeatFastest;
                    break;
                default:
                    clip = heartbeatNormal;
                    break;
            }
            //Debug.Log("beat");
            audioSource.pitch = Random.Range(1-maxPitchVariation, 1+maxPitchVariation);
            audioSource.PlayOneShot(clip);
            timer = 0f;
        }
    }

    private void UpdateHeartRate()
    {
        if (BPM > BPMFastestCutoff) { heartRate = HeartRate.Fastest; }
        else if (BPM > BPMFastCutoff) { heartRate = HeartRate.Fast; }
        else { heartRate = HeartRate.Normal; }
    }


}
