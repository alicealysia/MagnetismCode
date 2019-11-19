using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickrateAffiliate : MonoBehaviour {
    public Rigidbody2D Target;
    // Use this for initialization
    void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
            transform.position += (Vector3)Target.velocity * Time.deltaTime;
            transform.Rotate(Vector3.forward, Target.angularVelocity * Time.deltaTime);
    }
    void FixedUpdate()
    {
        transform.position = Target.position;
        transform.rotation = Target.transform.rotation;
    }
}
