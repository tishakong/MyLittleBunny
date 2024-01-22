using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FadeState { FadeIn = 0, FadeOut, FadeInOut, FadeLoop }
public class FadeEffect : MonoBehaviour
{
    [SerializerField]
    [Range(0.01f, 10f)]
    private float fadeTime;
    
    [SerializeField]
    private AnimationCurve fadeCurve;
    private Image image;
    private FadeState fadeState;

    private void Awake()
    {
        image = GetComponent<image>();
        
        // Fade In.
        // StartCoroutine(Fade(1,0));

        // Fade Out.
        // StartCoroutine(Fade(0,1));

        OnFade(fadeState.FadeInOut);
    }

    public void OnFade(FadeState state)
    {
        fadeState = state;

        switch ( fadeState )
        {
            case FadeState.FadeIn:
                StartCoroutine(Fade(1,0));
                break;
            case FadeState.FadeOut:
                StartCoroutine(Fade(0,1));
                break;
            case FadeState.FadeInOut:
                StartCoroutine(FadeInOut());
                break;
        }
    }

    private IEnumerator FadeInOut()
    {
        while (true)
        {
            yield return StartCoroutine(Fade(1,0));

            yield return StartCoroutine(Fade(0,1));

            if (fadeState == FadeState.FadeInOut) 
            {
                break;
            }
        }
    }
    private IEnumerator Fade(float start, float end)
    {

    }
}
