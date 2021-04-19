using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class mouseLock : MonoBehaviour
{   
    public bool locked;
    private bool mouseLockMode;
    public bool canChange;
    public thirdPersonCamera mainCamera;
    public thirdPersonController playerController;
    
    
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Confined;
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
        if(mainCamera != null){
            if(Input.GetKeyDown("left ctrl") && canChange){
                locked = !locked;
            }

            if(locked != mouseLockMode){
                mouseLockMode = locked;
                mainCamera.rotationLocked = locked;
                if(locked){
                    Cursor.lockState = CursorLockMode.Confined;
                } else {
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }
        }
    }

    public void ChangeLock(){
        if(mouseLockMode){
            Cursor.lockState = CursorLockMode.Locked;
            mouseLockMode = false;
            if(mainCamera){
                mainCamera.rotationLocked = false;
            }
        } else {
            Cursor.lockState = CursorLockMode.Confined;
            mouseLockMode = true;
            if(mainCamera){
                mainCamera.rotationLocked = true;
            }
        }
    }
}
