using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;

public class DestroyLegs : MonoBehaviour {

    public float radius;
    public VideoClip loose;

    // Update is called once per frame
    void LateUpdate () {
        Collider[] curCol = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider c in curCol)
        {
            if (c.CompareTag("Legs"))
            {
                //GameObject.Destroy(c.gameObject);
            }

            if (c.CompareTag("Player"))
            {
                //Stuff
            }
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        //Enable video
        Camera.main.gameObject.transform.GetChild(0).GetComponent<Renderer>().enabled = true;
        Camera.main.gameObject.transform.GetChild(0).GetComponent<VideoPlayer>().clip = loose;
        Camera.main.gameObject.transform.GetChild(0).GetComponent<VideoPlayer>().Play();

        //Disable Controls
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = false;
    }
}
