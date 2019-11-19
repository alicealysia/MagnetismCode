using UnityEngine;
using System.Collections;

public class Paralax : MonoBehaviour {
    private GameObject MC;
    private Vector3 InitialPosition;
    public float Layer; //Largest at back
    public float LVLW, LVLH;
    public bool InvX, InvY; //Set to -1 to invert, otherwise set to 1
	// Use this for initialization
	void Start ()
    {
        MC = GameObject.FindGameObjectWithTag("MainCamera");
        InitialPosition = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update ()
    {
        float XM = MC.transform.position.x;
        float YM = MC.transform.position.y;
        if (InvX)
            XM = LVLW - XM;
        if (InvY)
            YM = LVLH - YM;
        transform.localPosition = new Vector3((XM / LVLW)*((Layer*2f)-(Layer * 0.1f)), (YM / LVLH)*((Layer*0.2f) - (Layer*0.1f)), 1) + InitialPosition;
	}
}
