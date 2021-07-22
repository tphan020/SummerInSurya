using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Queue<string> sentences;
    public int StoryIndex = 0;
    int counter = 1;
    void Start()
    {
        sentences = new Queue<string>();
        GameObject.Find("Trigger").GetComponent<DialogueTrigger>().TriggerDialogue();
    }

    public void LoadNextDialogue(int skipamount = 0)
    {
        if (skipamount != 0)
        {
            counter += skipamount;
        }
        GameObject trigger = GameObject.Find($"Trigger_{counter}");
        if (trigger != null)
        {
            trigger.GetComponent<DialogueTrigger>().TriggerDialogue();
            counter++;
            FindObjectOfType<DialogueManager>().menu.GetComponent<Text>().text = "";
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
        }
        else
        {
            SceneManager.LoadScene("MainMenu"); // start
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FindObjectOfType<DialogueManager>().DisplayNextSentence();

            //FindObjectOfType<DialogueTrigger>().TriggerDialogue();
        }
        /*else if (Input.GetMouseButtonDown(1))
        {
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
        }*/
    }
}