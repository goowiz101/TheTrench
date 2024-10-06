using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class FlareGun : MonoBehaviour
{
    private Transform originalTransform;
    private bool isPickedUp = false;
    private bool canFire = true;

    private float timeToLookAtSky = 0.25f;

    public GameObject flarePrefab;

    private void Start()
    {
        originalTransform = transform;
    }

    private void Update()
    {
        if (isPickedUp)
        {
            // TODO: timer, not hardcoded time duh
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero, 60f * Time.deltaTime);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0f,0f,90f), 20f * Time.deltaTime);

            // DEBUG
            if (Input.GetMouseButtonDown(0))
            {
                Fire();
            }
        }
    }

    public void PickUpGun()
    {
        // TODO: attach to bone of hand?
        transform.SetParent(FirstPersonController.Instance.handTransform);
        isPickedUp = true;
    }

    public void Fire()
    {
        if(!canFire) { return; }

        canFire = false;
        Debug.Log("pshoooo");
        StartCoroutine(LookAtSky());
    }

    IEnumerator LookAtSky()
    {
        FirstPersonController.Instance.DisableInput();

        float elapsedTime = 0f;
        float initialPitch = FirstPersonController.Instance.GetCameraPitch();
        while (elapsedTime < timeToLookAtSky)
        {
            FirstPersonController.Instance.SetCameraPitch(Mathf.Lerp(initialPitch, -55f, Mathf.Min(1.0f, elapsedTime / timeToLookAtSky)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(0.25f);
        GameObject flare = Instantiate(flarePrefab, this.transform.position, Quaternion.identity);
        //Vector3 direction = Quaternion.Euler(FirstPersonController.Instance.GetCameraPitch(), 0, 0) * FirstPersonController.Instance.transform.up;
        Vector3 direction = transform.forward;
        flare.GetComponent<Rigidbody>().velocity = direction * 50f;
        //FirstPersonController.Instance.EnableInput();
        yield return null;
    }
}
