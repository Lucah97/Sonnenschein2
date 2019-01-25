using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;

public class LevelEndDoor : MonoBehaviour, InterfaceLetherTrigger {

    public VideoClip end;

    private bool isOpen = false;

    [Header("Custom Door")]

    public bool LoadCustomScene;

    public string customscene;

    public static Transform LastPos;

    public static bool Returning;

    private void Start()
    {
       if(Returning )
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = LastPos.position;
            GameObject.FindGameObjectWithTag("Player").transform.rotation = LastPos.rotation;
            Returning = false;
        }
    }

    public void OnSwitchTrigger()
    {
        transform.GetChild(1).rotation = Quaternion.Euler(0, 150, 0);
        isOpen = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((isOpen) && (other.tag == "Player"))
        {
            if (LoadCustomScene)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    LastPos = GameObject.FindGameObjectWithTag("Player").transform;
                    Camera.main.gameObject.transform.GetChild(0).GetComponent<VidRestart>().CustomScene = customscene;
                    Camera.main.gameObject.transform.GetChild(0).GetComponent<VidRestart>().customload = true;

                    Camera.main.gameObject.transform.GetChild(0).GetComponent<Renderer>().enabled = true;

                    Returning = true;
                }
            }

            Debug.Log("afafafafafa");

            //Enable video
            Camera.main.gameObject.transform.GetChild(0).GetComponent<Renderer>().enabled = true;
            Camera.main.gameObject.transform.GetChild(0).GetComponent<VideoPlayer>().clip = end;
            Camera.main.gameObject.transform.GetChild(0).GetComponent<VideoPlayer>().Play();

            //Disable Controls
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = false;
        }
    }
}
