using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// CREDIT TO CODEMONKEY: https://www.youtube.com/watch?v=nNbM40HFyCs
public class BlackBars : MonoBehaviour
{
    // CINEMATIC BAR STUFF
    private bool active = false;
    private RectTransform topBar, bottomBar;
    private float changeSizeAmount, targetSize;

    void Start()
    {
        GameObject gameObject = new GameObject("topBar", typeof(Image));
        gameObject.transform.SetParent(transform, false);
        gameObject.GetComponent<Image>().color = Color.black;
        topBar = gameObject.GetComponent<RectTransform>();
        topBar.anchorMin = new Vector2(0, 1);
        topBar.anchorMax = new Vector2(1, 1);
        topBar.sizeDelta = new Vector2(0, 0);

        gameObject = new GameObject("bottomBar", typeof(Image));
        gameObject.transform.SetParent(transform, false);
        gameObject.GetComponent<Image>().color = Color.black;
        bottomBar = gameObject.GetComponent<RectTransform>();
        bottomBar.anchorMin = new Vector2(0, 0);
        bottomBar.anchorMax = new Vector2(1, 0);
        bottomBar.sizeDelta = new Vector2(0, 0);
    }

    void Update()
    {
        if (active)
        {
            Vector2 sizeDelta = topBar.sizeDelta;
            sizeDelta.y += changeSizeAmount * Time.deltaTime;

            if (changeSizeAmount > 0)
            {
                if (sizeDelta.y >= targetSize)
                {
                    sizeDelta.y = targetSize;
                    active = false;
                }
            }
            else if (sizeDelta.y <= targetSize)
            {
                sizeDelta.y = targetSize;
                active = false;
            }

            topBar.sizeDelta = sizeDelta;
            bottomBar.sizeDelta = sizeDelta;   
        }
    }

    public void cinematicShow(float targetSize, float time) 
    {
        this.targetSize = targetSize;
        changeSizeAmount = (targetSize - topBar.sizeDelta.y) / time;
        active = true;
    }

    public void cinematicHide(float time)
    {
        targetSize = 0f;
        changeSizeAmount = (targetSize - topBar.sizeDelta.y) / time;
        active = true;
    }
}
