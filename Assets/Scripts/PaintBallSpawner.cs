using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBallSpawner : MonoBehaviour
{
    private Vector3[] paintBallOriginPoses;
    private Color[] paintBallColors;
    private bool isSpawning = false;
    private float lerpTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        PaintBall[] paintBalls = GetComponentsInChildren<PaintBall>();
        paintBallOriginPoses = new Vector3[paintBalls.Length];
        paintBallColors = new Color[paintBalls.Length];
        for (int i = 0; i < paintBalls.Length; i++)
        {
            paintBallOriginPoses[i] = paintBalls[i].transform.position;
            paintBallColors[i] = paintBalls[i].GetComponent<PaintBall>().color;
        }
    }

    private void Update() {
         TrySpawnPaintBalls();
    }

    private void TrySpawnPaintBalls() {
        if(transform.childCount == 0 && !isSpawning) {
            //StartCoroutine(SpawnPaintBallsCoroutine());
            //isSpawning = true;
            
        }
    }

    private IEnumerator SpawnPaintBallsCoroutine()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("SpawnPaintBallCoroutine Called");
        this.GetComponent<AudioSource>().PlayOneShot(FindObjectOfType<SoundManager>().effectSounds[2].clip);
        for (int i = 0; i < paintBallOriginPoses.Length; i++)
        {
            GameObject paintBall = Instantiate<GameObject>(Resources.Load("Prefabs/PaintBall") as GameObject);
            paintBall.transform.position = paintBallOriginPoses[i];
            paintBall.GetComponent<PaintBall>().SetPaintColor(paintBallColors[i]);
            paintBall.transform.SetParent(this.transform);
            StartCoroutine(PaintBallDissolveInCoroutine(paintBall));
        }
        isSpawning = false;
    }

    //dissolve 관련 로직 실행
    private IEnumerator PaintBallDissolveInCoroutine(GameObject paintBall)
    {
            float elapsedTime = 0f;
            while(elapsedTime < lerpTime) {
            elapsedTime += Time.deltaTime;

            if(elapsedTime >= lerpTime) {
                elapsedTime = lerpTime;
            }

            paintBall?.GetComponent<Renderer>().material.SetFloat("_Alpha_Clip_Threshold", Mathf.Lerp(1f,0f, elapsedTime/lerpTime));
            yield return null;
            }
    }
}
