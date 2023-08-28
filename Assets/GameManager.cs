using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.InputSystem;
using System;

public class GameManager : MonoBehaviour
{
    public ActionBasedSnapTurnProvider snapTurn;
    public ActionBasedContinuousTurnProvider continuousTurn;
    public InputActionProperty resetButton;
    public SoundManager soundManager;

    public void SetTurnTypeFromIndex(int index) {
        if(index == 0) {
            snapTurn.enabled = false; 
            continuousTurn.enabled = true;
        } else if(index == 1) {
            snapTurn.enabled = true;
            continuousTurn.enabled = false;
        }
    }

    public void ResetCurrentScene() {
        SceneManager.LoadScene(Application.loadedLevelName);
    }
    public void ExitGame() {

        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #endif
        
        Application.Quit();
    }

    // Start is called before the first frame update
    void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        TryResetScene();
    }

    private void TryResetScene()
    {
        if(resetButton.action.WasPressedThisFrame()) {
            ResetCurrentScene();
        }
    }
}
