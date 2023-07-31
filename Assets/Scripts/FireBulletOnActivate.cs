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
    private RaycastHit hit;

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
            // Debug.DrawRay(spawnPoint.position, spawnPoint.forward * 10f, Color.red);
            if(Physics.Raycast(spawnPoint.position, spawnPoint.forward, out RaycastHit hitInfo)) {
                if(hitInfo.transform.tag == "PaintBall") {
                    hit = hitInfo;
                    Debug.Log("Raycasted to " + hitInfo.transform.name);
                    hitInfo.transform.gameObject.GetComponent<Renderer>().material.SetInt("_isAimedAt", 1);
                    //Destroy(hitInfo.transform.gameObject);
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
