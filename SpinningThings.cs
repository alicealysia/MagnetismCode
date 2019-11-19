using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningThings : MonoBehaviour {
    public bool ShouldSpin;
    public float PushLength;
    public float MoveSpeed;

    private float MovedAmount;
    private Vector3 StartPos;
	// Use this for initialization
	void Start ()
    {
        StartPos = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (ShouldSpin) transform.Rotate(Vector3.forward,360*Time.deltaTime);
        if (MoveSpeed > 0)
        {
            transform.position += transform.up * Time.deltaTime * MoveSpeed;
            MovedAmount += MoveSpeed * Time.deltaTime;
            if (MovedAmount > PushLength)
            {
                MovedAmount -= PushLength;
                transform.position -= PushLength * transform.up;
            }
        }
	}
}
