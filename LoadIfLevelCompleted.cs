using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadIfLevelCompleted : MonoBehaviour {
    public GameObject[] ObjectsToHide;
    public int ReqLvl;
	// Use this for initialization
	void Start ()
    {
        if (PlayerPrefs.GetInt(ReqLvl.ToString()) > 0)
        {
            foreach(GameObject GO in ObjectsToHide)
            {
                GO.SetActive(true);
            }
        }
        else
            foreach (GameObject GO in ObjectsToHide)
            {
                GO.SetActive(false);
            }
    }
	
}
