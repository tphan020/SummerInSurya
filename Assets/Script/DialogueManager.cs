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
    public GameObject EndingDialogue;
    public GameObject ThankYouDialogue;
    public GameObject CreditDialogue;
    public GameObject ChoiceA;
    public GameObject ChoiceB;
    public GameObject ChoiceC; 
    public GameObject ChoiceAobj;
    public GameObject ChoiceBobj;
    public GameObject ChoiceCobj;
    public DateTime Cooldown = DateTime.UtcNow;
    private string CurrentPhase = "";
    public List<int> SelectedChoices = new List<int>();
    public List<string> SoundFiles = new List<string>();
    private int SoundIndex = 0;
    public int SkipNum = 0;
    public AudioSource SoundPlayer;
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(DialogueTrigger trigger)//Dialogue dialogue,List<string> SkipAmount, List<string>Soundfiles, bool IsGinny = false, bool IsChoice = false)
    {
        if (trigger.PlayScene != 0 && !trigger.DisplayedDialogue)
        {
            HandleScene(trigger);
            return;
        }
        SoundIndex = 0;
        SoundFiles = trigger.SoundFile;
        if (trigger.End || trigger.ThankYou || trigger.Credit)
        {
            ChangeEndMenu(trigger);
            HandleEndMenu(trigger);
            return;
        }
        else if (trigger.Choice)
        {
            ChangeChoosemenu();
            HandleChooseMenu(trigger.dialogue, trigger.SkipAmount);
            return;
        }
        else if (trigger.Ginny)
        {
            ChangeGinnymenu();
        }
        else
        {
            ChangeRegularmenu();
        }
        if (trigger.SkipAmount.Count > 0)
        {
            SkipNum = int.Parse(trigger.SkipAmount[0]);
        }
        else
        {
            SkipNum = 0;
        }
        sentences.Clear();
        foreach (string sentence in trigger.dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        if (trigger.DisplayedDialogue)
        {
            DisplayNextSentence();
        }
    }

    private void HandleScene(DialogueTrigger trigger)
    {
        CurrentPhase = "Scene";
        GinnyDialogue.SetActive(false);
        RegularDialogue.SetActive(false);
        ChooseDialogue.SetActive(false);
        if (trigger.PlayScene == 1)
        {
            GameObject.Find("FirstScene").GetComponent<FirstScene>().StartScene(trigger);
        }
        else if (trigger.PlayScene == 2)
        {
            GameObject.Find("SecondScene").GetComponent<SecondScene>().StartScene(trigger);
        }
        else if (trigger.PlayScene == 3)
        {
            GameObject.Find("ThirdScene").GetComponent<ThirdScene>().StartScene(trigger);
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
        if (dialogue.sentences.Length == 2)
        {
            ChoiceCobj.SetActive(false);
        }
        else
        {
            ChoiceCobj.SetActive(true);
        }
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
                ChoiceAobj.GetComponent<ButtonSelection>().SkipAmount = SkipVal == -1 ? GetSkipAmount(SkipAmount[0]) : SkipVal;
            }
            else if (i == 1)
            {
                ChoiceB.GetComponent<Text>().text = sentence;
                ChoiceBobj.GetComponent<ButtonSelection>().SkipAmount = SkipVal == -1 ? GetSkipAmount(SkipAmount[1]) : SkipVal;
            }
            else if ( i == 2)
            {
                ChoiceC.GetComponent<Text>().text = sentence;
                ChoiceCobj.GetComponent<ButtonSelection>().SkipAmount = SkipVal == -1 ? GetSkipAmount(SkipAmount[2]) : SkipVal;
            }
        }
        GameObject Title = GameObject.Find("Title");
        GameObject ObtainA = GameObject.Find("OptionAText");
        GameObject ObtainB = GameObject.Find("OptionBText");
        GameObject ObtainC = GameObject.Find("OptionCText");

        Title.GetComponent<FadeInOutText>().Image.CrossFadeAlpha(0, 0, true);
        ObtainA.GetComponent<FadeInOutText>().Image.CrossFadeAlpha(0, 0, true);
        ObtainB.GetComponent<FadeInOutText>().Image.CrossFadeAlpha(0, 0, true);
        Title.GetComponent<FadeInOutText>().m_Fading = true;
        ObtainA.GetComponent<FadeInOutText>().m_Fading = true;
        ObtainB.GetComponent<FadeInOutText>().m_Fading = true;
        if (ObtainC != null)
        {
            ObtainC.GetComponent<FadeInOutText>().Image.CrossFadeAlpha(0, 0, true);
            ObtainC.GetComponent<FadeInOutText>().m_Fading = true;
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
    public void ChangeEndMenu(DialogueTrigger trigger)
    {
        CurrentPhase = "End";
        RegularDialogue.SetActive(false);
        GinnyDialogue.SetActive(false);
        ChooseDialogue.SetActive(false);
        if (trigger.End)
        {
            EndingDialogue.SetActive(true);
        }
        else if (trigger.ThankYou)
        {
            EndingDialogue.SetActive(false);
            ThankYouDialogue.SetActive(true);
        }
        else if (trigger.Credit)
        {
            EndingDialogue.SetActive(false);
            ThankYouDialogue.SetActive(false);
            CreditDialogue.SetActive(true);
        }
    }
    public void HandleEndMenu(DialogueTrigger trigger)
    {
        if (trigger.End)
        {
            GameObject EndRegularOb = GameObject.Find("EndRegularText");
            GameObject EndBoldOb = GameObject.Find("EndBoldText");
            string EndText = EndRegularOb.GetComponent<Text>().text;
            //EndRegularOb.GetComponent<Text>().text = "";
            string BoldText = EndBoldOb.GetComponent<Text>().text;
            //EndBoldOb.GetComponent<Text>().text = "";
            EndRegularOb.GetComponent<FadeInOutText>().Image.CrossFadeAlpha(0, 0, true);
            EndBoldOb.GetComponent<FadeInOutText>().Image.CrossFadeAlpha(0, 0, true);
            EndRegularOb.GetComponent<FadeInOutText>().durationOn = 2f;
            EndBoldOb.GetComponent<FadeInOutText>().durationOn = 2f;
            EndRegularOb.GetComponent<FadeInOutText>().m_Fading = true;
            EndBoldOb.GetComponent<FadeInOutText>().m_Fading = true;
        }
        else if (trigger.ThankYou)
        {
            GameObject TYRegularOb = GameObject.Find("TYRegularText");
            GameObject TYBoldOb = GameObject.Find("TYBoldText");
            string TYText = TYRegularOb.GetComponent<Text>().text;
            //EndRegularOb.GetComponent<Text>().text = "";
            string TYBoldText = TYBoldOb.GetComponent<Text>().text;
            //EndBoldOb.GetComponent<Text>().text = "";
            TYRegularOb.GetComponent<FadeInOutText>().Image.CrossFadeAlpha(0, 0, true);
            TYBoldOb.GetComponent<FadeInOutText>().Image.CrossFadeAlpha(0, 0, true);
            TYRegularOb.GetComponent<FadeInOutText>().durationOn = 2f;
            TYBoldOb.GetComponent<FadeInOutText>().durationOn = 2f;
            TYRegularOb.GetComponent<FadeInOutText>().m_Fading = true;
            TYBoldOb.GetComponent<FadeInOutText>().m_Fading = true;
        }
        else if (trigger.Credit)
        {
            GameObject CredRegularOb = GameObject.Find("CredRegularText");
            GameObject CredBoldOb = GameObject.Find("CredBoldText");
            string CredText = CredRegularOb.GetComponent<Text>().text;
            //EndRegularOb.GetComponent<Text>().text = "";
            string CredBoldText = CredBoldOb.GetComponent<Text>().text;
            //EndBoldOb.GetComponent<Text>().text = "";
            CredRegularOb.GetComponent<FadeInOutText>().Image.CrossFadeAlpha(0, 0, true);
            CredBoldOb.GetComponent<FadeInOutText>().Image.CrossFadeAlpha(0, 0, true);
            CredRegularOb.GetComponent<FadeInOutText>().durationOn = 2f;
            CredBoldOb.GetComponent<FadeInOutText>().durationOn = 2f;
            CredRegularOb.GetComponent<FadeInOutText>().m_Fading = true;
            CredBoldOb.GetComponent<FadeInOutText>().m_Fading = true;
        }
        sentences.Enqueue("");
    }
    public void DisplayNextSentence()
    {
        if (DateTime.UtcNow.Subtract(Cooldown).TotalMilliseconds < 1500 || CurrentPhase == "Choose" || CurrentPhase == "Scene")
        {
            return;
        }
        if (sentences.Count == 0)
        {
            EndDialogue(SkipNum);
            return;
        }
        if (SoundFiles.Count > 0 && SoundFiles.Count > SoundIndex)
        {
            string sound = SoundFiles[SoundIndex];

            if (!String.IsNullOrEmpty(sound))
            {
                StartCoroutine(DelayedSound(sound));
            }
            SoundIndex++;
        }
        Cooldown = DateTime.UtcNow;
        string sentence = sentences.Dequeue();
        menu.GetComponent<FadeInOutText>().m_Fading = false;
        StartCoroutine(FadeInText(sentence));
    }

    IEnumerator DelayedSound(string sound)
    {
        // This will wait 1 second like Invoke could do, remove this if you don't need it
        yield return new WaitForSeconds(.75f);
        ((AudioPlayer)FindObjectOfType(typeof(AudioPlayer))).PlaySound(sound);
    }

    IEnumerator FadeInText(string sentence)
    {
        // This will wait 1 second like Invoke could do, remove this if you don't need it
        yield return new WaitForSeconds(1);
        menu.GetComponent<Text>().text = sentence;
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
