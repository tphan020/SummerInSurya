using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    // Start is called before the first frame update    
    public AudioClip Ring;
    public AudioClip Turbine;
    public AudioClip Train;
    public AudioClip Select;
    public AudioClip QuietParkBirds;
    public AudioClip PowerOn;
    public AudioClip MapOpening;
    public AudioClip MapClosing;
    public AudioClip HoverButton;
    public AudioClip HighSpeedTrain;
    public AudioClip Construction;
    public AudioClip CarEngine;
    public AudioClip CarDriving;
    public AudioClip BusyStreet;
    public AudioClip Bus;
    public AudioClip BicycleDriving;
    public AudioClip BackButton; 
    public AudioClip Happy;
    public AudioClip Sad;
    public AudioSource AudioSource;
    void Start()
    {
        
    }

    public void PlaySound(string type)
    {
        switch(type)
        {
            case "Ring":
                AudioSource.PlayOneShot(Ring, 1.0f);
                break;
            case "Turbine":
                AudioSource.PlayOneShot(Turbine, 1.0f);
                break;
            case "Train":
                AudioSource.PlayOneShot(Train, 1.0f);
                break;
            case "Select":
                AudioSource.PlayOneShot(Select, 1.0f);
                break;
            case "QuietParkBirds":
                AudioSource.PlayOneShot(QuietParkBirds, 1.0f);
                break;
            case "PowerOn":
                AudioSource.PlayOneShot(PowerOn, 1.0f);
                break;
            case "MapOpening":
                AudioSource.PlayOneShot(MapOpening, 1.0f);
                break;
            case "MapClosing":
                AudioSource.PlayOneShot(MapClosing, 1.0f);
                break;
            case "HoverButton":
                AudioSource.PlayOneShot(HoverButton, 1.0f);
                break;
            case "HighSpeedTrain":
                AudioSource.PlayOneShot(HighSpeedTrain, 1.0f);
                break;
            case "CarEngine":
                AudioSource.PlayOneShot(CarEngine, 1.0f);
                break;
            case "CarDriving":
                AudioSource.PlayOneShot(CarDriving, 1.0f);
                break;
            case "BusyStreet":
                AudioSource.PlayOneShot(BusyStreet, 1.0f);
                break;
            case "Bus":
                AudioSource.PlayOneShot(Bus, 1.0f);
                break;
            case "BicycleDriving":
                AudioSource.PlayOneShot(BicycleDriving, 1.0f);
                break;
            case "BackButton":
                AudioSource.PlayOneShot(BackButton, 1.0f);
                break;
            case "Construction":
                AudioSource.PlayOneShot(Construction, 1.0f);
                break;
            case "Sad":
                AudioSource.PlayOneShot(Sad, 1.0f);
                break;
            case "Happy":
                AudioSource.PlayOneShot(Happy, 1.0f);
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
