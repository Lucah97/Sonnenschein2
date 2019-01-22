using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceCanvas : MonoBehaviour {

    [Header("UI Message")]
    public UI_Types uitype;
    public Ctrl_Buttons button;
    public string text;

    public bool isActivated = false;
    private GameObject curMessage;

    private void LateUpdate()
    {
        if (curMessage != null)
        {
            if (Input.GetAxis("RT")>0)
            {
                transform.GetChild(0).GetComponent<Renderer>().enabled = true;
                isActivated = true;
                GameObject.Destroy(curMessage);
                curMessage = null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if ((curMessage == null) && (!isActivated))
            {
                curMessage = UI_Spawner.instance.spawn(uitype, button, text, GameObject.FindGameObjectWithTag("Player"), new Vector3(0, 2, 0));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if ((curMessage != null) && (!isActivated))
            {
                GameObject.Destroy(curMessage);
                curMessage = null;
            }
        }
    }
}
