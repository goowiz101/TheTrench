using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.Events;

public class KeyLock : MonoBehaviour
{
    [SerializeField] private GameObject AssociatedUI;

    bool isPickedUp = false;
    Vector3 originalPosition;
    Quaternion originalRotation;
    Vector3 goalPosition;
    Quaternion goalRotation;

    public UnityEvent OnUnlocked;
    
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
            transform.position = Vector3.Lerp(transform.position, goalPosition, 5f * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, goalRotation, 5f * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, originalPosition, 5f * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, originalRotation, 5f * Time.deltaTime);
        }
    }
    public void ActivateLock()
    {
        FirstPersonController.Instance.DisableInput();
        FirstPersonController.Instance.EnableCursor();
        goalPosition = FirstPersonController.Instance.faceTransform.TransformPoint(0,0,1);
        goalRotation = Quaternion.LookRotation(FirstPersonController.Instance.GetCameraForward(), FirstPersonController.Instance.GetCameraUp());
        isPickedUp = true;
        AssociatedUI.SetActive(true);
    }
    public void DeactivateLock()
    {
        FirstPersonController.Instance.EnableInput();
        FirstPersonController.Instance.DisableCursor();
        isPickedUp = false;
        AssociatedUI.SetActive(false);
    }

    public void CheckIfCorrect()
    {
        if (!FirstPersonController.Instance.HasKey()) { SoundManager.PlaySound(SoundType.LOCK); return; }

        SoundManager.PlaySound(SoundType.UNLOCK);
        OnUnlocked?.Invoke();
        DeactivateLock();
        FirstPersonController.Instance.RemoveKey();
        this.gameObject.SetActive(false);

    }

}
