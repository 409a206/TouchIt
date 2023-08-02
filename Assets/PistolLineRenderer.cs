using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PistolLineRenderer : MonoBehaviour
{
    [SerializeField]
    private Transform bulletSpawnPoint;
    // Start is called before the first frame update
    void Awake()
    {
        //this.GetComponent<LineRenderer>().SetPosition(1, bulletSpawnPoint.transform.forward.normalized);
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfPistolIsGrabbed();
    }

    private void CheckIfPistolIsGrabbed()
    {
        if(this.GetComponentInParent<XRGrabInteractable>().isSelected) {
           this.GetComponent<LineRenderer>().enabled = true;
        } else {
            this.GetComponent<LineRenderer>().enabled = false;
        }
    }
}
