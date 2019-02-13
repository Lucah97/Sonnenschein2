using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;

public class VidRestart : MonoBehaviour {

    public bool isDead = false;
    public bool customload = false;
    public string CustomScene;
    public float waitForInput = 2f;
    public VideoClip blackVid;

    public Vector3 perspScale;
    public Vector3 orthoScale;

    private float elapsedTime = 0f;
    private bool hasDisabled = false;

    private void Start()
    {
        resetVideoToBlack();
        GetComponent<VideoPlayer>().waitForFirstFrame = true;
    }

    void Update () {
        if (GetComponent<Renderer>().enabled)
        {
            adjustScreenScale();
            if (customload)
            {
                LoadcustomScene(CustomScene);
            }
            else
            {
                if (!hasDisabled)
                {
                    //Disable Player Collider
                    GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>().enabled = false;
                    //Disable Guards
                    foreach (GameObject e in GameObject.FindGameObjectsWithTag("Enemy"))
                    {
                        e.SetActive(false);
                    }
                    //Disable text Meshes
                    TextMesh[] foundT = FindObjectsOfType<TextMesh>();
                    foreach (TextMesh t in foundT)
                    {
                        t.gameObject.SetActive(false);
                    }

                    hasDisabled = true;
                }

                elapsedTime += Time.deltaTime;

                if ((Input.GetButtonDown("Jump")) && (elapsedTime > waitForInput))
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

                    elapsedTime = 0f;
                    resetVideoToBlack();
                }
            }
        }
	}

    private void adjustScreenScale()
    {
        bool ortho = Camera.main.orthographic;

        transform.localScale = ((ortho) ? orthoScale : perspScale);
    }

    public void LoadcustomScene(string SceneName)
    {
        elapsedTime = 0f;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>().enabled = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName);
    }

    public void resetVideoToBlack()
    {
        GetComponent<VideoPlayer>().clip = blackVid;
        GetComponent<VideoPlayer>().Play();
    }
}