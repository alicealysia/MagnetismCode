using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSettings : MonoBehaviour {
    public GameObject SettingsObject;
	
	public void OnClick ()
    {
        SettingsObject.SetActive(!SettingsObject.activeSelf);
	}
}
