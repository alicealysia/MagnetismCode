using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dieOnCol : MonoBehaviour {
    public bool ShouldRes;
    Vector3 StartPos;
    void Start()
    {
        StartPos = transform.position;
    }
    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "KillPlayer")
        {
            if (!ShouldRes)
                GameObject.Destroy(gameObject);
            else
            {
                transform.position = StartPos;
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                GetComponent<Rigidbody2D>().angularVelocity = 0;
            }
        }
    }
}
