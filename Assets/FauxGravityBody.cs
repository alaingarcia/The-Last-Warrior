using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxGravityBody : MonoBehaviour
{
    public FauxGravityAttractor attractor;
    public bool gravity;
 
    void Start()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        GetComponent<Rigidbody>().useGravity = false;
        gravity = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (gravity == true)
        {
            attractor.Attract(transform);
        }
    }
}
