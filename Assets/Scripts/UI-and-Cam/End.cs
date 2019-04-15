using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    public GameObject image1;
    public GameObject image2;
    GameObject video;
    bool played;
    bool splash;

    // Start is called before the first frame update
    void Start()
    {
        video = GameObject.FindWithTag("Video");
        video.SetActive(false);
        image2.SetActive(false);
        splash = false;
        played = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && played)
        {
            SceneManager.LoadScene("StartMenu");
        }
        
        else if (Input.anyKeyDown && !splash)
        {
            image1.SetActive(false);
            image2.SetActive(true);
            splash = true;
        }
        
        else if (Input.anyKeyDown && splash)
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
