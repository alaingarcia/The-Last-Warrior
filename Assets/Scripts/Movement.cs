using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // body is used for physics purposes (Rigidbody has physics)
    private Rigidbody body;

    // sprite is used to flip the sprite depending on movement direction
    private SpriteRenderer sprite;

    // customizable max speed
    public float speed = 3.0f;

    private float direction;

    // Start is called before the first frame update
    void Start()
    {
        // Set 'body' equal to the rigidbody of the gameObject
        body = gameObject.GetComponent<Rigidbody>();

        // Set 'sprite' equal to the sprite of the gameObject
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void move(float horizontal, bool jump)
    {
        // If A, D, Left Arrow, or Right Arrow is pressed, move horizontally
        if (horizontal != 0)
        {
            direction = horizontal * speed;

            // Flip the sprite if moving left (because the sprite faces right by default)
            if (direction < 0)
            {
                sprite.flipX = true;
            }

            // Flip the sprite back once the player starts moving to the right again
            else if (direction > 0) 
            {
                sprite.flipX = false;
            }

            // Actually makes the player move horizontally (on the x axis)
            // The horizontal movement does not have an effect on the vertical movement,
            // so the y component remains body.velocity.y
            body.velocity = new Vector3(direction, body.velocity.y, 0.0f);
        }
    }
}
