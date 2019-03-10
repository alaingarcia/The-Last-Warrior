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

    // minimum distance between enemies in the flock
    public float minDist;

    // wait sometime before jumping up to the player position (a little different from jump cooldown)
    // prevents enemy from jumping whenever the player jumps
    // will only jump if the player has been above the AI for 2 seconds (or whatever jumpWait is)
    public float jumpWait = 2;
    private float currentJumpWait;

    private Vector3 currentLocation;
    private Vector3 playerLocation;

    private Movement movementScript;

    private GameObject[] flock;

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
        if (player == null)
            return;
        
        // Initialize positions for the current frame
        currentLocation = transform.position;
        playerLocation = player.position;

        // This might be slow
        flock = GameObject.FindGameObjectsWithTag("Enemy");

        // Iterate through all enemies and pick the closest one to us
        Vector3 closestFlockerPos = Vector3.zero;
        float closestFlockerDistance = minDist;
        foreach (var f in flock)
        {
            if (f == null || f == gameObject)
                continue;

            float curDistance = Vector3.Distance(transform.position, f.transform.position);
            if (curDistance < minDist)
            {
                closestFlockerPos = f.transform.position;
                closestFlockerDistance = curDistance;
            }
        }

        // If we're close to the player, attack
        if (Vector3.Distance(transform.position, player.position) < 0.5)
            gameObject.transform.Find("WeaponHitBox").GetComponent<MeleeAttack>().Attack();

        Vector3 direction = player.position - transform.position;

        // We are too close to another enemy
        if (closestFlockerDistance < minDist)
        {
            //Debug.Log("Closest enemy is " + closestFlockerDistance + "m away - too close!");
            Vector3 avoidDir = transform.position - closestFlockerPos;
            avoidDir = avoidDir.normalized * (1 - closestFlockerDistance / minDist);
            direction += avoidDir;
        }

        // change to unit direction vector
        direction.Normalize();


        movementScript.move(direction.x, direction.z);

        if (currentJumpCooldown >= 0)
        {
            currentJumpCooldown -= Time.deltaTime;
        }

        // If player is below the AI, jump. 1 is added for unnecesary jumping
        if ((currentLocation.y+1) < playerLocation.y )
        {
            // lower the jump wait so that jumping will eventually occur
            currentJumpWait -= Time.deltaTime;

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
