using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeMap : MonoBehaviour
{
    private CanvasGroup cg;
    public float fadeTime = 1f;
    float accumTime = 0f;
    private Coroutine fadeCor;

    public void Awake() 
    {
        cg = gameObject.GetComponent<CanvasGroup>();
        StartFadeOut();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ToInside"))
        {
            if (other.name == "BakeryPoint")
            {
                SceneManager.LoadScene("BakeryShop");
            }
            else if (other.name == "WhitePoint")
            {
                SceneManager.LoadScene("cafe_white_ver");
            }
            else if (other.name == "BrownPoint")
            {
                SceneManager.LoadScene("Garden");
            }
            else if (other.name == "ChristmasPoint")
            {
                SceneManager.LoadScene("Christmas");
            }
            else if (other.name == "BlackPoint")
            {
                SceneManager.LoadScene("cafe_black_ver");
            }
        }

        if (other.gameObject.CompareTag("ToOutside"))
        {
            print("ToOutside �±׵�");
            SceneManager.LoadScene("MainMap");
        }

    }
    public void StartFadeOut()
    {
        if (fadeCor != null) 
        {
            StopAllCoroutines();
            fadeCor = null;
        }
        fadeCor = StartCoroutine(FadeOut());
    }
    private IEnumerator FadeOut()
    {
        //yield return new WaitForSeconds(1.0f);
        accumTime = 0f;
        while (accumTime < fadeTime)
        {
            cg.alpha = Mathf.Lerp(1f, 0f, accumTime / fadeTime);
            yield return 0;
            accumTime += Time.deltaTime;
        }
        cg.alpha = 0f;
    }
}