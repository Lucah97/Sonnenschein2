using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;

public class LevelEndDoor : MonoBehaviour, InterfaceLetherTrigger {

    public VideoClip end;

    private bool isOpen = false;

    public void OnSwitchTrigger()
    {
        transform.GetChild(1).rotation = Quaternion.Euler(0, 150, 0);
        isOpen = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((isOpen) && (other.tag == "Player"))
        {
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
