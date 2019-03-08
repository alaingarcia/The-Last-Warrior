using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    Collider WeaponHitBox;
    public float damage;
    List<GameObject> targets = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        WeaponHitBox = gameObject.GetComponent<Collider>();
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

    public void Attack()
    {
        foreach (GameObject target in targets)
        {
            if (target)
            {
                target.GetComponent<Health>().TakeDamage(damage);
            }
            else
            {
                targets.Remove(target);
            }
        }
    }
}
