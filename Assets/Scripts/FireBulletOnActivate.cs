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
    private SoundManager soundManager;
    private bool isSelectSoundPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(FireBullet);
        soundManager = GameObject.FindObjectOfType<SoundManager>();
    }
    private void Update() {
        PlaySoundOnGrab();
    }

    private void PlaySoundOnGrab()
    {
        if(this.GetComponent<XRGrabInteractable>().isSelected) {
            if(!isSelectSoundPlayed) {
                GetComponent<AudioSource>().PlayOneShot(soundManager.effectSounds[10].clip);
                isSelectSoundPlayed = true;
            }
        } else {
            isSelectSoundPlayed = false;
        }
    }

    private void FireBullet(ActivateEventArgs arg)
    {
        GameObject spawnedBullet = Instantiate(bullet);
        spawnedBullet.transform.position = spawnPoint.position;
        spawnedBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * fireSpeed;
        //this.GetComponent<AudioSource>().PlayOneShot(soundManager.effectSounds[4].clip);
        Destroy(spawnedBullet, 5);    
    }
}
