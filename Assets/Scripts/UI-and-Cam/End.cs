using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    GameObject video;
    bool played;

    // Start is called before the first frame update
    void Start()
    {
        video = GameObject.FindWithTag("Video");
        video.SetActive(false);
        played = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && played)
        {
            SceneManager.LoadScene("StartMenu");
        }
        
        else if (Input.anyKeyDown)
        {
            Invoke("PlayVideo", 2f);
        }
    }

    void PlayVideo()
    {
        video.SetActive(true);
        played = true;
    }
}
