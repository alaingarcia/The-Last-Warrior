using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAggression : MonoBehaviour

{
    public float speed = 6.0f;
    private float health = 10.0f;
    private GameObject player;
    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        direction = new Vector2(player.transform.position.x, player.transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, direction, speed * Time.deltaTime);
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
