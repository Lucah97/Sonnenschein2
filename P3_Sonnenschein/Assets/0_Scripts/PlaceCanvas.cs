using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceCanvas : MonoBehaviour {

    private GameObject curMessage;

    private void LateUpdate()
    {
        if (curMessage != null)
        {
            //Input
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (curMessage == null)
            {
                curMessage = UI_Spawner.instance.spawn(UI_Types.ButtonIndicator, Ctrl_Buttons.X, "Place Canvas", GameObject.FindGameObjectWithTag("Player"), new Vector3(0, 2, 0));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (curMessage != null)
            {
                GameObject.Destroy(curMessage);
                curMessage = null;
            }
        }
    }
}
