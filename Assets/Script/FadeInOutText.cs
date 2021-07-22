using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOutText : MonoBehaviour
{
    //Attach an Image you want to fade in the GameObject's Inspector
    public TextMeshProUGUI m_Image = null;
    public Text Image = null;
    //Use this to tell if the toggle returns true or false
    public bool m_Fading = false;
    public float durationOn = 1;

    void FixedUpdate()
    {
        //If the toggle returns true, fade in the Image
        if (m_Image != null)
        {
            if (m_Fading == true)
            {
                //Fully fade in Image (1) with the duration of 2
                m_Image.CrossFadeAlpha(1, durationOn, true);
            }
            //If the toggle is false, fade out to nothing (0) the Image with a duration of 2
            if (m_Fading == false)
            {
                m_Image.CrossFadeAlpha(0, .5f, true);
            }
        }
        else if (Image != null)
        {
            if (m_Fading == true)
            {
                //Fully fade in Image (1) with the duration of 2
                Image.CrossFadeAlpha(1, durationOn, true);
            }
            //If the toggle is false, fade out to nothing (0) the Image with a duration of 2
            if (m_Fading == false)
            {
                Image.CrossFadeAlpha(0, .5f, true);
            }
        }
    }
}
