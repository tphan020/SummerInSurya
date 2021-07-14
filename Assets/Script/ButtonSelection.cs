using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonSelection : MonoBehaviour
{
    private bool mouse_over = false;
    private RectTransform recttransform;
    public int type = 0;
    private Vector2 GameSize = new Vector2();
    public DialogueManager Manager;
    public int SkipAmount = 0;

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
        }
        CheckPosition();
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
        Manager.SelectedChoices.Add(type);
        Manager.EndDialogue(SkipAmount);
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