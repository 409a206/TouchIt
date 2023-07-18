using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandRigidBody : MonoBehaviour
{
    public GameObject HandGo;
    public GameObject XROrigin;
    private Rigidbody _rigid;
    [SerializeField]
    private float handTouchPower = 10f;

    // Start is called before the first frame update
    void Awake()
    {
        Physics.IgnoreCollision(XROrigin.GetComponent<CharacterController>(), this.GetComponent<Collider>());
        _rigid = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
            this.transform.position = HandGo.transform.position;
    }


    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "PaintBall") {
            Vector3 contactPos = other.contacts[0].point;
            Vector3 contactDir =  (other.transform.position - contactPos).normalized;
            other.rigidbody.AddForce(contactDir * handTouchPower, ForceMode.Force);
            Debug.Log("sef");
        }
    }

}
