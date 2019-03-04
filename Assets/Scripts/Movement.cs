using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // body is used for physics purposes (Rigidbody has physics)
    private Rigidbody body;

    // sprite is used to flip the sprite depending on movement direction
    private SpriteRenderer sprite;

    // animator used to change the current animation
    private Animator animator;

    // customizable max speed, default value is based on what felt good
    public float speed = 3.0f;

    // customizable jump force, default value is based on what felt good
    public float jumpForce = 45.0f;

    // a value that will hold how fast the player should be going
    // directional because: if its less than 0, its going left. if its more than 1, its going right
    private float directional_velocity;

    //to see if it is on a grounded object
    private bool isGrounded =false;


    // Start is called before the first frame update
    void Start()
    {
        // Set 'body' equal to the rigidbody of the gameObject
        body = gameObject.GetComponent<Rigidbody>();

        // Set 'sprite' equal to the sprite of the gameObject
        sprite = gameObject.GetComponent<SpriteRenderer>();

        // set 'animator' equal to the animator of the gameObject
        animator = gameObject.GetComponent<Animator>();
    }

    void OnCollisionEnter(Collision col)
    {
        //Checks for a collision with an object named ground
        if ( (col.gameObject.tag == ("Ground") || col.collider.gameObject.tag == ("Enemy") ) && isGrounded == false)
        {
            isGrounded = true;
        }

        // Update animations accordingly if we're grounded
        animator.SetBool("Ground", isGrounded);
    }

    void OnCollisionExit(Collision col)
    {
        //Checks for a collision with an object named ground
        if ((col.gameObject.tag == ("Ground") || col.collider.gameObject.tag == ("Enemy")) && isGrounded == true)
        {
            isGrounded = false;
        }

        // Update animations accordingly if we're grounded
        animator.SetBool("Ground", isGrounded);
    }

    public void move(float horizontal)
    {
        // If A, D, Left Arrow, or Right Arrow is pressed, move horizontally
        if (horizontal != 0)
        {
            // horizontal = 1 when D or Right arrow is pressed
            // horizontal = -1 when A or Left arrow is pressed
            directional_velocity = horizontal * speed;

            Vector3 oldScale = transform.localScale;

            // Flip the sprite and colliders if moving left (because the sprite faces right by default)
            if (directional_velocity < 0)
            {     
                transform.localScale = new Vector3 (Mathf.Abs(oldScale.x) * -1f, oldScale.y, oldScale.z);
            }

            // Flip the sprite and colliders back once the player starts moving to the right again
            else if (directional_velocity > 0) 
            {
                transform.localScale = new Vector3 (Mathf.Abs(oldScale.x), oldScale.y, oldScale.z);
            }

            // Actually makes the player move horizontally (on the x axis)
            // The horizontal movement does not have an effect on the vertical movement,
            // so the y component remains body.velocity.y
            body.velocity = new Vector3(directional_velocity, body.velocity.y, 0.0f);
        }

        // flag animations for horizontal movement
        animator.SetFloat("Speed", Mathf.Abs(horizontal));
    }

    public void jump()
    {   
        //Check if it is grounded
        if (isGrounded)
        {
            // applies jumpForce in the y direction
            body.AddForce(0, jumpForce, 0, ForceMode.Impulse);
            isGrounded = false;
        }  
    }
}
