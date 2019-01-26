using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class Desaturate : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PostProcessingProfile PPP = Camera.main.GetComponent<PostProcessingBehaviour>().profile;
        var curHue = PPP.colorGrading.settings;

        curHue.basic.hueShift = Mathf.Lerp(-180, 180, 0.5f);
        curHue.basic.saturation = 0f;
        PPP.colorGrading.settings = curHue;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
