using UnityEngine;
using System.Collections;

public class ConSpr_Code : MonoBehaviour {
    private GameObject Contravert;

    void Start ()
    {
        Contravert = GameObject.FindGameObjectWithTag("Contravert");
    }
	// Update is called once per frame
	void Update ()
    {
        transform.position = Contravert.transform.position;
	}
}
