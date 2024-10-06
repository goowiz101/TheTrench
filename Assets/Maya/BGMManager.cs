using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BGMManager : MonoBehaviour
{
    private static BGMManager instance;
    [SerializeField] private AudioSource ambientSource;
    [SerializeField] private AudioSource monsterSource;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        PlayBGM();
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Q)) { FadeOut(15f); }
    }

    public void PlayBGM()
    {
        ambientSource.Play();
    }
    public void StopBGM()
    {
        ambientSource.Stop();
    }
    public void FadeOut(float overSeconds)
    {
        StartCoroutine(FadeOutBGM(overSeconds));
    }


    // coroutines
    private IEnumerator FadeOutBGM(float overSeconds)
    {
        float elapsedTime = 0f;
        float originalVolume = ambientSource.volume;
        while (elapsedTime < overSeconds)
        {
            elapsedTime += Time.deltaTime; 
            if (elapsedTime > overSeconds) { elapsedTime = overSeconds; }
            ambientSource.volume = originalVolume * (1 - elapsedTime / overSeconds);
            yield return null;
        }
        StopBGM();
        yield return null;
    }
}
