using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceCanvas : MonoBehaviour {

    [Header("UI Message")]
    public UI_Types uitype;
    public Ctrl_Buttons button;
    public string text;

    public GameObject DoorToOpen;
    public bool isActivated = false;
    private GameObject curMessage;

    private void OnTriggerStay(Collider other)
    {
         if (other.CompareTag("Player"))
         {
            if ((Input.GetAxis("RT") > 0f) && (!isActivated))
            {
                if (!other.GetComponent<PlayerMovement>().getCanonMode())
                {
                    //Activate Renderer
                    transform.GetChild(0).GetComponent<Renderer>().enabled = true;
                    isActivated = true;
                    //Destroy Message
                    GameObject.Destroy(curMessage);
                    curMessage = null;
                    //Open Door
                    DoorToOpen.GetComponent<InterfaceLetherTrigger>().OnSwitchTrigger();

                    //SetCheckPoint
                    CheckPointHandler.instance.setCheckPoint(other.transform.position);

                    //Spawn Text
                    GameObject nText = other.GetComponent<StateSymbolSpawner>().spawnSymbol(1);
                    nText.GetComponent<TextMesh>().text = "Checkpoint Reached!";
                    nText.GetComponent<TextMesh>().color = Color.green;
                }
            }
         }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!other.GetComponent<PlayerMovement>().getCanonMode())
            {
                if ((curMessage == null) && (!isActivated))
                {
                    curMessage = UI_Spawner.instance.spawn(uitype, button, text, GameObject.FindGameObjectWithTag("Player"), new Vector3(0, 2, 0));
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if ((curMessage != null))
            {
                GameObject.Destroy(curMessage);
                curMessage = null;
            }
        }
    }
}
