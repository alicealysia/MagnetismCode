using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour {
    public Vector2 StartPos;
    public Vector2 EndPos;
    public float speed;
    private Vector2 TargetPos;
	// Use this for initialization
	void Start ()
    {
        TargetPos = StartPos;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Vector2.Distance(transform.position,TargetPos) < speed*Time.deltaTime)
        {
            transform.position = TargetPos;
            if (TargetPos == EndPos)
                TargetPos = StartPos;
            else
                TargetPos = EndPos;
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, TargetPos, speed*Time.deltaTime);
        }
	}
}
