using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    private bool isPickedUp = false;
    public bool canFire = false;

    private Vector3 goalPosition;

    [SerializeField] private GameObject grenadeLive;

    public static List<Interaction> allGrenades = new List<Interaction>();

    void Start()
    {
        allGrenades.Add(GetComponent<Interaction>());
    }

    private void Update()
    {
        if (isPickedUp)
        {
            // TODO: timer, not hardcoded time duh
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, goalPosition, 60f * Time.deltaTime);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0f,0f,0f), 20f * Time.deltaTime);
            transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(2, 2, 2), 180f * Time.deltaTime);

            // DEBUG
            if (Input.GetMouseButtonDown(0))
            {
                Fire();
            }
        }
    }

    public void Fire()
    {
        if(!canFire) { return; }

        GameObject nade = Instantiate(grenadeLive, this.transform.position, Quaternion.identity);
        //Vector3 direction = Quaternion.Euler(FirstPersonController.Instance.GetCameraPitch(), 0, 0) * FirstPersonController.Instance.transform.up;
        Vector3 direction = FirstPersonController.Instance.GetCameraForward();
        nade.GetComponent<Rigidbody>().velocity = direction * 25f;
        Vector3 torque = new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-3f, -1f));
        nade.GetComponent<Rigidbody>().AddTorque(torque, ForceMode.VelocityChange);

        FirstPersonController.Instance.DisableGrenade();
        // canFire = false;
    }

    public void PickUpGrenade()
    {
        foreach (Interaction nade in allGrenades)
        {
            nade.isInteractable = false;
        }

        goalPosition = new Vector3(0, 0, 0.2f);
        transform.SetParent(FirstPersonController.Instance.handTransform);
        FirstPersonController.Instance.GiveGrenade(this);
        isPickedUp = true;
        canFire = true;
        GetComponent<Interaction>().isInteractable = false;
    }
}
