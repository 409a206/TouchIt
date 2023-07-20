using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandRigidBody : MonoBehaviour
{
    public GameObject XROrigin;
    private Rigidbody _rigid;
    [SerializeField]
    private float handTouchPower = 10f;

    // Start is called before the first frame update
    void Awake()
    {
        _rigid = this.GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "PaintBall") {
            Vector3 contactPos = other.contacts[0].point;
            Vector3 contactDir =  (other.transform.position - contactPos).normalized;
            other.rigidbody.AddForce(contactDir * handTouchPower, ForceMode.Force);

            this.GetComponentInParent<XRBaseControllerInteractor>().xrController.SendHapticImpulse(0.7f,0.5f);
            // Debug.Log("sef");
        }
    }

}
