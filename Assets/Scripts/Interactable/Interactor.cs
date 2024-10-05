using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform InteractorSource;
    [SerializeField] private float InteractRange;

    void Update()
    {
        HandleInteraction();
    }

    private void HandleInteraction()
    {
        // If press E, fire a raycast
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
            // If it hits something, call interact
            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
            {
                if (hitInfo.transform == null) { return; }
                if (!hitInfo.transform.TryGetComponent<Interaction>(out Interaction interactObj)) { return; }

                interactObj.Interact();
            }
        }
    }


}
