using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FireBulletOnActivate : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPoint;
    public float fireSpeed = 20f;
    private RaycastHit hitInfo;
    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(FireBullet);
    }
    private void Update() {
        DetectPaintBallOnAim();
    }

    private void DetectPaintBallOnAim()
    {
        if(GetComponent<XRGrabInteractable>().isSelected) {
            if(Physics.Raycast(spawnPoint.position, spawnPoint.forward, out hitInfo)) {
                if(hitInfo.transform.tag == "PaintBall") {
                    Debug.Log("Raycasted to " + hitInfo.transform.name);
                    Destroy(hitInfo.transform.gameObject);
                }
            }
        }
    }

    private void FireBullet(ActivateEventArgs arg)
    {
        GameObject spawnedBullet = Instantiate(bullet);
        spawnedBullet.transform.position = spawnPoint.position;
        spawnedBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * fireSpeed;
        Destroy(spawnedBullet, 5);    
    }
}
