using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowdownAbility : MonoBehaviour
{
    // list of enemy game objects that will be slowed
    private GameObject[] enemies;

    // customizable multiplier for enemy slowdown
    public float enemySlowdownMultiplier = 0.5f;

    // customizable multiplier for player slowdown
    public float playerSlowdownMultiplier = 1.0f;

    // Function to slow enemies/players down
    public void slowdown()
    {
        // ENEMY SLOWDOWN

        // get a list of all enemies (found by tag)
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // iterate through the enemy list
        // save initialSpeed for that gameObject to be order to revert it later
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Movement>().speed *= enemySlowdownMultiplier;
        }

        // PLAYER SLOWDOWN
        gameObject.GetComponent<Movement>().speed *= playerSlowdownMultiplier;
    }

    // Function to revert speeds back to normal
    public void normalSpeed()
    {
        // ENEMY NORMAL SPEED
        foreach(GameObject enemy in enemies)
        {
            enemy.GetComponent<Movement>().speed /= enemySlowdownMultiplier;
        }

        // PLAYER NORMAL SPEED
        gameObject.GetComponent<Movement>().speed /= playerSlowdownMultiplier;
    }
}
