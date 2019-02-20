using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerButtonControl : MonoBehaviour
{
    private float horizontal;

    // default jump button will be space
    public KeyCode jumpKey = KeyCode.Space;

    public KeyCode slowKey = KeyCode.LeftShift;

    // gets the movement component of the gameObject
    private Movement movementScript;

    private SlowdownAbility slowdownScript;

    void Start() 
    {
        // sets value to the movement script
        movementScript = gameObject.GetComponent<Movement>();

        slowdownScript = gameObject.GetComponent<SlowdownAbility>();
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

        if (Input.GetKeyDown(slowKey))
        {
            slowdownScript.slowdown();
        }

        if (Input.GetKeyUp(slowKey))
        {
            slowdownScript.normalSpeed();
        }
    }
}
