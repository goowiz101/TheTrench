using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using TMPro;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform InteractorSource;
    [SerializeField] private float InteractRange;

    [SerializeField] private TMP_Text promptText;

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
        /*
        // If press E, fire a raycast
        if (_input.interact) 
        {
            Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
            // If it hits something, call interact
            //Debug.DrawRay(InteractorSource.position, InteractorSource.forward * InteractRange, Color.white, 5f);
            int layerMask = 1 << 9;
            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange, layerMask))
            {
                if (hitInfo.transform == null) { return; }
                if (!hitInfo.transform.TryGetComponent<Interaction>(out Interaction interactObj)) { return; }
                interactObj.Interact();
            }
            _input.interact = false;
        }
        */

        // each frame, fire a raycast
        Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
        int layerMask = 1 << 9;
        promptText.text = "";
        if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange, layerMask))
        {
            if (hitInfo.transform == null) { return; }
            if (!hitInfo.transform.TryGetComponent<Interaction>(out Interaction interactObj)) { return; }
            if (!interactObj.isInteractable) { return; }
            DetectHit(interactObj);
            if (_input.interact)
            {
                interactObj.Interact();
            }
        }
        _input.interact = false;
    }

    private void DetectHit(Interaction interactObj)
    {
        promptText.text = "Press E to " + interactObj.verb;
    }

}
