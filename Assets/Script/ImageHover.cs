using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ImageHover : MonoBehaviour
{
    private bool mouse_over = false;
    private RectTransform recttransform;
    public Image Source;
    public Sprite FirstImage;
    public Sprite SecondImage;
    public int type = 0;
    public bool switchedOn = false;
    public bool switchedOff = false;
    private Vector2 GameSize = new Vector2();

    public void Start()
    {
        recttransform = GetComponent<RectTransform>();
        GameSize = GetMainGameViewSize();
    }

    void Update()
    {
        if (mouse_over)
        {
            if (Input.GetMouseButtonDown(0))
            {
                DoInteraction();
            }
            if (!switchedOn)
            {
                switchedOn = true;
                Source.sprite = SecondImage;
            }
            switchedOff = false;
        }
        else
        {
            if (!switchedOff)
            {
                switchedOff = true;
                Source.sprite = FirstImage;
            }
            switchedOn = false;
        }
        CheckPosition();
    }

    public void SceneSwitch()
    {
        GameObject.Find("Ginny").GetComponent<MoveObject>().StartMoveObject(new Vector3(-4f, 0, 0), 2);
        Invoke("FadeEffect", 1);
        Invoke("SwitchScenes", 2);
    }

    public void FadeEffect()
    {
        GameObject.Find("BlackBackground").GetComponent<FadingTransition>().BeginFade(1);
        GameObject.Find("AudioManager").GetComponent<AudioManager>().Stop("MainMenu");
    }
    public void SwitchScenes()
    {
        SceneManager.LoadScene("MainMap"); // start
    }

public void DoInteraction()
    {
        /*if (type == 1)
        {
            SceneManager.LoadScene("Battle"); // start
        }
        else if (type == 2)
        {
            SceneManager.LoadScene("Tutorial"); // Tutorial
        }
        else if (type == 3)
        {
            Application.Quit();
        }*/
        SceneSwitch();
    }

    public static Vector2 GetMainGameViewSize()
    {
        System.Type T = System.Type.GetType("UnityEditor.GameView,UnityEditor");
        System.Reflection.MethodInfo GetSizeOfMainGameView = T.GetMethod("GetSizeOfMainGameView", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
        System.Object Res = GetSizeOfMainGameView.Invoke(null, null);
        return (Vector2)Res;
    }
    public void CheckPosition()
    {
        var GameSize = new Vector2(1920, 1080);
        var screenPoint = Input.mousePosition;
        //screenPoint = Camera.main.ScreenToWorldPoint(screenPoint);
        float ConvertedX = recttransform.sizeDelta.x / 2 * (GameSize.x / 1920);
        float ConvertedY = recttransform.sizeDelta.y / 2 * (GameSize.y / 1080);
        Vector2 Max = new Vector2(transform.position.x + ConvertedX, transform.position.y + ConvertedY);
        Vector2 Min = new Vector2(transform.position.x - ConvertedX, transform.position.y - ConvertedY);
        if (screenPoint.x < Max.x && screenPoint.x > Min.x && screenPoint.y < Max.y && screenPoint.y > Min.y)
        {
            mouse_over = true;
        }
        else
        {
            mouse_over = false;
        }
        //Instantiate(particle, transform.position, transform.rotation);
    }
}