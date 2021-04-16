using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFadeOut : MonoBehaviour
{
    public bool fIn;
    public bool fOut;
    public float speedIn = 0.1f;
    public float speedOut = 0.1f;
    public CanvasGroup canvas;

    void Start(){
        canvas = GetComponent<CanvasGroup>();
    }


    public void FadeOut(){
        fOut = true;
        fIn = false;
        canvas.blocksRaycasts = false;
    }
    public void FadeIn(){
        fIn = true;
        fOut = false;
        canvas.blocksRaycasts = true;
    }

    void FixedUpdate(){
        if(fOut == true){
            if(canvas.alpha > 0){
                canvas.alpha -= speedOut;
            } else {
                fOut = false;
            }
        }
        if(fIn == true){
            if(canvas.alpha < 1){
                canvas.alpha += speedIn;
            } else {
                fIn = false;
            }
        }
    }
}
