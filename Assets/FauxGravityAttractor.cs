using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxGravityAttractor : MonoBehaviour
{
    public float gravity = -10;
    private Vector3 difference;

    public void Attract(Transform body)
    {

        difference = new Vector3(0f, body.position.y - transform.position.y, 0f);

        Vector3 gravityUp = difference.normalized;

        body.GetComponent<Rigidbody>().AddForce(gravityUp * gravity);

    }
}
