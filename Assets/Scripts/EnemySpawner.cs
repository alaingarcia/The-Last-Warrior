using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private int enemiesLeft;
    private float nextSpawnTime;
    private Vector3 location;


    public GameObject enemyToSpawn;
    public float spawnRate;
    public int enemiesToSpawn;
    
    // Start is called before the first frame update
    void Start()
    {
        enemiesLeft = enemiesToSpawn;
        location = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawnTime && enemiesLeft > 0)
        {
            nextSpawnTime = Time.time + spawnRate;
            GameObject e = Instantiate(enemyToSpawn, location, Quaternion.identity);
            e.SetActive(true);
            enemiesLeft -= 1;
        }
    }
}
