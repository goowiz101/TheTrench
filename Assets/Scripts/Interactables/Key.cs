using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class Key : MonoBehaviour
{
    private Transform originalTransform;

    private Vector3 goalPosition;
    private bool isPickedUp = false;

    private void Start()
    {
        originalTransform = transform;
    }

    private void Update()
    {
        if (isPickedUp)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, goalPosition, 60f * Time.deltaTime);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0f,0f,0f), 20f * Time.deltaTime);
        }
    }

    public void PickUpKey()
    {
        goalPosition = new Vector3(0, 0, 3f);
        FirstPersonController.Instance.GiveKey(this);
        transform.SetParent(FirstPersonController.Instance.handTransform);
        isPickedUp = true;
    }
}
