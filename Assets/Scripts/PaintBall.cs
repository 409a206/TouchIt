using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBall : MonoBehaviour
{
    public Color color;
    private Rigidbody _rigidBody;
    [HideInInspector]
    public Vector3 originPos;
    private Vector3 lastTouchPos;
    private Vector3 collisionPos;


    //paint radius 적용 상수
    private float paintRadiusAdjustmentConstant = 1f;

    private void Awake() {
        _rigidBody = this.GetComponent<Rigidbody>();
        SetPaintColor(color);
        lastTouchPos = originPos;
    }

   private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Paintable") {
            lastTouchPos = this.transform.position;
            CalculatePaintRadius();
            StartCoroutine(DestroySphereCoroutine());
        }
   }

   private void CalculatePaintRadius() {
        collisionPos = this.transform.position;
        this.GetComponent<PaintIn3D.P3dPaintDecal>().Radius = (1f / (Vector3.Distance(lastTouchPos,collisionPos))) * paintRadiusAdjustmentConstant;
   }

   IEnumerator DestroySphereCoroutine() {
        yield return new WaitForSeconds(0.03f);
        Destroy(this.gameObject);
   }

   public void SetPaintColor(Color color) {
        this.gameObject.GetComponent<PaintIn3D.P3dPaintDecal>().Color = color;
        this.gameObject.GetComponent<Renderer>().material.color = color;
   }
}
