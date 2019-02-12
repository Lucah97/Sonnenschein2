using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class RestartLevel : MonoBehaviour {

	// Update is called once per frame
	void LateUpdate () {
        /*
		if (Input.GetKeyDown(KeyCode.Space))
        {
            if (GetComponent<Image>().enabled)
            {
                SceneManager.LoadScene(0);
            }
        }*/
        if (GetComponent<PlayerMovement>().allowInput)
        {
            if (Input.GetButton("Start"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
	}
}
