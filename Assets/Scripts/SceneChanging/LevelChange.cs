using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    public string SceneName;

    public void TransitionTo(string levelName)
    {
        StartCoroutine(GameObject.FindObjectOfType<Transition>().FadeAndLoadScene(Transition.FadeDirection.Out,levelName));
    }
    public void LoadStartMenu()
    {
        TransitionTo("StartMenu");
    }
    public void LoadFirstLevel()
    {
        TransitionTo("FirstLevel");
    }
    public void LoadSecondLevel()
    {
        TransitionTo("SecondLevel");
    }

    public void OnTriggerEnter()
    {
        TransitionTo(SceneName);
    }
}
