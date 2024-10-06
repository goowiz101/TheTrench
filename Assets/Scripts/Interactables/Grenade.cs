using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    private bool isPickedUp = false;

    private Vector3 goalPosition;

    private void Update()
    {
        if (isPickedUp)
        {
            // TODO: timer, not hardcoded time duh
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, goalPosition, 60f * Time.deltaTime);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0f,0f,0f), 20f * Time.deltaTime);

            // DEBUG
            if (Input.GetMouseButtonDown(0))
            {
                // Fire();
            }
        }
    }

    public void PickUpGrenade()
    {
        goalPosition = new Vector3(0, 0, 0.2f);
        transform.SetParent(FirstPersonController.Instance.handTransform);
        FirstPersonController.Instance.GiveGrenade();
        isPickedUp = true;
        GetComponent<Interaction>().isInteractable = false;
    }
}
