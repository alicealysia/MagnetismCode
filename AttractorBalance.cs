using UnityEngine;
using System.Collections;

public class AttractorBalance : MonoBehaviour {

    public bool Gset = true;
    public Vector2 MagnetizePos;
    private Rigidbody2D CharRig;
	// Use this for initialization
	void Start ()
    {
        CharRig = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (!Gset)
        {
            CharRig.AddForce(((MagnetizePos - CharRig.position)/4) , ForceMode2D.Impulse);
            CharRig.velocity *= 0.925f;
        }
    }

    public void ChangeMags()
    {
        GameObject[] GO =
            GameObject.FindGameObjectsWithTag("Attractor_tag");

        MagnetizePos = Vector2.zero;

        if (GO.Length > 0)
        {
            int j = 0;
            foreach (GameObject i in GO)
            {
                i.transform.rotation = Quaternion.identity;
                j++;
                MagnetizePos += new Vector2(i.transform.position.x, i.transform.position.y);
            }
            MagnetizePos /= j;
            if (Gset)
            {
                CharRig.gravityScale = 0;
                Gset = false;
            }
        }
        else if (!Gset)
        {
            CharRig.gravityScale = 1;
            Gset = true;
        }
    }
}
