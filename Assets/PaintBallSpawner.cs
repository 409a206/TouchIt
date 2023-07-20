using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBallSpawner : MonoBehaviour
{
    private Vector3[] paintBallOriginPoses;
    private Color[] paintBallColors;
    private bool isSpawning = false;
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
    public void TrySpawnPaintBalls() {
       
        if(transform.childCount == 0 && !isSpawning) {
            isSpawning = true;
            StartCoroutine(SpawnPaintBallCoroutine());
        }
    }

    private IEnumerator SpawnPaintBallCoroutine()
    {
        Debug.Log("Starting SpawnPaintBallCoroutine");
        yield return new WaitForSeconds(3f);
        
        for (int i = 0; i < paintBallOriginPoses.Length; i++)
        {
            GameObject paintBall = Instantiate<GameObject>(Resources.Load("Prefabs/PaintBall") as GameObject);
            paintBall.transform.position = paintBallOriginPoses[i];
            paintBall.GetComponent<PaintBall>().SetPaintColor(paintBallColors[i]);
        }
        yield return new WaitForSeconds(3f); 
        isSpawning = false;
    }
}
