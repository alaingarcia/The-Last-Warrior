using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementDetector : MonoBehaviour
{
    // For detecting if AI is stuck
    public const float movingThreshold = 0.01f;
    public const int movementFrameCount = 3;
    private Vector3[] prevLocations = new Vector3[movementFrameCount];

    public bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < prevLocations.Length; i++)
        {
            prevLocations[i] = Vector3.zero;
        }
        isMoving = true;
    }

    public bool IsMoving()
    {
        return isMoving;
    }

    // Update is called once per frame
    void Update()
    {
        //Store recent locations
        for (int i = 0; i < prevLocations.Length - 1; i++)
        {
            prevLocations[i] = prevLocations[i + 1];
        }
        prevLocations[prevLocations.Length - 1] = transform.position;

        //Check the distances between the points in your previous locations
        //If for the past several updates, there are no movements smaller than the threshold,
        //you can most likely assume that the object is not moving
        for (int i = 0; i < prevLocations.Length - 1; i++)
        {
            if (Vector3.Distance(prevLocations[i], prevLocations[i + 1]) >= movingThreshold)
            {
                //The minimum movement has been detected between frames
                isMoving = true;
                break;
            }
            else
            {
                isMoving = false;
            }
        }
    }
}
