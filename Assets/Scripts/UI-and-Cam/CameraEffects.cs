using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Script currently only responsible for zooming in the camera
// So far, used when slowdown ability is used

// *** FOV and Zoom Level are interchangeable in terms of language ***

public class CameraEffects : MonoBehaviour
{
    private Camera playerCamera;

    // ZOOM LEVEL STUFF
    // newZoomLevel is the FOV level necessary for the appearance of zooming in (customizable)
    public int newZoomLevel = 30;
    // store the normal FOV level to return back to this level once zooming in is done
    private int normalZoomLevel;
    // store the current FOV level for use in changing the current level
    private int currentZoomLevel;  
    // Will help provide a smooth transition into the smoothing
    public float zoomSmoothing = 10.0f;
    // By default, not zoomed in
    private bool zoomedIn = false;

    void Start()
    {
        // Initializes player camera
        playerCamera = GameObject.FindWithTag("PlayerCamera").GetComponent<Camera>();

        // Finds the initial zoom level and then considers it the normal zoom level
        normalZoomLevel = (int)playerCamera.fieldOfView;
    }

    public void zoom(bool zoomIn)
    {
        zoomedIn = zoomIn;
    }

    void Update()
    {
        currentZoomLevel = (int)playerCamera.fieldOfView;

        // If zoomed in, gradually change the FOV level to the newZoomLevel over time t = Time.deltaTime * zoomSmoothing
        if (zoomedIn)
            playerCamera.fieldOfView = Mathf.Lerp(currentZoomLevel, newZoomLevel, Time.deltaTime * zoomSmoothing);

        // Return to normal zoom level if not zoomed in and current zoom is not normal zoom
        else if (currentZoomLevel < normalZoomLevel - 5)
            playerCamera.fieldOfView = Mathf.Lerp(currentZoomLevel, normalZoomLevel, Time.deltaTime * zoomSmoothing);
    }
}
