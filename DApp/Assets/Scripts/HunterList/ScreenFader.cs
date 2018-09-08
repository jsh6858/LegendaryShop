using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScreenFader : MonoBehaviour
{
    [HideInInspector] public float fadeOutSpeed;
    [HideInInspector] public float fadeInSpeed;

    public UnityAction onFadeOutComplete = null;
    public UnityAction onFadeInComplete = null;

    private Texture2D fadeTexture;
    private Color currentColor;

    private void Awake()
    {
        fadeTexture = new Texture2D(1, 1);
        currentColor = Color.clear;
        fadeTexture.SetPixel(0, 0, currentColor);
        fadeTexture.Apply();

        onFadeOutComplete = () => { };
    }

    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture, ScaleMode.StretchToFill, true);
    }

    public IEnumerator FadeOut()
    {
        while (fadeTexture.GetPixel(0, 0).a < 0.95f)
        {
            currentColor = Color.Lerp(currentColor, Color.black, fadeOutSpeed * Time.deltaTime);
            SetColor(currentColor);
            yield return null;
        }

        SetColor(Color.black);

        if (onFadeOutComplete != null)
        {
            onFadeOutComplete.Invoke();
        }
    }

    public IEnumerator FadeIn()
    {
        while (fadeTexture.GetPixel(0, 0).a > 0.05f)
        {
            currentColor = Color.Lerp(currentColor, Color.clear, fadeInSpeed * Time.deltaTime);
            SetColor(currentColor);
            yield return null;
        }

        SetColor(Color.clear);

        if (onFadeInComplete != null)
        {
            onFadeInComplete.Invoke();

        }
    }

    public void QuickSetFadeOut()
    {
        SetColor(Color.black);

        if(onFadeOutComplete != null)
        {
            onFadeOutComplete.Invoke();
        }
    }

    public void QuickSetFadeIn()
    {
        SetColor(Color.clear);

        if (onFadeInComplete != null)
        {
            onFadeInComplete.Invoke();
        }
    }

    private void SetColor(Color color)
    {
        fadeTexture.SetPixel(0, 0, color);
        fadeTexture.Apply();

        currentColor = color;
    }
}
