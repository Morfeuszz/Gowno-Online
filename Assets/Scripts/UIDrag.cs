using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class UIDrag : MonoBehaviour, IDragHandler
{
    [SerializeField]
    private RectTransform dragRect;
    [SerializeField]
    private CanvasScaler scaler;
    private float borderXrescall,borderYrescall;
    private float UPDTborderXrescall,UPDTborderYrescall;
    
    public void OnDrag(PointerEventData eventData){
        borderXrescall = (dragRect.sizeDelta.x * Screen.height/scaler.referenceResolution.y) + (dragRect.anchoredPosition.x *  Screen.height/scaler.referenceResolution.y);
        borderYrescall = (dragRect.sizeDelta.y * Screen.height/scaler.referenceResolution.y) + (dragRect.anchoredPosition.y *  Screen.height/scaler.referenceResolution.y);
        if(dragRect.anchoredPosition.x > 0f && eventData.delta.x < 0f || borderXrescall < Screen.width && eventData.delta.x > 0){
            dragRect.anchoredPosition +=  new Vector2(eventData.delta.x * scaler.referenceResolution.y / Screen.height,0f);
        }
        if(dragRect.anchoredPosition.y > 0f && eventData.delta.y < 0f || borderYrescall < Screen.height && eventData.delta.y > 0){
            dragRect.anchoredPosition += new Vector2(0f,eventData.delta.y * scaler.referenceResolution.y / Screen.height);
        }
    }

    void Update(){
        UPDTborderXrescall = (dragRect.sizeDelta.x * Screen.height/scaler.referenceResolution.y) + (dragRect.anchoredPosition.x *  Screen.height/scaler.referenceResolution.y);
        UPDTborderYrescall = (dragRect.sizeDelta.y * Screen.height/scaler.referenceResolution.y) + (dragRect.anchoredPosition.y *  Screen.height/scaler.referenceResolution.y);
        if(dragRect.anchoredPosition.x < 0f){
            dragRect.anchoredPosition = new Vector2(0f,dragRect.anchoredPosition.y);
        }else if(UPDTborderXrescall > Screen.width + 5){
            dragRect.anchoredPosition = new Vector2((scaler.referenceResolution.y/Screen.height*Screen.width) - dragRect.sizeDelta.x,dragRect.anchoredPosition.y);
        }else if(dragRect.anchoredPosition.y < 0f){
            dragRect.anchoredPosition = new Vector2(dragRect.anchoredPosition.x,0f);
        }else if(UPDTborderYrescall > Screen.height + 5){
            dragRect.anchoredPosition = new Vector2(dragRect.anchoredPosition.x,(scaler.referenceResolution.y) - dragRect.sizeDelta.y);
        }
    }


}
 