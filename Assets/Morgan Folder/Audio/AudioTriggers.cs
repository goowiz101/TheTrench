using UnityEngine;

public class AudioTriggers : MonoBehaviour
{
    // Reference to the AudioSource component
    private AudioSource audioSource;

    // Initialize the AudioSource component
    void Start()
    {
        // Get the AudioSource component on this GameObject
        audioSource = GetComponent<AudioSource>();
    }

    // Method called when another object enters the trigger collider
    void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger has the "Player" tag
        if (other.CompareTag("Player"))
        {
            // Play the audio clip
            audioSource.Play();
        }
    }
}
