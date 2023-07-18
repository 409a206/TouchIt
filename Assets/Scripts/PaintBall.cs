using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBall : MonoBehaviour
{
    public Color color;
    private Rigidbody _rigidBody;
    private Vector3 lastTouchPos;
    private Vector3 collisionPos;
    
    //paint radius 적용 상수
    private float paintRadiusAdjustmentConstant = 0.1f;

    private void Awake() {
        _rigidBody = this.GetComponent<Rigidbody>();
        this.gameObject.GetComponent<Renderer>().material.color = color;

        this.gameObject.GetComponent<PaintIn3D.P3dPaintDecal>().Color = color;
        lastTouchPos = this.transform.position;
    }

   private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Paintable") {
            CalculatePaintRadius();
            StartCoroutine(DestroySphereCoroutine());
        }
   }

   private void CalculatePaintRadius() {
        collisionPos = this.transform.position;
        this.GetComponent<PaintIn3D.P3dPaintDecal>().Radius = Vector3.Distance(lastTouchPos,collisionPos) * paintRadiusAdjustmentConstant;
   }

   IEnumerator DestroySphereCoroutine() {
        yield return new WaitForSeconds(0.1f);
        Destroy(this.gameObject);
   }
}
