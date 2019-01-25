using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidRestart : MonoBehaviour {

    public bool isDead = false;

    public bool customload = false;

    public string CustomScene;

	void Update () {
        if (GetComponent<Renderer>().enabled)
        {
            if (customload)
            {
                LoadcustomScene(CustomScene);
            }
            else { 
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

    public void LoadcustomScene(string SceneName)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>().enabled = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName);
    }
}
