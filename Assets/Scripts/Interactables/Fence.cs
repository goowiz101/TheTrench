using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence : MonoBehaviour
{
    [SerializeField] private Transform leftFence;
    [SerializeField] private Transform rightFence;

    private float maxRotation = 100f;
    private float timeToRotate = 0.75f;

    public void OpenFence()
    {
        StartCoroutine(SwingOpen());
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
