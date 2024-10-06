using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;

public class NameLock : MonoBehaviour
{
    //private float timeToLerp = 0.25f;
    //private Vector3 offsetFromPlayer = new Vector3(0f, 1.5f, 0f);
    //private float inFrontOfPlayer = 1f;

    private Transform originalTransform;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip lockFailSFX;
    [SerializeField] private AudioClip lockSucceedSFX;

    [SerializeField] private GameObject AssociatedUI;
    [SerializeField] private List<GameObject> Wheels;
    private List<int> numbers;
    private float twistTime = 0.2f;
    private float clickCooldown = 0.25f;
    private float clickTimer = 0f;

    bool isPickedUp = false;
   // float lerpTimer = 0;
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
        numbers = new List<int>{0, 0, 0, 0, 0, 0};
    }

    private void Update()
    {
        if (clickTimer < clickCooldown)
        {
            clickTimer += Time.deltaTime;
        }

        if (isPickedUp)
        {
            //lerpTimer += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, goalPosition, 5f * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, goalRotation, 5f * Time.deltaTime);
        }
        else
        {
           // lerpTimer = 0;
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, 60f * Time.deltaTime);
        }
    }

    public void ActivateLock()
    {
        FirstPersonController.Instance.DisableInput();
        FirstPersonController.Instance.EnableCursor();
        //StartCoroutine(LerpToPlayer());
        goalPosition = FirstPersonController.Instance.faceTransform.TransformPoint(0,0,0);
        //goalRotation = Quaternion.FromToRotation(transform.forward, -FirstPersonController.Instance.GetCameraForward());
        goalRotation = Quaternion.LookRotation(FirstPersonController.Instance.GetCameraForward(), FirstPersonController.Instance.GetCameraUp());
        isPickedUp = true;
        AssociatedUI.SetActive(true);
    }
    public void DeactivateLock()
    {
        FirstPersonController.Instance.EnableInput();
        FirstPersonController.Instance.DisableCursor();
        //StartCoroutine(LerpFromPlayer());
        goalPosition = originalPosition;
        goalRotation = originalRotation;
        AssociatedUI.SetActive(false);
    }
    public void IncreaseNumber(int number)
    {
        if (clickTimer < clickCooldown) { return; }
        clickTimer = 0f;

        numbers[number] = (numbers[number] + 1) % 9;
        //Debug.Log("Numbers are: " + numbers[0] + numbers[1] + numbers[2] + numbers[3] + numbers[4] + numbers[5]); 
        StartCoroutine(LerpWheelUp(Wheels[number]));
    }
    public void DecreaseNumber(int number)
    {
        if (clickTimer < clickCooldown) { return; }
        clickTimer = 0f;

        numbers[number] = (numbers[number] - 1 + 9) % 9;
        //Debug.Log("Numbers are: " + numbers[0] + numbers[1] + numbers[2] + numbers[3] + numbers[4] + numbers[5]); 
        StartCoroutine(LerpWheelDown(Wheels[number]));
    }
    public void CheckIfCorrect()
    {
        if (numbers[0] != 2) { audioSource.PlayOneShot(lockFailSFX); return; }
        if (numbers[1] != 4) { audioSource.PlayOneShot(lockFailSFX); return; }
        if (numbers[2] != 1) { audioSource.PlayOneShot(lockFailSFX); return; }
        if (numbers[3] != 8) { audioSource.PlayOneShot(lockFailSFX); return; }
        if (numbers[4] != 2) { audioSource.PlayOneShot(lockFailSFX); return; }
        if (numbers[5] != 0) { audioSource.PlayOneShot(lockFailSFX); return; }

        audioSource.PlayOneShot(lockSucceedSFX);
        OnUnlocked?.Invoke();
        DeactivateLock();
        this.gameObject.SetActive(false);
    }

    // coroutines
    /*
    public IEnumerator LerpToPlayer()
    {
        float elapsedTime = 0f;
        Vector3 goalPosition = FirstPersonController.Instance.handTransform.position;
        goalPosition += (Quaternion.Euler(FirstPersonController.Instance.GetCameraPitch(), 0, 0) * FirstPersonController.Instance.transform.forward) * inFrontOfPlayer;
        while (elapsedTime < timeToLerp)
        {
            transform.position = Vector3.Lerp(originalTransform.position, 
                                                goalPosition,
                                                Mathf.Min(1.0f, elapsedTime / timeToLerp));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }
    public IEnumerator LerpFromPlayer()
    {
        float elapsedTime = 0f;
        while (elapsedTime < timeToLerp)
        {
            transform.position = Vector3.Lerp(FirstPersonController.Instance.transform.position + FirstPersonController.Instance.transform.forward * inFrontOfPlayer + offsetFromPlayer,
                                                originalTransform.position, 
                                                Mathf.Min(1.0f, elapsedTime / timeToLerp));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }
    */
    public IEnumerator LerpWheelUp(GameObject wheel)
    {
        float elapsedTime = 0f;
        while (elapsedTime < twistTime)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > twistTime) { elapsedTime = twistTime; }
            wheel.transform.Rotate(-40 * (Time.deltaTime / twistTime), 0, 0);
            yield return null;
        }
        yield return null;
    }
        public IEnumerator LerpWheelDown(GameObject wheel)
    {
        float elapsedTime = 0f;
        while (elapsedTime < twistTime)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > twistTime) { elapsedTime = twistTime; }
            wheel.transform.Rotate(40 * (Time.deltaTime / twistTime), 0, 0);
            yield return null;
        }
        yield return null;
    }

}
