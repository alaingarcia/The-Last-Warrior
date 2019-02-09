using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attack : MonoBehaviour
{
    public Transform attackPos;
    public float attackRange;
    public LayerMask attackLayer;
    public float attackCooldown = 10;
    public KeyCode attackKey = KeyCode.Mouse0;

    public Text scoreText;
    private int score;

    // Called on the first frame
    void Start()
    {
        score = 0;
        scoreText.text = "Score: " + score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        // While the key is pressed, keep checking for enemies that need to be attacked
        if (Input.GetKey(attackKey))
        {
            // Get all enemies to be attacked, then attack one of them
            Collider[] objectsAttacked = Physics.OverlapSphere(attackPos.position, attackRange, attackLayer);
            if (objectsAttacked.Length >= 1)
            {
                objectsAttacked[0].GetComponent<PlatformerCharacter3D>().Die();
                score += 100;

            }
        }
        scoreText.text = "Score: " + score.ToString();
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}