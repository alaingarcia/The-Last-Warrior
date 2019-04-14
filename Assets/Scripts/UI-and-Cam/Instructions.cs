using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Instructions : MonoBehaviour
{
    GameObject instructions;

    private Color invisible;
    private Color visible;

    // Start is called before the first frame update
    void Start()
    {
        instructions = GameObject.FindWithTag("Instructions");
        instructions.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (instructions.activeSelf)
        {
            if (Input.anyKeyDown)
            {
                instructions.SetActive(false);
            }
        }
    }
    
    IEnumerator Wait(int seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
    }

    public void Show()
    {
        instructions.SetActive(true);
    }
}
