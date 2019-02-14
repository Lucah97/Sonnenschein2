using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour {

    public AudioClip musicMain;

	// Use this for initialization
	void Start () {
        GetComponent<AudioSource>().PlayOneShot(musicMain);
	}
	
	// Update is called once per frame
	void Update () {
		if (!GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().PlayOneShot(musicMain);
        }
        if (Camera.main.transform.GetChild(0).GetComponent<Renderer>().enabled)
        {
            GetComponent<AudioSource>().Stop();
            this.gameObject.SetActive(false);
        }
	}
}
