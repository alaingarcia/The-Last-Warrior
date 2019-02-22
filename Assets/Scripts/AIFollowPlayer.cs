using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFollowPlayer : MonoBehaviour
{
    private Transform player;
    private Rigidbody body;

    private float right = 1;
    private float left = -1;

    // set cooldown for jump to prevent constant jumping
    public float jumpCooldown = 2;
    private float currentJumpCooldown;

    private Vector3 currentLocation;
    private Vector3 playerLocation;

    private Movement movementScript;

    // Start is called before the first frame update
    void Start()
    {
        // Set 'player' equal to the transform of the GameObject with the 'Player' tag (should only be one object)
        player = GameObject.FindWithTag("Player").transform;

        // sets value to the movement script
        movementScript = gameObject.GetComponent<Movement>();

        // Set 'body' equal to the current gameObject's Rigidbody (for physics)
        body = gameObject.GetComponent<Rigidbody>();

        // initialize cooldown
        currentJumpCooldown = jumpCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentJumpCooldown < jumpCooldown)
        {
            currentJumpCooldown += Time.deltaTime;
        }

        // Initialize positions for the current frame
        currentLocation = transform.position;
        playerLocation = player.position;

        // If player is to the right of the AI, move to the right
        if (currentLocation.x < playerLocation.x)
        {
            movementScript.move(right);
        }

        // If player is below the AI, jump. 1 is added for unnecesary jumping
        if ((currentLocation.y+1) < playerLocation.y )
        {
            // check if cooldown requirement has been met
            if (currentJumpCooldown >= jumpCooldown)
            {
                movementScript.jump();

                // set current cooldown to zero, can't jump again until the current cooldown is back to jumpCooldown
                currentJumpCooldown = 0;
            }
        }
        // If player is to the left of the AI, move to the left
        if (currentLocation.x > playerLocation.x)
        {
            movementScript.move(left);
        }

    }
}
