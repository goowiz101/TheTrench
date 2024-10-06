using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class Lock : MonoBehaviour
{
    private float timeToLerp = 0.25f;
    private Vector3 offsetFromPlayer = new Vector3(0f, 1.5f, 0f);
    private float inFrontOfPlayer = 1f;

    private Transform originalTransform;

    private void Start()
    {
        originalTransform = transform;
    }

    public void ActivateLock()
    {
        FirstPersonController.Instance.DisableInput();
        FirstPersonController.Instance.EnableCursor();
        StartCoroutine(LerpToPlayer());
    }
    public void DeactivateLock()
    {
        FirstPersonController.Instance.EnableInput();
        FirstPersonController.Instance.DisableCursor();
        StartCoroutine(LerpFromPlayer());
    }

    // coroutines
    public IEnumerator LerpToPlayer()
    {
        float elapsedTime = 0f;
        while (elapsedTime < timeToLerp)
        {
            transform.position = Vector3.Lerp(originalTransform.position, 
                                                FirstPersonController.Instance.transform.position + FirstPersonController.Instance.transform.forward * inFrontOfPlayer + offsetFromPlayer,
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

}
