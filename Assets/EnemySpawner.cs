using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
    
{
    public GameObject enemy;
    private Vector3 spawnLocation;

    public float spawnRate = 5;
    public int amountToSpawn = 5;

    private int amountLeft;
    private float nextSpawn;

    // Start is called before the first frame update
    void Start()
    {
        // Sets spawn location to the currenct location of the spawner
        spawnLocation = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        amountLeft = amountToSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawn && amountLeft != 0)
        {
            nextSpawn = Time.time + spawnRate;
            Instantiate(enemy, spawnLocation, Quaternion.identity);
            amountLeft = amountLeft - 1;
        }
    }
}
