using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startEffects : MonoBehaviour
{
    public GameObject blackScreen,logo, characterSelect;
    private CanvasFadeOut blackScreenCanvas,logoCanvas, characterSelectCanvas;
    public authClient auth;
    public DataHolder DataController;
    void Start()
    {
        DataController = GameObject.Find ("DATA").GetComponent<DataHolder>();
        blackScreenCanvas = blackScreen.GetComponent<CanvasFadeOut>();
        characterSelectCanvas = characterSelect.GetComponent<CanvasFadeOut>();
        logoCanvas = logo.GetComponent<CanvasFadeOut>();
        StartCoroutine(waiter());
    }
    IEnumerator waiter(){
        if(DataController.menu.characterCreator == false || DataController.menu.logged == false){
            yield return new WaitForSeconds(1);
            blackScreenCanvas.FadeOut();
            yield return new WaitForSeconds(3);
            logoCanvas.FadeIn();
            yield return new WaitForSeconds(2);
            
        } else if(DataController.menu.characterCreator == true){
            yield return new WaitForSeconds(1);
            blackScreenCanvas.FadeOut();

        }
        auth.init();
}
}
