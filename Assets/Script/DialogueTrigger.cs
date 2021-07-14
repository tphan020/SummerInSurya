using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public Dialogue dialogue;
    public bool Ginny = false;
    public bool Choice = false;
    public List<string> SkipAmount = new List<string>();
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue,SkipAmount, Ginny, Choice);
        if (Ginny || Choice)
        {
            GameObject.Find("Ginny").GetComponent<Animator>().SetBool("IsOn", true);
        }
        else
        {
            GameObject.Find("Ginny").GetComponent<Animator>().SetBool("IsOn", false);
        }
    }
}
