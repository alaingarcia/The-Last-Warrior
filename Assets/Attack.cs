using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float attackCooldown;
    private float timeLeft;

    public Transform attackPos;

    public float attackRange;
    public int damage;

    public KeyCode attackKey = KeyCode.Mouse0;

    void Start()
    {
        // Set the radius sphere collider (which checks for collisions with enemies to determine whether there is a hit or not) 
        gameObject.GetComponent<SphereCollider>().radius = attackRange;
    }

    void OnCollisionStay(Collision collision)
    {
        if (timeLeft <= 0)
        {
            // If key is pressed and the thing collided with is in the enemy layer, destroy it
            if (Input.GetKeyDown(attackKey) && (collision.gameObject.layer == LayerMask.NameToLayer("Enemy")))
            {
                Destroy(collision.gameObject);
            }
            timeLeft = attackCooldown;
        }
        else
        {
            timeLeft = timeLeft - Time.deltaTime;
        }
    }

    // Gives the red wireframe thing
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}