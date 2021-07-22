using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageLeftToRight : MonoBehaviour
{
    // Start is called before the first frame update
    public Image Image;
    private float onTick = 0;
    private float SkipTime = 1.5f;

    private DateTime WaitTime = DateTime.UtcNow;
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        onTick += Time.fixedDeltaTime;
        if (onTick > SkipTime && Image.fillAmount < 1)
        {
            Image.fillAmount += .025f;
        }
    }
}
