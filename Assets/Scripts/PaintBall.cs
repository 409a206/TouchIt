using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PaintBall : MonoBehaviour
{
    public Color color;
    private Rigidbody _rigidBody;


    //shader 관련 변수
    float lerpTime = 1f;
    public bool isRayCastedAt = false;

    private void Awake() {
        _rigidBody = this.GetComponent<Rigidbody>();
        SetPaintColor(color);
    }

   private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Paintable") {
            //CalculatePaintRadius();
            StartCoroutine(DestroySphereCoroutine());
        } else if(other.gameObject.tag == "HandRigidBody") {
          Debug.Log("Hit with hand");
        } else if(other.gameObject.tag == "Bullet") {
          Debug.Log("Hit with Bullet");
        } else if(other.gameObject.tag =="PaintBall") {
          Debug.Log("Hit with PaintBall");
        }
   }



   IEnumerator DestroySphereCoroutine() {
     //float elapsedTime = 0f;
     _rigidBody.velocity = Vector3.zero;
     _rigidBody.angularVelocity = Vector3.zero;
     this.GetComponent<Collider>().enabled = false;
     // while(elapsedTime < lerpTime) {
     //      elapsedTime += Time.deltaTime;

     //      if(elapsedTime >= lerpTime) {
     //           elapsedTime = lerpTime;
     //      }

     //      this.gameObject.GetComponent<Renderer>().material.SetFloat("_Alpha_Clip_Threshold", Mathf.Lerp(0f,1f, elapsedTime/lerpTime));
     //      yield return null;
     // }
     yield return null;
      Destroy(this.gameObject);
   }

   public void SetPaintColor(Color color) {
        this.gameObject.GetComponent<PaintIn3D.P3dPaintDecal>().Color = color;
        this.gameObject.GetComponent<Renderer>().material.color = color;
   }
}

