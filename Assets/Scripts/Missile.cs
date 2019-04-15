using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float damage = 25;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<Health>().TakeDamage(damage);
            return;
        }

        if (other.tag == "Enemy")
        {
            // Don't do damage to yourself
            if (other.name != "Ranged Enemy")
            {
                //print("Missile hit " + other.name);

                // Arrows do quadruple damage if they hit an enemy instead of a player
                other.gameObject.GetComponent<Health>().TakeDamage(damage * 4);
            }
        }

        if (other.tag == "Ground")
        {
            //print("Missle hit the floor. Destroying in 1s");
            Destroy(gameObject, 1.0f);
        }
    }
}
