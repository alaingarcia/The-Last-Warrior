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

    // Game Over stuff
    Image gameOver;
    Color imageColor;

    // Start is called before the first frame update
    void Start()
    {
        health = startingHealth;

        healthBar = gameObject.transform.Find("StatCanvas").Find("HealthBar").GetComponent<Image>();
        
        // initialize gameOver image
        if (gameObject.tag == "Player")
        {
            gameOver = gameObject.transform.Find("GameOverCanvas").Find("GameOverImage").GetComponent<Image>();
        }
    }

    void Update()
    {
        HealthDisplay(showHealth);
        
        // if entity dies
        if (health <= 0)
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

                Time.timeScale = 1f;

                // If any key is pressed, go back to the scene
                if (Input.anyKey)
                {
                    SceneManager.LoadScene("FirstScene");
                    Time.timeScale = 1f;
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
        // First, destroy collider so that the entity sinks into the ground
        Destroy(gameObject.GetComponent<Collider>());

        // After a second, destroy the entity
        Invoke("DestroySelf", 1f);
    }

    // Destroy function used by Die()
    void DestroySelf()
    {
        Destroy(gameObject);
    }

}
