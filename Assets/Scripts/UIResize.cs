using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class UIResize : MonoBehaviour, IDragHandler
{

    [SerializeField]
    private RectTransform dragRect;
    [SerializeField]
    private CanvasScaler scaler;
    public Vector2 maxSize;
    public Vector2 minSize;
    public bool LockAspectRatio = false;
    private float aspectRatio;

    void Start(){
        aspectRatio = dragRect.sizeDelta.y/dragRect.sizeDelta.x;
    }

    public void OnDrag(PointerEventData eventData){
        if(dragRect.sizeDelta.x >= minSize.x && eventData.delta.x <= 0 || dragRect.sizeDelta.x <= maxSize.y && eventData.delta.x >= 0){
            if(dragRect.sizeDelta.x >= minSize.y && eventData.delta.y <= 0 || dragRect.sizeDelta.y <= maxSize.y && eventData.delta.y >= 0){
                if(!LockAspectRatio){
                    dragRect.sizeDelta += eventData.delta * scaler.referenceResolution.y / Screen.height;
                } else {
                    dragRect.sizeDelta += new Vector2(eventData.delta.x  * scaler.referenceResolution.y / Screen.height,eventData.delta.x  * scaler.referenceResolution.y / Screen.height * aspectRatio );
                }
            }
        }
    }
    
    void Update(){
        if(dragRect.sizeDelta.x < minSize.x) dragRect.sizeDelta = new Vector2(minSize.x,dragRect.sizeDelta.y);
        if(dragRect.sizeDelta.y < minSize.y) dragRect.sizeDelta = new Vector2(dragRect.sizeDelta.x,minSize.y);
        if(dragRect.sizeDelta.x > maxSize.x) dragRect.sizeDelta = new Vector2(maxSize.x,dragRect.sizeDelta.y);
        if(dragRect.sizeDelta.x > maxSize.x) dragRect.sizeDelta = new Vector2(dragRect.sizeDelta.x,maxSize.y);
        if(LockAspectRatio && dragRect.sizeDelta.x * aspectRatio != dragRect.sizeDelta.y) dragRect.sizeDelta = new Vector2(dragRect.sizeDelta.x,dragRect.sizeDelta.x * aspectRatio);
    }

}
