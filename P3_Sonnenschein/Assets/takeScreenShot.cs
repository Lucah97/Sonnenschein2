using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class takeScreenShot : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ScreenCapture.CaptureScreenshot((Random.Range(-999, 999).ToString()));
            Debug.LogError("Took the screenshot, boss");
        }
    }
}
