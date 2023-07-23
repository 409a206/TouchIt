using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ActivateGrabRay : MonoBehaviour
{
    public GameObject leftGrabRay;
    public GameObject RightGrabRay;

    public XRDirectInteractor leftDirectGrab;
    public XRDirectInteractor rightDirectGrab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        leftGrabRay.SetActive(leftDirectGrab.interactablesSelected.Count == 0);
        RightGrabRay.SetActive(rightDirectGrab.interactablesSelected.Count == 0);
    }
}
