using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.Events;

public class GasMask : MonoBehaviour
{
    private Transform originalTransform;

    bool isPickedUp = false;
    Vector3 originalPosition;
    Quaternion originalRotation;
    Vector3 goalPosition;
    Quaternion goalRotation;

    public UnityEvent OnWorn;

    private void Start()
    {
        //originalTransform = transform;
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    private void Update()
    {
        if (isPickedUp)
        {
            //lerpTimer += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, goalPosition, 10f * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, goalRotation, 10f * Time.deltaTime);

            if (Vector3.Distance(goalPosition, transform.position) < 0.1f)
            {
                OnWorn?.Invoke();
                gameObject.SetActive(false);
            }
        }
    }

    public void ActivateMask()
    {
        goalPosition = FirstPersonController.Instance.headTransform.TransformPoint(0,0,0);
        //goalRotation = Quaternion.FromToRotation(transform.forward, -FirstPersonController.Instance.GetCameraForward());
        goalRotation = Quaternion.LookRotation(FirstPersonController.Instance.GetCameraForward(), FirstPersonController.Instance.GetCameraUp()) * transform.rotation;
        isPickedUp = true;
        GetComponent<Interaction>().isInteractable = false;
    }

}
