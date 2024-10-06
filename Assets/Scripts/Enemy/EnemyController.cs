using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyController : MonoBehaviour
{
    public static EnemyController Instance;

    [SerializeField] private Volume monsterPPVol;

    private bool isActive;
    private float spawnDistFromPlayer;
    private float distanceAdjustPP = 10f;

    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Debug.LogError("Multiple EnemyController scripts detected." ); Destroy(this); }
    }

    void Start()
    {
        Disappear();
    }

    void Update()
    {
        if (isActive)
        {
            float distanceFromPlayer = Vector3.Distance(transform.position, FirstPersonController.Instance.transform.position);
            SetPostProcessBlend(1 - ((distanceFromPlayer - distanceAdjustPP) / (spawnDistFromPlayer - distanceAdjustPP)));
        }
    }

    // public functions
    public void SpawnAt(Transform point)
    {
        this.transform.SetPositionAndRotation(point.position, point.rotation);
        Appear();
    }
    public void Despawn()
    {
        //Disappear();
        SoundManager.PlaySound(SoundType.RADIO);
        StartCoroutine(ChasePlayer());
    }



    // Helper functions
    private void Appear()
    {
        gameObject.SetActive(true);
        isActive = true;
        spawnDistFromPlayer = Vector3.Distance(transform.position, FirstPersonController.Instance.transform.position);
    }
    private void Disappear()
    {
        gameObject.SetActive(false);
        isActive = false;
        SetPostProcessBlend(0);
    }
    private void SetPostProcessBlend(float percentage)
    {
        percentage = Mathf.Clamp(percentage, 0f, 1f);
        Debug.Log("setting post process to " + percentage);

        //basePPVol.weight = (1f - percentage);
        monsterPPVol.weight = (percentage);
    }

    IEnumerator ChasePlayer()
    {
        // TODO: play audio
        float totalTime = 1f;
        float elapsedTime = 0f;
        Vector3 dir = FirstPersonController.Instance.transform.position - this.transform.position;
        dir = Vector3.Normalize(dir);
        while (elapsedTime < totalTime)
        {
            this.transform.position += dir * 40 * Time.deltaTime;
            yield return null;
        }
        Disappear();
        yield return null;
    }
}
