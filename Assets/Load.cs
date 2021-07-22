using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour
{
    private float onTick = 0;
    public bool LoadScene = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        onTick += Time.fixedDeltaTime;
        if (onTick < 3)
        {
            return;
        }
        else if (!LoadScene)
        {
            LoadScene = true;
            SceneManager.LoadScene("MainMenu"); // start
        }
    }
}
