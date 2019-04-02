using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    public void LoadStartMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
    public void LoadFirstLevel()
    {
        SceneManager.LoadScene("FirstLevel");
    }
    public void LoadSecondLevel()
    {
        SceneManager.LoadScene("SecondLevel");
    }
}
