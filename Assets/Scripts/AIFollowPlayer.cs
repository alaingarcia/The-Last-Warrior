using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AIFollowPlayer : MonoBehaviour
{
    private Transform player;
    private Rigidbody body;

    private float right = 1;
    private float left = -1;

    // set cooldown for jump to prevent constant jumping
    public float jumpCooldown = 2;
    private float currentJumpCooldown;

    // wait sometime before jumping up to the player position (a little different from jump cooldown)
    // prevents enemy from jumping whenever the player jumps
    // will only jump if the player has been above the AI for 2 seconds (or whatever jumpWait is)
    public float jumpWait = 2;
    private float currentJumpWait;

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

        // initialize cooldown and jump wait
        currentJumpCooldown = jumpCooldown;
        currentJumpWait = jumpWait;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentJumpCooldown >= 0)
        {
            currentJumpCooldown -= Time.deltaTime;
        }

        // Initialize positions for the current frame
        currentLocation = transform.position;
        playerLocation = player.position;

        // If player is to the right of the AI, move to the right
        if (currentLocation.x < playerLocation.x)
        {
            // prevents enemy from just flipping back and forth when under or on the player
            if (Math.Abs(currentLocation.x - playerLocation.x) > 0.5)
            {
                movementScript.move(right);
            }
        }

        // If player is to the left of the AI, move to the left
        if (currentLocation.x > playerLocation.x)
        {
            // prevents enemy from just flipping back and forth when under or on the player
            if (Math.Abs(currentLocation.x - playerLocation.x) > 0.5)
            {
                movementScript.move(left);
            }
        }

        // If player is below the AI, jump. 1 is added for unnecesary jumping
        if ((currentLocation.y+1) < playerLocation.y )
        {
            jumpWait -= Time.deltaTime;
            // check if cooldown requirement has been met
            // then, check if the player has been above the AI for longer than jump wait (do not want the AI just immediately jumping when the player jumps)
            if ((currentJumpCooldown <= 0) && (jumpWait <= 0))
            {
                movementScript.jump();

                // reset current cooldown and current jump wait, can't jump again until the proper amount of time has passed
                currentJumpCooldown = jumpCooldown;
                currentJumpWait = jumpWait;
            }
        }
        
        // reset the waiting if the player jumps back down
        else
        {
            currentJumpWait = jumpWait;
        }
    }
}
