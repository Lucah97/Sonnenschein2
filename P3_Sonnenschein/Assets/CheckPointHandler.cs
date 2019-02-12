using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CheckPointHandler : MonoBehaviour {

    public static CheckPointHandler instance;

    public int lastBuildIndex = 0;
    public Vector3 lastPosition = Vector3.zero;

    private void Awake()
    {
        //Singleton Instance
        if ((instance != this) && (instance != null))
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            resetPlayer();
        }
    }

    public void Start()
    {
        resetPlayer();
    }

    private void resetPlayer()
    {
        int curBuildIndex = SceneManager.GetActiveScene().buildIndex;

        if (curBuildIndex == lastBuildIndex)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = lastPosition;

            Vector3 curCameraPos = Camera.main.transform.position;
            curCameraPos.x = lastPosition.x;
            curCameraPos.y = lastPosition.y;
            Camera.main.transform.position = curCameraPos;
        }
    }

    public void setCheckPoint(Vector3 position)
    {
        lastBuildIndex = SceneManager.GetActiveScene().buildIndex;
        lastPosition = position;
    }

    public Vector3 getPlayerPosition()
    {
        int curBuildIndex = SceneManager.GetActiveScene().buildIndex;

        if (curBuildIndex == lastBuildIndex)
        {
            return lastPosition;
        }
        else
        {
            return GameObject.FindGameObjectWithTag("Player").transform.position;
        }
    }
}
