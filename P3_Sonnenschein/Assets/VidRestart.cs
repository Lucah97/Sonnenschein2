using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidRestart : MonoBehaviour {


	void Update () {
        if (GetComponent<Renderer>().enabled)
        {
            if (Input.GetButtonDown("Jump"))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
            }
        }
	}
}
