using UnityEngine;
using System.Collections;

public class AndroidTut : MonoBehaviour {
    public string AndroidText;
	// Use this for initialization
	void Start () {
	    if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            GetComponent<TextMesh>().text = AndroidText;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
