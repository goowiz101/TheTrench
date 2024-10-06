using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class FootstepController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> potentialSteps;

    private bool running = false;

    private float timer = 0.3f;
    private float delayBetweenSteps = 0.5f;
    private float randomPitchVariation = 0.3f;

    // Update is called once per frame
    void Update()
    {
        if (!running && FirstPersonController.Instance._speed > 1)
        {
            running = true;
        }
        else if (running && FirstPersonController.Instance._speed < 1)
        {
            running = false;
        }

        if (running)
        {
            timer += Time.deltaTime;
            if (timer > delayBetweenSteps)
            {
                timer = 0f;
                audioSource.pitch = Random.Range(1-randomPitchVariation, 1+randomPitchVariation);
                audioSource.PlayOneShot(potentialSteps[Random.Range(0, potentialSteps.Count-1)]);
            }
        }
        else
        {
            timer = 0.3f;
        }
    }
}
