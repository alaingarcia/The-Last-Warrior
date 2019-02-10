using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthManagement : MonoBehaviour
{
    private PlatformerCharacter3D player;

    // Keeps track of enemies, because we want to be able to heal when there are 0 enemies around
    public int enemyCount = 0;

    public float rateOfDamage = 2f;
    public float rateOfHealing = 1f;

    private Color newColor;

    private Image BlackBG;
    private Text GameOver;
    private Text Press;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlatformerCharacter3D>();

        // Set Game Over invisible
        changeAlpha(0f);

    }

    void Update()
    {
        if (enemyCount == 0 && player.Health < 100f && player.Health > 0f)
        {
            // Heal
            player.Health += rateOfHealing;
        }
        else if (player.Health <= 0f)
        {
            // Pause everything
            Time.timeScale = 0f;

            // Show Game Over
            changeAlpha(1f);

            if (Input.anyKey)
            {
                // Hide Game Over
                changeAlpha(0f);

                // Restart scene
                SceneManager.LoadScene("2dCharacter");

                // Resume everything
                Time.timeScale = 1f;
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.name.Contains("Enemy"))
        {
            enemyCount += 1;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.name.Contains("Enemy"))
        {
            enemyCount -= 1;
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.name.Contains("Enemy"))
        {
            // Take damage
            if (player.Health > 0f)
            {
                player.Health -= rateOfDamage;
            }
        }
    }

    void changeAlpha(float alpha)
    {
        BlackBG = GameObject.Find("BlackBG").GetComponent<Image>();
        newColor = BlackBG.color;
        newColor.a = alpha;
        BlackBG.color = newColor;

        GameOver = GameObject.Find("Game Over").GetComponent<Text>();
        newColor = GameOver.color;
        newColor.a = alpha;
        GameOver.color = newColor;

        Press = GameObject.Find("Press").GetComponent<Text>();
        newColor = Press.color;
        newColor.a = alpha;
        Press.color = newColor;
    }
}
