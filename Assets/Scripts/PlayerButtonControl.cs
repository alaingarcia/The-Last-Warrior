using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerButtonControl : MonoBehaviour
{
    private float horizontal;

    // default jump button will be space
    private KeyCode jumpKey = KeyCode.Space;

    // gets the movement component of the gameObject
    private Movement movementScript;

    void Start() 
    {
        // sets value to the movement script
        movementScript = gameObject.GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        movementScript.move(horizontal);

        // if jump button is pressed, call jump from the movement script
        if (Input.GetKeyDown(jumpKey))
        {
            movementScript.jump();
        }
    }
}
