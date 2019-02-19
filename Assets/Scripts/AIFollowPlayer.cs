using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFollowPlayer : MonoBehaviour
{
    private Transform player;
    private Rigidbody body;

    private float right = 1;
    private float left = -1;

    private Vector3 currentLocation;
    private Vector3 playerLocation;

    // Start is called before the first frame update
    void Start()
    {
        // Set 'player' equal to the transform of the GameObject with the 'Player' tag (should only be one object)
        player = GameObject.FindWithTag("Player").transform;

        // Set 'body' equal to the current gameObject's Rigidbody (for physics)
        body = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Initialize positions for the current frame
        currentLocation = transform.position;
        playerLocation = player.position;

        // If player is to the right of the AI, move to the right
        if (currentLocation.x < playerLocation.x)
        {
            // jump is false because its not implemented yet
            gameObject.GetComponent<Movement>().move(right, false);
        }

        // If player is to the left of the AI, move to the left
        if (currentLocation.x > playerLocation.x)
        {
            // jump is false because its not implemented yet
            gameObject.GetComponent<Movement>().move(left, false);
        }

    }
}
