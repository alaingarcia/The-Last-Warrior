using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float health;
    public float startingHealth = 100f;

    // Health display stuff
    public bool showHealth = true;
    Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        health = startingHealth;

        healthBar = gameObject.transform.Find("HealthCanvas").Find("HealthBar").GetComponent<Image>();
    }

    void Update()
    {
        HealthDisplay(showHealth);
        
        if (health <= 0)
        {
            Die();
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
        Destroy(gameObject.GetComponent<Collider>());
        Invoke("DestroySelf", 1f);
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }

}
