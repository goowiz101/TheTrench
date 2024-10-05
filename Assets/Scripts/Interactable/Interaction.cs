using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interaction : MonoBehaviour
{
    public UnityEvent OnInteract;
    public void Interact()
    {
        Debug.Log("Interacted with: " + name);
        OnInteract?.Invoke();
    }
}
