using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void LoadNextDialogue()
    {
        GameObject.Find($"Trigger_{counter}").GetComponent<DialogueTrigger>().TriggerDialogue();
        counter++;
        FindObjectOfType<DialogueManager>().DisplayNextSentence();

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