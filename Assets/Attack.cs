using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float attackCooldown;
    private float timeLeft;

    public Transform attackPos;
    public LayerMask enemyLayer;

    public float attackRange;
    public int damage;

    // Update is called once per frame
    void Update()
    {
        if (timeLeft <= 0)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                Collider2D[] enemiesDamaged = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyLayer);
                for (int i = 0; i < enemiesDamaged.Length; i++)
                {
                    enemiesDamaged[i].GetComponent<AIAggression>().takeDamage(damage);
                }
            }
            timeLeft = attackCooldown;
        }

        else
        {
            timeLeft = timeLeft - Time.deltaTime;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
