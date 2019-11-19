using UnityEngine;
using System.Collections;

public class AnimSwitch : MonoBehaviour {

    public GameObject LeftEye;
    public GameObject RightEye;
    public Animator animator;
    public float EyeRad;
    public float AnimOffsX, AnimOffsY;
    private Vector3 DefaultL, DefaultR;
    public Rigidbody2D rig;
    public Collider2D col;
    private float TimeFrame;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        //rig = GetComponent<Rigidbody2D>();
        //col = GetComponent<Collider2D>();
        DefaultL = LeftEye.transform.localPosition;
        DefaultR = RightEye.transform.localPosition;
    }

    // Update is called once per frame
    void Update ()
    {
        animator.SetFloat("Speed", rig.velocity.magnitude);
        animator.SetBool("OnGround", col.IsTouchingLayers(1));
        if (Input.anyKey)
        {
            animator.SetFloat("IdleTime", 0);
            TimeFrame = Time.timeSinceLevelLoad;
        }
        else
        {
            animator.SetFloat("IdleTime", Time.timeSinceLevelLoad - TimeFrame);
        }
        Vector3 _MousePoint =
                    Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        //use animation set values to indicate how far the eyes may wander
        Vector3 AnimOffs = new Vector3(AnimOffsX, AnimOffsY, 0);
        //convert mouse pos to localpos
        Vector3 LEP = transform.InverseTransformPoint(_MousePoint);
        Vector3 REP = transform.InverseTransformPoint(_MousePoint);
        //reduce mouse pos to a small number, then set eye pos to that number.
        if (LEP.magnitude > EyeRad)
            LEP = LEP.normalized * EyeRad;
        if (REP.magnitude > EyeRad)
            REP = REP.normalized * EyeRad;
        LeftEye.transform.localPosition = LEP + DefaultL + AnimOffs;
        RightEye.transform.localPosition = REP + DefaultR + AnimOffs;

    }
}
