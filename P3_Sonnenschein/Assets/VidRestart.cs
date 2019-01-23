using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidRestart : MonoBehaviour {

    public bool isDead = false;

	void Update () {
        if (GetComponent<Renderer>().enabled)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>().enabled = false;

            if (Input.GetButtonDown("Jump"))
            {
                if (isDead)
                {
                    isDead = false;
                    UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
                }
                else
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
        }
	}
}
