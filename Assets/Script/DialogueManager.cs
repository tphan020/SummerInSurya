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
    public List<int> SelectedChoices = new List<int>();
    public int SkipNum = 0;
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue,List<string> SkipAmount, bool IsGinny = false, bool IsChoice = false)
    {
        if (IsChoice)
        {
            ChangeChoosemenu();
            HandleChooseMenu(dialogue, SkipAmount);
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
        if (SkipAmount.Count > 0)
        {
            SkipNum = int.Parse(SkipAmount[0]);
        }
        else
        {
            SkipNum = 0;
        }
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
    }
    public string Between(string STR, string FirstString, string LastString)
    {
        string FinalString;
        int Pos1 = STR.IndexOf(FirstString) + FirstString.Length;
        int Pos2 = STR.IndexOf(LastString);
        FinalString = STR.Substring(Pos1, Pos2 - Pos1);
        return FinalString;
    }

    public string HardcodedSection(string type)
    {
        if (type.Contains("*HardcodedSectionP2*"))
        {
            if (SelectedChoices[0] == 3)
            {
                return "Drive on";
            }
            else if (SelectedChoices[0] == 2)
            {
                return "Ride your bicycle";
            }
            else
            {
                return "Walk on";
            }
        }
        if (type.Contains("*HardcodedSectionP3*"))
        {
            if (SelectedChoices[0] == 3 && SelectedChoices[1] == 3)
            {
                return "Drive on";
            }
            else
            {
                return "Rent a car";
            }
        }
        else
        {
            return "";
        }
    }
    public int HardcodedSectionSkip(string type)
    {
        if (type.Contains("*HardcodedSectionP2Bus*"))
        {
            if (SelectedChoices[0] == 2)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
        else if (type.Contains("*HardcodedSectionP2Choice*"))
        {
            if (SelectedChoices[0] == 3)
            {
                return 3;
            }
            else
            {
                return 4;
            }
        }
        else if (type.Contains("*HardcodedSectionP3Choice*"))
        {
            if (SelectedChoices[0] == 3)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
        else
        {
            return 0;
        }
    }
    public void HandleChooseMenu(Dialogue dialogue, List<string> SkipAmount)
    {
        sentences.Clear();
        for (int i = 0; i < dialogue.sentences.Length; i++)
        {
            int SkipVal = -1;
            string sentence = dialogue.sentences[i];
            if (SkipAmount[i].Contains("*HardcodedSection"))
            {
                SkipVal = HardcodedSectionSkip(SkipAmount[i]);
            }
            if (sentence.Contains("*HardcodedSection"))
            {
                sentence = HardcodedSection(sentence);
            }
            else
            {
                if (dialogue.sentences[i].Contains("*"))
                {
                    string parshedSent = Between(sentence, "*", "/*");
                    int index = int.Parse(parshedSent[0].ToString());
                    int selection = SelectedChoices[index];
                    int option1 = int.Parse(parshedSent[2].ToString());
                    int option2 = int.Parse(parshedSent[parshedSent.IndexOf(",") + 1].ToString());
                    string option1str = parshedSent.Substring(4, parshedSent.IndexOf(",") - 4);
                    string option2str = parshedSent.Substring(parshedSent.IndexOf(",") + 3, parshedSent.Length - parshedSent.IndexOf(",") - 3);
                    if (selection == option1)
                    {
                        sentence = sentence.Substring(0, sentence.IndexOf("*")) + option1str + sentence.Substring(sentence.IndexOf("/") + 1, sentence.Length - 1 - sentence.IndexOf("/") - 1);
                    }
                    else
                    {
                        sentence = sentence.Substring(0, sentence.IndexOf("*")) + option2str + sentence.Substring(sentence.IndexOf("/") + 1, sentence.Length - 1 - sentence.IndexOf("/") - 1);
                    }
                }
            }
            if (i == 0)
            {
                ChoiceA.GetComponent<Text>().text = sentence;
                GameObject.Find("OptionA").GetComponent<ButtonSelection>().SkipAmount = SkipVal == -1 ? GetSkipAmount(SkipAmount[0]) : SkipVal;
            }
            else if (i == 1)
            {
                ChoiceB.GetComponent<Text>().text = sentence;
                GameObject.Find("OptionB").GetComponent<ButtonSelection>().SkipAmount = SkipVal == -1 ? GetSkipAmount(SkipAmount[1]) : SkipVal;
            }
            else if ( i == 2)
            {
                ChoiceC.GetComponent<Text>().text = sentence;
                GameObject.Find("OptionC").GetComponent<ButtonSelection>().SkipAmount = SkipVal == -1 ? GetSkipAmount(SkipAmount[2]) : SkipVal;
            }
        }
        if (dialogue.sentences.Length == 2)
        {
            GameObject.Find("OptionC").SetActive(false);
        }
        else
        {
            GameObject.Find("OptionC").SetActive(true);
        }
        sentences.Enqueue("");
    }
    public int GetSkipAmount(string amt)
    {
        if (amt.Contains("*"))
        {
            string parshedSent = Between(amt, "*", "/*");
            int index = int.Parse(parshedSent[0].ToString());
            int selection = SelectedChoices[index];
            int option1 = int.Parse(parshedSent[2].ToString());
            int option2 = int.Parse(parshedSent[parshedSent.IndexOf(",") + 1].ToString());
            string option1str = parshedSent.Substring(4, parshedSent.IndexOf(",") - 4);
            string option2str = parshedSent.Substring(parshedSent.IndexOf(",") + 3, parshedSent.Length - parshedSent.IndexOf(",") - 3);
            if (selection == option1)
            {
                return int.Parse(option1str);
            }
            else
            {
                return int.Parse(option2str);
            }
        }
        return int.Parse(amt);
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
    }

    public void DisplayNextSentence()
    {
        if (DateTime.UtcNow.Subtract(Cooldown).TotalMilliseconds < 1000 || CurrentPhase == "Choose")
        {
            return;
        }
        if (sentences.Count == 0)
        {
            EndDialogue(SkipNum);
            SkipNum = 0;
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

    public void EndDialogue(int skipamount = 0)
    {
        FindObjectOfType<StoryManager>().LoadNextDialogue(skipamount);

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
