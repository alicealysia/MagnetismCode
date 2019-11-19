using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetImage : MonoBehaviour {
    public Sprite[] Sprites;
    public float FadeTime;

    private float NextSprite;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (NextSprite > 0)
            NextSprite -= Time.deltaTime;
        else
        {
            
        }
	}
}
