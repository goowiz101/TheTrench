using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static EnemyController Instance;

    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Debug.LogError("Multiple EnemyController scripts detected." ); Destroy(this); }
    }

    void Start()
    {
        Disappear();
    }

    // public functions
    public void SpawnAt(Transform point)
    {
        this.transform.SetPositionAndRotation(point.position, point.rotation);
        Appear();
    }
    public void Despawn()
    {
        Disappear();
    }


    // Helper functions
    private void Appear()
    {
        gameObject.SetActive(true);
    }
    private void Disappear()
    {
        gameObject.SetActive(false);
    }
}
