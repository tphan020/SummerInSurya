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
    public List<string> SoundFile;
    public bool End = false;
    public bool ThankYou = false;
    public bool Credit = false;
    public int PlayScene = 0;
    public bool DisplayedDialogue = false;
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(this);
        if ((Ginny || Choice) && (PlayScene == 0  || DisplayedDialogue))
        {
            GameObject.Find("Ginny").GetComponent<Animator>().SetBool("IsOn", true);
        }
        else
        {
            GameObject.Find("Ginny").GetComponent<Animator>().SetBool("IsOn", false);
        }
    }
}
