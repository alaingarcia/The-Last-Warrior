using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementControl : MonoBehaviour
{

    private bool jump;
    private float horizontal;


    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        
        // jump is false because its not implemented yet
        jump = false;
        gameObject.GetComponent<Movement>().move(horizontal, jump);

    }
}
