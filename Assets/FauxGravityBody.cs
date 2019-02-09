using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxGravityBody : MonoBehaviour
{
    public FauxGravityAttractor attractor;
 
    void Start()
    {
        GetComponent<Rigidbody>().useGravity = false;

        attractor = GameObject.Find("Main Floor").GetComponent<FauxGravityAttractor>();
    }
    // Update is called once per frame
    void Update()
    {
        attractor.Attract(transform);
    }
}
