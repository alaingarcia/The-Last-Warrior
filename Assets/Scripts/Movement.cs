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
    // if horizontal is less than 0, its going left. if its more than 1, its going right
    // if vertical is less than 0, its coming towards the camera
    private float horizontal_velocity;
    private float vertical_velocity;

    //to see if it is on a grounded object
    private bool isGrounded = false;


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
        if (col.gameObject.tag == "Ground" || col.collider.gameObject.tag == "Enemy")
        {
            isGrounded = true;
        }

        //Debug.Log("CollisionEnter with " + col.gameObject.name);

        // Update animations accordingly if we're grounded
        animator.SetBool("Ground", isGrounded);
    }

    void OnCollisionExit(Collision col)
    {
        //If we just left the ground, we are no longer grounded
        if (col.gameObject.tag == "Ground" || col.collider.gameObject.tag == "Enemy")
        {
            isGrounded = false;
        }

        Debug.Log("CollisionExit with " + col.gameObject.name);

        // Update animations accordingly if we're grounded
        animator.SetBool("Ground", isGrounded);
    }

    void Update()
    {
        // update animation stats for falling or jumping based on our Y velocity
        animator.SetFloat("VelocityY", body.velocity.y);
    }

    public void move(float horizontal, float vertical)
    {
        // horizontal = 1 when D or Right arrow is pressed
        // horizontal = -1 when A or Left arrow is pressed
        horizontal_velocity = horizontal * speed;

        // vertical = 1 when W or Up arrow is pressed
        // vertical = -1 when S or Down arrow is pressed
        vertical_velocity = vertical * speed;

        // Check if the horizontal movement should flip the player
        if (horizontal_velocity != 0)
        {

            // Flip the sprite and colliders if moving left (because the sprite faces right by default)
            if (horizontal_velocity < 0)
            {   
                sprite.flipX = true;  
                if (gameObject.transform.Find("WeaponHitBox"))
                {
                    gameObject.transform.Find("WeaponHitBox").localScale = new Vector3(-1f, 1f, 1f);
                }         
            }

            // Flip the sprite and colliders back once the player starts moving to the right again
            else if (horizontal_velocity > 0) 
            {
                sprite.flipX = false;
                if (gameObject.transform.Find("WeaponHitBox"))
                {
                    gameObject.transform.Find("WeaponHitBox").localScale = new Vector3(1f, 1f, 1f);
                }    
            }
        }

        // Actually makes the player move horizontally (on the x axis) and vertically (z axis)
        // y component remains body.velocity.y because that is only changed by jump
        body.velocity = new Vector3(horizontal_velocity, body.velocity.y, vertical_velocity);

        // flag animations for horizontal movement. We use the same animation for left/right movement so always pass in a positive value
        animator.SetFloat("VelocityX", Mathf.Abs(horizontal));

        // flag animations for vertical movement
        animator.SetFloat("VelocityZ", vertical);
    }

    public void jump()
    {   
        // Don't jump if we are already jumping/falling
        if (isGrounded)
        {
            // applies jumpForce in the y direction
            body.AddForce(0, jumpForce, 0, ForceMode.Impulse);
            isGrounded = false;
            animator.SetBool("Ground", isGrounded);
        }  
    }

    public void jump(float force)
    {
        // Don't jump if we are already jumping/falling
        if (/*isGrounded*/ body.velocity.y < 0.1 && body.velocity.y > -0.1)
        {
            // applies jumpForce in the y direction
            body.AddForce(0, force, 0, ForceMode.Impulse);
            isGrounded = false;
            animator.SetBool("Ground", isGrounded);
        }
    }
}
