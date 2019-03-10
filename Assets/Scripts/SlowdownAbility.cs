using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlowdownAbility : MonoBehaviour
{
    // list of enemy game objects that will be slowed
    private GameObject[] enemies;

    // customizable multiplier for enemy slowdown
    public float enemySlowdownMultiplier = 0.5f;

    // customizable multiplier for player slowdown
    public float playerSlowdownMultiplier = 1.0f;

    // used to access effects for the player camera (zoom, black bars)
    private CameraEffects playerCameraEffects;

    // used for the slowdown cooldown
    private Image slowBar;
    private float slowCooldownStart = 100;
    public float slowCooldownCurrent;
    private bool slowCooldownRestore = true;
    public float cooldownWasteRate = 30;
    public float cooldownRestoreRate = 10;

    
    void Start()
    {
        // intialize cameraEffects (which has zoom, black bars) script associated with the player camera
        playerCameraEffects = GameObject.FindWithTag("PlayerCamera").GetComponent<CameraEffects>();
    
        // initialize the cooldown bar
        slowBar = gameObject.transform.Find("StatCanvas").Find("SlowBar").GetComponent<Image>();
        slowCooldownCurrent = slowCooldownStart;
    }

    void Update()
    {
        if (slowCooldownRestore)
        {
            // only restore if cooldown < 100
            if (slowCooldownCurrent < 100)
            {
                slowCooldownCurrent += cooldownRestoreRate * Time.deltaTime;
            }
        }

        else
        {
            // only decrease if cooldown is greater than 0
            if (slowCooldownCurrent > 0)
            {
                slowCooldownCurrent -= cooldownWasteRate * Time.deltaTime;
            }
        }

        slowBar.fillAmount = slowCooldownCurrent / slowCooldownStart;
    }

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
            if (enemy)
                enemy.GetComponent<Rigidbody>().velocity *= enemySlowdownMultiplier;
        }

        // PLAYER SLOWDOWN
        gameObject.GetComponent<Movement>().speed *= playerSlowdownMultiplier;

        playerCameraEffects.zoom(true);
        playerCameraEffects.cinematic();

        cooldownRestore(false);
    }

    // Function to revert speeds back to normal
    public void normalSpeed()
    {
        // ENEMY NORMAL SPEED
        foreach(GameObject enemy in enemies)
        {
            if (enemy)
                enemy.GetComponent<Rigidbody>().velocity /= enemySlowdownMultiplier;
        }

        // PLAYER NORMAL SPEED
        gameObject.GetComponent<Movement>().speed /= playerSlowdownMultiplier;

        playerCameraEffects.zoom(false);
        cooldownRestore(true);
    }

    void cooldownRestore(bool restore)
    {
        slowCooldownRestore = restore;
    }
}
