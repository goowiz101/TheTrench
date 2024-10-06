using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class Key : MonoBehaviour
{
    private Transform originalTransform;
    private bool isPickedUp = false;

    private void Start()
    {
        originalTransform = transform;
    }

    private void Update()
    {
        if (isPickedUp)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero, 60f * Time.deltaTime);
        }
    }

    public void PickUpKey()
    {
        // TODO: attach to bone of hand?
        transform.SetParent(FirstPersonController.Instance.handTransform);
        isPickedUp = true;
    }
}
