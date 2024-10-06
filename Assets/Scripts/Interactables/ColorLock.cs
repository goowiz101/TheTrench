using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.Events;

public class ColorLock : MonoBehaviour
{

    private Transform originalTransform;

    //[SerializeField] private AudioSource audioSource;
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
        numbers = new List<int>{0, 0, 0};
    }

    private void Update()
    {
        if (clickTimer < clickCooldown)
        {
            clickTimer += Time.deltaTime;
        }

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
        goalPosition = FirstPersonController.Instance.faceTransform.TransformPoint(0,0.5f,0);
        goalRotation = Quaternion.LookRotation(FirstPersonController.Instance.GetCameraForward(), FirstPersonController.Instance.GetCameraUp());
        isPickedUp = true;
        AssociatedUI.SetActive(true);
    }
    public void DeactivateLock()
    {
        FirstPersonController.Instance.EnableInput();
        FirstPersonController.Instance.DisableCursor();
        //StartCoroutine(LerpFromPlayer());
        //goalPosition = originalPosition;
        //goalRotation = originalRotation;
        isPickedUp = false;
        AssociatedUI.SetActive(false);
    }
    public void IncreaseNumber(int number)
    {
        if (clickTimer < clickCooldown) { return; }
        clickTimer = 0f;

        numbers[number] = (numbers[number] + 1) % 9;
        StartCoroutine(LerpWheelUp(Wheels[number]));
    }
    public void DecreaseNumber(int number)
    {
        if (clickTimer < clickCooldown) { return; }
        clickTimer = 0f;

        numbers[number] = (numbers[number] - 1 + 9) % 9;
        StartCoroutine(LerpWheelDown(Wheels[number]));
    }
    public void CheckIfCorrect()
    {
        if (numbers[0] != 4) { SoundManager.PlaySound(SoundType.LOCK); return; }
        if (numbers[1] != 4) { SoundManager.PlaySound(SoundType.LOCK); return; }
        if (numbers[2] != 3) { SoundManager.PlaySound(SoundType.LOCK); return; }

        SoundManager.PlaySound(SoundType.UNLOCK);
        OnUnlocked?.Invoke();
        DeactivateLock();
        this.gameObject.SetActive(false);
    }

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
