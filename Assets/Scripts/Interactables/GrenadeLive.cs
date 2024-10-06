using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLive : MonoBehaviour
{

    public GameObject explosionFX;

    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 7)
        {
            Detonate();

            Fence hitFence = other.transform.GetComponentInParent<Fence>();
            if (hitFence != null) { hitFence.ExplodeFence(this.transform.position); }
            return;
        }
    }


    void Detonate()
    {
        Instantiate(explosionFX, this.transform.position, Quaternion.identity);
        SoundManager.PlaySound(SoundType.EXPLOSION);
        Destroy(this.gameObject);
    }
}
