using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class mouseLock : MonoBehaviour
{   

    private bool mouseLockMode;
    public bool canChange;
    private thirdPersonCamera mainCamera;
    private thirdPersonController playerController;
    
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        mouseLockMode = true;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        try {
            mainCamera = GameObject.Find("Camera").GetComponent<thirdPersonCamera>();
            playerController = GameObject.Find("Player").GetComponent<thirdPersonController>();
        }
        catch (Exception e) {
            return;
        }  
    }

    void Update()
    {
        if(Input.GetKeyDown("left ctrl") && canChange){
            ChangeLock();
        }
    }

    public void ChangeLock(){
        if(mouseLockMode){
            Cursor.lockState = CursorLockMode.Locked;
            mouseLockMode = false;
            if(mainCamera){
                mainCamera.enabled = true;
                playerController.canMove = true;
            }
        } else {
            Cursor.lockState = CursorLockMode.Confined;
            mouseLockMode = true;
            if(mainCamera){
                mainCamera.enabled = false;
                playerController.canMove = false;
            }
        }
    }
}
