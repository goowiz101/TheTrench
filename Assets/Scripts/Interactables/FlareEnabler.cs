using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class FlareEnabler : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("walked into flare collider"); // TODO: do I need player tag?
        if (FirstPersonController.Instance.HasFlare())
        {
            FirstPersonController.Instance.EnableFlareGun();
        }
    }

}
