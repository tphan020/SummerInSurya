using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstScene : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Path;
    public GameObject Player;
    public PathFollower Follower;
    public Camera MainCamera;
    public Vector3 CamPosition = new Vector3(-40, -16, -10);
    public Vector3 StartPos;
    public bool MoveCamera = false;
    public bool ReverseCamera = false;
    float Timer;
    float percent = 0;
    public float StartZ = 10;
    public float EndZ = 5;
    public DialogueTrigger UsedTrigger;


    void Start()
    {
        StartPos = MainCamera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (MoveCamera)
        {
            Timer += Time.deltaTime;
            MainCamera.transform.position = Vector3.Lerp(StartPos, CamPosition, Timer);
            percent = Timer / 1;
            // multiply the percentage to the difference of our two positions
            // and add to the start
            MainCamera.orthographicSize = StartZ - EndZ * percent;
            if (Timer >= 1)
            {
                MoveCamera = false;
                Follower.SetPositionHolder();
                Follower.canMove(GetMovementSpeed());
                Timer = 0;                
            }
        }
        else if (ReverseCamera)
        {
            Timer += Time.deltaTime;
            MainCamera.transform.position = Vector3.Lerp(CamPosition, StartPos, Timer);
            percent = Timer / 1;
            // multiply the percentage to the difference of our two positions
            // and add to the start
            MainCamera.orthographicSize = StartZ - EndZ * (1 - percent);
            if (Timer >= 1)
            {
                Path.SetActive(false);
                Player.SetActive(false);
                ReverseCamera = false;
                UsedTrigger.DisplayedDialogue = true;
                UsedTrigger.TriggerDialogue();
            }
        }
    }
    public float GetMovementSpeed()
    {
        List<int> Choices = GameObject.Find("DialogueManager").GetComponent<DialogueManager>().SelectedChoices;
        if (Choices[0] == 3)
        {
            ((AudioPlayer)FindObjectOfType(typeof(AudioPlayer))).PlaySound("CarDriving");
            return 3f;
        }
        else if (Choices[1] == 1)
        {
            ((AudioPlayer)FindObjectOfType(typeof(AudioPlayer))).PlaySound("Bus");
            return 3f;
        }
        else if (Choices[0] == 2)
        {
            ((AudioPlayer)FindObjectOfType(typeof(AudioPlayer))).PlaySound("BicycleDriving");
            return 2.5f;
        }
        else if (Choices[1] == 1)
        {
            //((AudioPlayer)FindObjectOfType(typeof(AudioPlayer))).PlaySound("Select");
            return 2f;
        }
        return 1f;
    }
    public void StartScene(DialogueTrigger trigger)
    {
        UsedTrigger = trigger;
        Path.SetActive(true);
        Player.SetActive(true);
        Follower.Scene = 1;
        Timer = 0;
        MoveCamera = true;
    }
}