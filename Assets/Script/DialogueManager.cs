using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{

    public GameObject menu;
    public GameObject dialogueManager;
    //public ShowDialogue showDialogue;
    private Queue<string> sentences;
    // Start is called before the first frame update
    public GameObject GinnyDialogue;
    public GameObject RegularDialogue;
    public GameObject ChooseDialogue;
    public GameObject ChoiceA;
    public GameObject ChoiceB;
    public GameObject ChoiceC;
    public DateTime Cooldown = DateTime.UtcNow;
    private string CurrentPhase = "";
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue, bool IsGinny = false, bool IsChoice = false)
    {
        if (IsChoice)
        {
            ChangeChoosemenu();
            HandleChooseMenu(dialogue);
            return;
        }
        else if (IsGinny)
        {
            ChangeGinnymenu();
        }
        else
        {
            ChangeRegularmenu();
        }
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
    }

    public void HandleChooseMenu(Dialogue dialogue)
    {
        sentences.Clear();
        for (int i = 0; i < dialogue.sentences.Length; i++)
        {
            if (i == 0)
            {
                ChoiceA.GetComponent<Text>().text = dialogue.sentences[i];

            }
            else if (i == 1)
            {
                ChoiceB.GetComponent<Text>().text = dialogue.sentences[i];

            }
            else if ( i == 2)
            {
                ChoiceC.GetComponent<Text>().text = dialogue.sentences[i];
            }
        }
        sentences.Enqueue("");
    }

    public void ChangeGinnymenu()
    {
        CurrentPhase = "Ginny";
        RegularDialogue.SetActive(false);
        GinnyDialogue.SetActive(true);
        ChooseDialogue.SetActive(false);
        menu = GameObject.Find("GinnyText");
    }

    public void ChangeRegularmenu()
    {
        CurrentPhase = "Regular";
        GinnyDialogue.SetActive(false);
        RegularDialogue.SetActive(true);
        ChooseDialogue.SetActive(false);
        menu = GameObject.Find("RegularText");
    }
    public void ChangeChoosemenu()
    {
        CurrentPhase = "Choose";
        RegularDialogue.SetActive(false);
        GinnyDialogue.SetActive(false);
        ChooseDialogue.SetActive(true);
        //menu = GameObject.Find("GinnyText");
    }

    public void DisplayNextSentence()
    {
        if (DateTime.UtcNow.Subtract(Cooldown).TotalMilliseconds < 1000 && CurrentPhase != "Choose")
        {
            return;
        }
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        Cooldown = DateTime.UtcNow;
        string sentence = sentences.Dequeue();
        menu.GetComponent<FadeInOutText>().m_Fading = false;
        StartCoroutine(FadeInText(sentence));
    }

    IEnumerator FadeInText(string sentence)
    {
        // This will wait 1 second like Invoke could do, remove this if you don't need it
        yield return new WaitForSeconds(1);
        menu.GetComponent<TextMeshProUGUI>().text = sentence;
        menu.GetComponent<FadeInOutText>().m_Fading = true;
    }

    public void EndDialogue()
    {
        FindObjectOfType<StoryManager>().LoadNextDialogue();

        //GameObject.Find("DialogueBox").GetComponent<Animator>().SetBool("IsOpen", false);
    }

    public void makeChoice() {
        dialogueManager.SetActive(false);
        //showDialogue.Show();
        //menu.ShowMessageBox();
    }

    public void Hide() {
       // showDialogue.Show();
        dialogueManager.SetActive(false);
    }

    public void Show() {
        dialogueManager.SetActive(true);
    }
}
