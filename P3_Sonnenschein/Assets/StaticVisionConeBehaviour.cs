using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class StaticVisionConeBehaviour : MonoBehaviour {

    public VideoClip loose;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerAbilities pa = other.GetComponent<PlayerAbilities>();
            if (!pa.isHidden())
            {
                //Enable video
                Camera.main.gameObject.transform.GetChild(0).GetComponent<Renderer>().enabled = true;
                Camera.main.gameObject.transform.GetChild(0).GetComponent<VideoPlayer>().clip = loose;
                Camera.main.gameObject.transform.GetChild(0).GetComponent<VideoPlayer>().Play();
                Camera.main.gameObject.transform.GetChild(0).GetComponent<VidRestart>().isDead = true;

                //Disable Controls
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = false;
            }
        }
    }
}
