using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using TMPro;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform InteractorSource;
    [SerializeField] private float InteractRange;

    private StarterAssetsInputs _input;

    void Start()
    {
        _input = FirstPersonController.Instance.GetComponent<StarterAssetsInputs>();
    }

    void Update()
    {
        HandleInteraction();
    }

    private void HandleInteraction()
    {
        // If press E, fire a raycast
        if (_input.interact) 
        {
            Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
            // If it hits something, call interact
            //Debug.DrawRay(InteractorSource.position, InteractorSource.forward * InteractRange, Color.white, 5f);
            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange, ~LayerMask.NameToLayer("Interactable")))
            {
                if (hitInfo.transform == null) { return; }
                if (!hitInfo.transform.TryGetComponent<Interaction>(out Interaction interactObj)) { return; }
                interactObj.Interact();
            }
            _input.interact = false;
        }
    }


}
