using UnityEngine;
using System.Collections;

public class FollowChar : MonoBehaviour {

    //this class is really shit, if I intend to use the chase cam I should redo the whole thing.

    private GameObject Conv;
    private Vector3 Target;
    public float MincX, MincY, MaxcX, MaxcY, MinX, MinY, MaxX, MaxY;
	// Use this for initialization
	void Start ()
    {
        Conv = GameObject.FindGameObjectWithTag("Contravert");
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Conv.transform.position.x > MincX && Conv.transform.position.y > MincY && Conv.transform.position.x < MaxcX && Conv.transform.position.y < MaxcY)
        {
            Target.z = -10;
            if (Conv.transform.position.x < MinX)
                Target.x = MinX;
            else if (Conv.transform.position.x > MaxX)
                Target.x = MaxX;
            else
                Target.x = Conv.transform.position.x;

            if (Conv.transform.position.y < MinY)
                Target.y = MinY;
            else if (Conv.transform.position.y > MaxY)
                Target.y = MaxY;
            else
                Target.y = Conv.transform.position.y;
            transform.position = Vector3.MoveTowards(transform.position, Target, 1);
        }
	}
}
