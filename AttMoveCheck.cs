using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttMoveCheck : MonoBehaviour {
    public AttractorBalance[] AB;
    public Vector3 PrevPos;
    public Quaternion zero = new Quaternion(0, 0, 0, 0);
    private AudioSource AS;
    // Use this for initialization
    void Start () {
        AB = FindObjectsOfType<AttractorBalance>();
        AS = GetComponent<AudioSource>();
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Slow Time") && AS.pitch > 0.11f)
            AS.pitch *= 0.1f;
        if (Input.GetButtonUp("Slow Time") && AS.pitch < 0.11f)
            AS.pitch *= 10f;
        if (transform.position != PrevPos)
        {
            PrevPos = transform.position;
            foreach (AttractorBalance A in AB)
            A.ChangeMags();
        }
    }
}
