using UnityEngine;
using System.Collections;

public class RemoveMags : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
	    if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            GameObject.Destroy(gameObject);
        }
	}
}
