using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFadeOut : MonoBehaviour
{
 public float fadeOutTime;
 public  Color color;
 public IEnumerator co;

 public static string errorText;


    void Start(){
        co = FadeOutRoutine("pies");
    }
         public void FadeOut(string Text)
         {

             StopCoroutine(co);
            co = FadeOutRoutine(Text);
             StartCoroutine(co);
         }
         private IEnumerator FadeOutRoutine(string Text)
         { 
             Text text = GetComponent<Text>();
             text.text = Text;
             text.color = color;
             Color originalColor = text.color;
             yield return new WaitForSeconds(2);
             for (float t = 0.01f; t < fadeOutTime; t += Time.deltaTime)
             {
                 text.color = Color.Lerp(originalColor, Color.clear, Mathf.Min(1, t/fadeOutTime));
                 yield return null;
             }
         }
}
