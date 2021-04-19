using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class loadingScreen : MonoBehaviour
{


    public bool test = true;
    public Slider testSlider;

    void Start(){
        if(test==true){
            LoadLevel();
        }

    }

    public void LoadLevel ()
    {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync ()
    {
        if(test==true){
            AsyncOperation op = SceneManager.LoadSceneAsync("menuNew");
            while ( !op.isDone )
        {
            float progress = Mathf.Clamp01(op.progress / .9f);
            testSlider.value = progress;

            yield return null;
        }
        
        } else {
            AsyncOperation op = SceneManager.LoadSceneAsync("LoadingScreen");
            while ( !op.isDone )
        {
            float progress = Mathf.Clamp01(op.progress / .9f);
            Debug.Log(progress);
            


            yield return null;


        }
        

        
    }
    }
}
