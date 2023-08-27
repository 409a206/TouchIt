using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameMenuManager : MonoBehaviour
{
    public GameObject menu;
    public GameObject finishCanvas;
    public InputActionProperty showButton;
    public Transform head;
    public float spawnDistance = 2f;
    public GameObject PaintBallSpawner;
    
    // Update is called once per frame
    void Update()
    {
        if(showButton.action.WasPressedThisFrame()) {
            menu.SetActive(!menu.activeSelf);
            menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
        }

        menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y,head.position.z));
        menu.transform.forward *= -1;

        TryShowClearCanvas();
    }

    private void TryShowClearCanvas()
    {
        if(PaintBallSpawner.transform.childCount == 0) {
            finishCanvas.SetActive(true);
            finishCanvas.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
            finishCanvas.transform.LookAt(new Vector3(head.position.x, finishCanvas.transform.position.y,head.position.z));
            finishCanvas.transform.forward *= -1;
            StartCoroutine(TimerCoroutine());
        }
    }

    private IEnumerator TimerCoroutine()
    {
        yield return new WaitForSeconds(2f);
        finishCanvas.SetActive(false);
    }
}
