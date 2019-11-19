using UnityEngine;
using System.Collections;

public class stretchSkew : MonoBehaviour {

    public Rigidbody2D RGB;
    public Transform TRN;
    private float RotateOffset = 0;
    private float ROS;
	// Use this for initialization
	void Start ()
    {
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = TRN.position;
        TRN.localPosition = Vector3.zero;
        float angle = Mathf.Atan2(RGB.velocity.y, RGB.velocity.x) * Mathf.Rad2Deg;
        Vector3 Rot = transform.rotation.eulerAngles;
        Rot.z = angle;
        transform.rotation = Quaternion.Euler(Rot);

        ROS = Mathf.DeltaAngle(RotateOffset, -angle);
        TRN.Rotate(0, 0, ROS,Space.Self);
        RotateOffset += ROS;

        

            if (RGB.velocity.magnitude < 10f)
                transform.localScale = new Vector3(1, 1f - 0.5f * (RGB.velocity.magnitude) / 10, 1);
            else
                transform.localScale = new Vector3(1, 0.5f, 1);
    }
}
