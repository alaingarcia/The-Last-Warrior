using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlowdownAbility : MonoBehaviour
{
    public bool showBar = true;

    // list of enemy game objects that will be slowed
    private GameObject[] enemies;
    private GameObject[] missiles;

    // customizable multiplier for enemy slowdown
    public float enemySlowdownMultiplier = 0.5f;

    // customizable multiplier for player slowdown
    public float playerSlowdownMultiplier = 1.0f;

    // used to access effects for the player camera (zoom, black bars)
    private CameraEffects playerCameraEffects;
    
    private BlackBars blackBars;

    // used for the slowdown cooldown
    private Image slowBar;
    private float slowCooldownStart = 100;
    public float slowCooldownCurrent;
    private bool slowCooldownRestore = true;
    public float cooldownWasteRate = 30;
    public float cooldownRestoreRate = 10;
    
    void Start()
    {
        // initialize cameraEffects (which has zoom) script associated with the player camera
        playerCameraEffects = GameObject.FindWithTag("PlayerCamera").GetComponent<CameraEffects>();
    
        // initialize blackBars (which has cinematic black bars)
        blackBars = GameObject.FindWithTag("BlackBars").GetComponent<BlackBars>();

        // initialize the cooldown bar
        slowBar = GameObject.FindWithTag("SlowBar").GetComponent<Image>();
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

        if (showBar)
            slowBar.fillAmount = slowCooldownCurrent / slowCooldownStart;
        else
            slowBar.fillAmount = 0;
    }

    // Function to slow enemies/players down
    public void slowdown()
    {
        // ENEMY SLOWDOWN

        // get a list of all enemies (found by tag)
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        missiles = GameObject.FindGameObjectsWithTag("Missile");
        // iterate through the enemy list
        foreach (GameObject enemy in enemies)
        {
            if (enemy)
                enemy.GetComponent<Movement>().speed *= enemySlowdownMultiplier;
        }

        // slow down missiles
        foreach (GameObject missile in missiles)
        {
            if (missile)
                missile.GetComponent<Rigidbody>().velocity *= enemySlowdownMultiplier;
        }

        // PLAYER SLOWDOWN
        gameObject.GetComponent<Movement>().speed *= playerSlowdownMultiplier;

        // zoom in and show cinematic bars
        playerCameraEffects.zoom(true);
        blackBars.cinematicShow(100, 0.2f);

        cooldownRestore(false);
    }

    // Function to revert speeds back to normal
    public void normalSpeed()
    {
        // ENEMY NORMAL SPEED
        foreach(GameObject enemy in enemies)
        {
            if (enemy && enemy.GetComponent<Movement>().speed != enemy.GetComponent<Movement>().startSpeed)
                enemy.GetComponent<Movement>().speed /= enemySlowdownMultiplier;
        }

        foreach (GameObject missile in missiles)
        {
            if (missile)
                missile.GetComponent<Rigidbody>().velocity /= enemySlowdownMultiplier;
        }

        // PLAYER NORMAL SPEED
        gameObject.GetComponent<Movement>().speed /= playerSlowdownMultiplier;

        playerCameraEffects.zoom(false);
        blackBars.cinematicHide(0.3f);
        cooldownRestore(true);
    }

    void cooldownRestore(bool restore)
    {
        slowCooldownRestore = restore;
    }
}
