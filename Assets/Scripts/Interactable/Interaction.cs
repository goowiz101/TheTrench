using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interaction : MonoBehaviour
{
    public UnityEvent OnInteract;
    public string verb;
    private bool isInteractable = true;
    public void Interact()
    {
        if (isInteractable)
        {
            Debug.Log("Interacted with: " + name);
            OnInteract?.Invoke();
        }
        else
        {
            Debug.Log("Tried to interact with disabled: " + name);
        }
    }
    public void DisableInteraction()
    {
        isInteractable = false;
    }
}
