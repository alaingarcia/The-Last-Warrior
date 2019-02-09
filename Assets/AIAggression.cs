using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AIAggression : MonoBehaviour

{
    private PlatformerCharacter3D m_Character;
    public float speed = 6.0f;
    private float health = 10.0f;
    private GameObject player;
    private Vector3 direction;
    private float speedMultiplier = 1;

    public void setSpeedMultiplier(float multiplier)
    {
        speedMultiplier = multiplier;
    }

    private void Awake()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        direction = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, direction, speedMultiplier * speed * Time.deltaTime);
    }

    public void takeDamage(float damage)
    {
        health = health - damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}