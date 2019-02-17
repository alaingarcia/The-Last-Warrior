using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    private Vector3 rotateVector;
    private GameObject player;

    private bool rotated0;
    private bool rotated180;

    void Start()
    {
        player = gameObject;

        rotated0 = true;
        rotated180 = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            rotate180();
        }
    }

    public void rotate180()
    {
        rotateVector = new Vector3(transform.position.x, -1 * transform.position.y, transform.position.z);
        //gameObject.GetComponent<FauxGravityBody>().gravity = false;
        transform.position = rotateVector;

        if (!rotated180)
        {
            Debug.Log("Initial Rotation");
            rotated180 = true;
            transform.eulerAngles = new Vector3(180f, 0f, 0f);
        }

        else if (rotated180)
        {
            Debug.Log("Rotating Back");
            rotated180 = false;
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        //gameObject.GetComponent<FauxGravityBody>().gravity = true;
    }
}
