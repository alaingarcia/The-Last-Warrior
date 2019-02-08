using UnityEngine;
using System.Collections;

public class CompleteCameraController : MonoBehaviour
{

    public GameObject player;       //Public variable to store a reference to the player game object
    private Vector3 offset;         //Private variable to store the offset distance between the player and camera
    public float zoomLevel = 10.0f;
    private Vector3 zoomVector;
    private bool zoomIn = false;

    public void zoom()
    {
        zoomIn = !zoomIn;
        // zoomLevel negative for zoom in, zoomLevel position for zoom out
        zoomVector = new Vector3(0.0f, 0.0f, zoomLevel);
    }

    // Use this for initialization
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - player.transform.position;

        player = GameObject.Find("Player");
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        if (!zoomIn)
        {
            // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
            transform.position = player.transform.position + offset;
        }

        else
        {
            transform.position = player.transform.position + offset + zoomVector;
        }
    }
}