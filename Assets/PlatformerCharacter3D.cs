using System;
using UnityEngine;

public class PlatformerCharacter3D : MonoBehaviour
{
    [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
    [SerializeField] private float m_JumpForce = 4f;                  // Amount of force added when the player jumps.
    // [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
    [SerializeField] public LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

    private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is grounded.
    private Transform m_CeilingCheck;   // A position marking where to check for ceilings
    const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
    private Animator m_Anim;            // Reference to the player's animator component.
    private Rigidbody m_Rigidbody;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.

    private float speedMultiplier = 1.0f;
    public float verticalSpeed = 0.25f;

    public void setSpeedMultiplier(float multiplier)
    {
        speedMultiplier = multiplier;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "Main Floor")
        {
            m_Grounded = true;
            m_Anim.SetBool("Ground", m_Grounded);
        }
     
        // Set the vertical animation
        m_Anim.SetFloat("vSpeed", m_Rigidbody.velocity.y);
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.name == "Main Floor")
        {
            m_Grounded = false;
            m_Anim.SetBool("Ground", m_Grounded);
        }
    }

    private void Awake()
    {
        // Setting up references.
        m_GroundCheck = transform.Find("GroundCheck");
        m_CeilingCheck = transform.Find("CeilingCheck");
        m_Anim = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        Physics.IgnoreLayerCollision(1, 9);
    }

    public void Die()
    {
        Destroy(gameObject.GetComponent<Collider>());
        Invoke("Destroy", 2f);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    /*
    private void FixedUpdate()
    {
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
                m_Grounded = true;
        }
        m_Anim.SetBool("Ground", m_Grounded);

        // Set the vertical animation
        m_Anim.SetFloat("vSpeed", m_Rigidbody.velocity.y);
    }
    */


    public void Move(float move_x, float move_z, bool jump)
    {
        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {

            // The Speed animator parameter is set to the absolute value of the horizontal input.
            m_Anim.SetFloat("Speed", Mathf.Abs(move_x));

            float x_component = speedMultiplier * move_x * m_MaxSpeed;
            Vector3 direction;

            direction = new Vector3(x_component, m_Rigidbody.velocity.y, speedMultiplier * move_z * m_MaxSpeed);
            //m_Rigidbody.MovePosition(m_Rigidbody.position + (direction.normalized)/5);
            m_Rigidbody.velocity = direction;
            
            // If the input is moving the player right and the player is facing left...
            if (move_x > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
                // Otherwise if the input is moving the player left and the player is facing right...
            else if (move_x < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
        }
        // If the player should jump...
        if (m_Grounded && jump && m_Anim.GetBool("Ground"))
        {
            Vector3 jumpVector = new Vector3(transform.position.x, transform.position.y * m_JumpForce, transform.position.z);
            // Add a vertical force to the player.
            m_Rigidbody.AddForce(jumpVector);
            m_Grounded = false;
            m_Anim.SetBool("Ground", false);
        }
    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
