using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attack : MonoBehaviour
{
    public Transform attackPos;
    public float attackRange;
    public LayerMask attackLayer;
    public float attackCooldown = .25f;
    public KeyCode attackKey = KeyCode.Mouse0;

    public Text scoreText;
    private int score;

    public Animator animator;
    public float attackTimer = 0;
    public bool attacking;

    // Called on the first frame
    void Start()
    {
        score = 0;
        //scoreText = GameObject.Find("Canvas").GetComponent<Text>();
        scoreText.text = "Score: " + score.ToString();

        attacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        // While the key is pressed, keep checking for enemies that need to be attacked
        if (Input.GetKey(attackKey) && !attacking)
        {
            attacking = true;
            attackTimer = attackCooldown;
            
            // Get all enemies to be attacked, then attack one of them
            Collider[] objectsAttacked = Physics.OverlapSphere(attackPos.position, attackRange, attackLayer);
            if (objectsAttacked.Length >= 1)
            {
                objectsAttacked[0].GetComponent<PlatformerCharacter3D>().Die();
                score += 100;
            }
        }
        scoreText.text = "Score: " + score.ToString();

        if (attacking)
        {
            if (attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
            }
            else
            {
                attacking = false;
            }
        }

        animator.SetBool("Attacking", attacking);
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}