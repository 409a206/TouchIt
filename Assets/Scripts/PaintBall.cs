using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PaintBall : MonoBehaviour
{
    public Color color;
    private Rigidbody _rigidBody;
    [SerializeField]
    private Transform bulletSpawnPoint;
    private AudioSource _audioSource;
    private bool _isAimSoundPlayed = false;
    private SoundManager soundManager;

    //shader 관련 변수
    float lerpTime = 1f;
    private bool isRayCastedAt = false;

    private void Awake() {
        _rigidBody = this.GetComponent<Rigidbody>();
        SetPaintColor(color);
        bulletSpawnPoint = GameObject.FindWithTag("BulletSpawnPoint").transform;
       
        _audioSource = GetComponent<AudioSource>();
        soundManager = GameObject.FindObjectOfType<SoundManager>();
        //_audioSource.PlayOneShot(soundManager.effectSounds[2].clip);
    }

  private void Update() {
    DetectRaycastFromPistol();
  }
   private void DetectRaycastFromPistol()
    {
        if(bulletSpawnPoint.GetComponentInParent<XRGrabInteractable>().isSelected) {
            // Debug.DrawRay(spawnPoint.position, spawnPoint.forward * 10f, Color.red);
            if(Physics.Raycast(bulletSpawnPoint.position, bulletSpawnPoint.forward, out RaycastHit hitInfo)) {
                if(hitInfo.transform == this.transform) {
                    Debug.Log("Raycasted to " + hitInfo.transform.name);
                    this.GetComponent<Renderer>().material.SetInt("_isAimedAt", 1);
                    if(!_isAimSoundPlayed) {
                      
                      //play aim sound
                      // _audioSource.volume = 0.3f;
                      // _audioSource.PlayOneShot(soundManager.effectSounds[12].clip);
                      
                      _isAimSoundPlayed = true;
                    }
                    //Destroy(hitInfo.transform.gameObject); 
                } else {
                  this.GetComponent<Renderer>().material.SetInt("_isAimedAt", 0);
                  _isAimSoundPlayed = false;
                }
            } 
        }
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
     _audioSource.volume = 1.0f;
     _audioSource.PlayOneShot(soundManager.effectSounds[1].clip);
     _rigidBody.velocity = Vector3.zero;
     _rigidBody.angularVelocity = Vector3.zero;
     this.GetComponent<Collider>().enabled = false;
     this.GetComponent<Renderer>().enabled = false;
     // while(elapsedTime < lerpTime) {
     //      elapsedTime += Time.deltaTime;

     //      if(elapsedTime >= lerpTime) {
     //           elapsedTime = lerpTime;
     //      }

     //      this.gameObject.GetComponent<Renderer>().material.SetFloat("_Alpha_Clip_Threshold", Mathf.Lerp(0f,1f, elapsedTime/lerpTime));
     //      yield return null;
     // }
     yield return new WaitForSeconds(2f);
      Destroy(this.gameObject);
   }

   public void SetPaintColor(Color color) {
        this.gameObject.GetComponent<PaintIn3D.P3dPaintDecal>().Color = color;
        this.gameObject.GetComponent<Renderer>().material.color = color;
   }
}

