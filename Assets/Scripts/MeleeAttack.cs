using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    Collider WeaponHitBox;
    Animator animator;
    public float damage;
    public float attackTimeDuration;
    List<GameObject> targets = new List<GameObject>();

    // true when we are inside our attack animation
    private bool attacking = false;
    private float attackTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        WeaponHitBox = gameObject.GetComponent<Collider>();
        animator = gameObject.transform.parent.GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Player")
        {
            targets.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Player")
        {
            targets.Remove(other.gameObject);
        }
    }

    void Update()
    {
        // If the attack animation is playing, deal damage to anything hit by our attack animation
        if (attacking)
        {
            foreach (GameObject target in targets)
            {
                if (target)
                {
                    // Confirm that our target has health before we try to deal damage to it
                    if (target.GetComponent<Health>() != null)
                        target.GetComponent<Health>().TakeDamage(damage);
                }
            }

            // Reduce the attack timer and stop attacking 
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

    public void Attack()
    {
        // Only start attacking if we aren't attacking already
        if (!attacking)
        {
            attacking = true;
            attackTimer = attackTimeDuration;
        }
    }
}
