using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetObjects : MonoBehaviour {
    public GameObject[] ToReset;
    private Vector3[] transformations;
    private float[] Rotations;
	// Use this for initialization
	void Start ()
    {
        transformations = new Vector3[ToReset.Length];
		for (int i = 0; i < ToReset.Length;i++)
        {
            transformations[i] = ToReset[i].transform.position;
            Rotations[i] = ToReset[i].GetComponent<Rigidbody2D>().rotation;
        }
	}
	
	// Update is called once per frame
	public void OnDeath ()
    {
        for (int i = 0; i < ToReset.Length; i++)
        {
            ToReset[i].GetComponent<Rigidbody2D>().MovePosition(transformations[i]);
            ToReset[i].GetComponent<Rigidbody2D>().MoveRotation(Rotations[i]);
            ToReset[i].GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            ToReset[i].GetComponent<Rigidbody2D>().angularVelocity = 0;
        }
    }
}
