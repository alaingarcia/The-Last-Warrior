using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxGravityAttractor : MonoBehaviour
{
    public float gravity = -10;
    private Vector3 difference;

    public void Attract(Transform body)
    {
        if (body.rotation.x == 0f || body.rotation.x == 180f)
        {
            difference = new Vector3(0f, body.position.y - transform.position.y, 0f);
        }
        else if (body.rotation.x == 90f || body.rotation.x ==  270f)
        {
            difference = new Vector3(0f, body.position.y - transform.position.y, 0f);
        }
        Vector3 gravityUp = difference.normalized;
       // Vector3 gravityUp = (body.position - transform.position).normalized;
        Vector3 bodyUp = body.up;

        body.GetComponent<Rigidbody>().AddForce(gravityUp * gravity);

        Quaternion targetRotation = Quaternion.FromToRotation(bodyUp, gravityUp) * body.rotation;
        body.rotation = Quaternion.Slerp(body.rotation, targetRotation, 50 * Time.deltaTime);
    }
}
