using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class Attack : MonoBehaviour
    {
        public float attackCooldown;
        private float timeLeft;

        public Transform attackPos;

        public float attackRange;
        public int damage;

        public KeyCode attackKey = KeyCode.Mouse0;

        void Start()
        {
           
        }

        // Update is called once per frame
        void Update()
        {
            if (timeLeft <= 0)
            {
                if (Input.GetKey(attackKey))
                {
                    void OnCollisionEnter(Collision collision)
                    {/*
                        if (collision.GetComponent<Collider>().name == "Enemy")
                        {
                            Destroy(collider.gameObject);
                        }*/
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