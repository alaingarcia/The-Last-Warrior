using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


[RequireComponent(typeof (PlatformerCharacter3D))]
public class Platformer3DUserControl : MonoBehaviour
{
    private PlatformerCharacter3D m_Character;
    private bool m_Jump;


    private void Awake()
    {
        m_Character = GetComponent<PlatformerCharacter3D>();
    }


    private void Update()
    {
        if (!m_Jump)
        {
            // Read the jump input in Update so button presses aren't missed.
            m_Jump = Input.GetButtonDown("Jump");
        }
    }


    private void FixedUpdate()
    {
        // Read the inputs.
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        // Pass all parameters to the character control script.
        m_Character.Move(horizontal, vertical, m_Jump);
        m_Jump = false;
    }
}

