using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV_Camera : MonoBehaviour {

    public RenderTexture renTex;

	void Start () {
        GetComponent<Camera>().targetTexture = renTex;
	}
	
}
