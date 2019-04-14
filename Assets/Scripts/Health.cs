using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public float health;
    public float startingHealth = 100f;

    // Health display stuff
    public bool showHealth = true;
    Image healthBar;
    public bool dead;

    // Game Over stuff
    Image gameOver;
    Color imageColor;
    string currentLevel;

    //Sound
    public AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        health = startingHealth;

        //Get oof sound
        audio = GameObject.FindWithTag("sound").GetComponent<AudioSource>();

        healthBar = gameObject.transform.Find("StatCanvas/HealthBarMask/HealthBar").GetComponent<Image>();
        
        // initialize gameOver image
        if (gameObject.tag == "Player")
        {
            gameOver = GameObject.FindWithTag("GameOver").GetComponent<Image>();
        }

        currentLevel = SceneManager.GetActiveScene().name;
    }

    void Update()
    {
        HealthDisplay(showHealth);

        // if entity died on this tick
        if (health <= 0 && !dead)
        { 
            dead = true;
            if (audio != null)
            {
                audio.Play();
            }
        }

        if (health <= 0 )
        {
            // only die if not the player
            if (gameObject.tag != "Player")
            {
                Die();
            }
            
            // If player dies, do game over
            else
            {
                // Show the game over
                imageColor = new Color(1f, 1f, 1f, 1f);
                gameOver.color = imageColor;

                // If any key is pressed, go back to the scene
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SceneManager.LoadScene(currentLevel);
                }
            }
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    public void HealthDisplay(bool showHealth)
    {
        if (showHealth)
        {
            healthBar.fillAmount = health/startingHealth;
        }
        else
        {
            healthBar.fillAmount = 0f;
        }
    }

    public void Die()
    {
        // Stop moving
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;

        // Play death animation
        gameObject.GetComponent<Animator>().Play("Die");
        
        // First, destroy collider so that the entity sinks into the ground
        // Destroy(gameObject.GetComponent<Collider>());

        // After half a second, destroy the entity
        Invoke("DestroySelf", 0.5f);
    }

    // Destroy function used by Die()
    void DestroySelf()
    {
        Destroy(gameObject);
    }

}
