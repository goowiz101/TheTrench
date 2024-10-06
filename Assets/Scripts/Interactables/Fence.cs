using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class Fence : MonoBehaviour
{
    [SerializeField] private Transform leftFence;
    [SerializeField] private Transform rightFence;
    [SerializeField] private List<GameObject> planks;
    [SerializeField] private List<BoxCollider> groundColliders;

    private float maxRotation = 100f;
    private float timeToRotate = 0.75f;

    private bool exploded = false;
    [SerializeField] private bool removeGrenadeAfter = false;

    public void OpenFence()
    {
        StartCoroutine(SwingOpen());
    }

    public void ExplodeFence(Vector3 explosionPos)
    {
        if (exploded) { return; }
        exploded = true;
        foreach(BoxCollider col in groundColliders)
        {
            col.enabled = false;
        }
        foreach(GameObject plank in planks)
        {
            plank.GetComponent<BoxCollider>().enabled = true;
            plank.GetComponent<Rigidbody>().useGravity = true;
            plank.GetComponent<Rigidbody>().AddExplosionForce(20f, explosionPos, 10f, 3f, ForceMode.VelocityChange);
        }
        if (removeGrenadeAfter)
        {
            FirstPersonController.Instance.RemoveGrenade();
        }
    }

    IEnumerator SwingOpen()
    {
        float elapsedTime = 0f;
        float originalLeftY = leftFence.eulerAngles.y;
        float originalRightY = rightFence.eulerAngles.y;
        while(elapsedTime < timeToRotate)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > timeToRotate) { elapsedTime = timeToRotate; }

            leftFence.eulerAngles = new Vector3(leftFence.eulerAngles.x, originalLeftY - maxRotation * elapsedTime / timeToRotate, leftFence.eulerAngles.z);
            rightFence.eulerAngles = new Vector3(rightFence.eulerAngles.x, originalRightY + maxRotation * elapsedTime / timeToRotate, rightFence.eulerAngles.z);
            yield return null;
        }
        yield return null;
    }
}
