using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowdownAbility : MonoBehaviour
{
    // list of enemy game objects that will be slowed
    private GameObject[] enemies;

    // customizable multiplier by which the speed is decreased
    public float slowdownMultiplier;

    // Update is called once per frame
    void slowdown()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            // slow them down
        }
    }
}
