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

    public Image instructions1;
    public Image instructions2;

    // Start is called before the first frame update
    void Start()
    {
        instructions = GameObject.FindWithTag("Instructions");
        instructions.SetActive(false);

        invisible = new Color(0f, 0f, 0f, 0f);
        visible = new Color(1f, 1f, 1f, 1f);
        instructions2.color = invisible;
    }

    // Update is called once per frame
    void Update()
    {
        if (instructions.activeSelf)
        {
            if (Input.anyKeyDown)
            {
                // Transition from page 1 to page 2
                if (instructions2.color == invisible)
                {
                    instructions1.color = invisible;
                    instructions2.color = visible;
                }

                // Transition from page 2 to the Start Menu
                else
                {
                    instructions.SetActive(false);
                }

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
