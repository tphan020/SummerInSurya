using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadingTransition : MonoBehaviour
{
    public Texture2D FadeOutTexture;
    public float fadeSpeed = 0.8f;
    private int drawDepth = -1000;
    private float alpha = 10f;
    private int fadeDir = -1;
    public bool SkipIntro = false;

    private void OnGUI()
    {        
        alpha += fadeDir * fadeSpeed * Time.deltaTime;
        alpha = Mathf.Clamp01(alpha);
        if (SkipIntro && fadeDir == -1)
        {
            return;
        }
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.depth = drawDepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), FadeOutTexture);
    }

    public float BeginFade(int direction)
    {
        fadeDir = direction;
        return fadeSpeed;
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!SkipIntro)
        {
            BeginFade(-1);
        }
    }
}
