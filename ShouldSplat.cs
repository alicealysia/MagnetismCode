using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShouldSplat : MonoBehaviour {
    float PrevVel;
    Rigidbody2D SquishyRigid;
    AudioSource SoundSource;

	// Use this for initialization
	void Start ()
    {
        SquishyRigid = GetComponent<Rigidbody2D>();
        SoundSource = GetComponent<AudioSource>();
        PrevVel = Mathf.NegativeInfinity;
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if (SquishyRigid.velocity.magnitude  - PrevVel < -5f)
        {
            SoundSource.volume = -(SquishyRigid.velocity.magnitude - PrevVel +5) * 0.1f;
            SoundSource.Play();
        }
        PrevVel = SquishyRigid.velocity.magnitude;

    }
}
