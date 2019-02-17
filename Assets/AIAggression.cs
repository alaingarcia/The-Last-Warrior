using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof(PlatformerCharacter2D))]
    public class AIAggression : MonoBehaviour

    {
        private PlatformerCharacter2D m_Character;
        public float speed = 6.0f;
        private float health = 10.0f;
        private GameObject player;
        private Vector2 direction;
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
            direction = new Vector2(player.transform.position.x, player.transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, direction, speedMultiplier * speed * Time.deltaTime);
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
}