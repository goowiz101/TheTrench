using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    public Transform spawnPoint;
    public bool spawnOrDespawn;

    void OnTriggerEnter(Collider other)
    {
        if (spawnOrDespawn)
        {
            Debug.Log("spawn");
            EnemyController.Instance.SpawnAt(spawnPoint);
        }
        else
        {
            Debug.Log("despawn");
            EnemyController.Instance.Despawn();
        }
    }

}
