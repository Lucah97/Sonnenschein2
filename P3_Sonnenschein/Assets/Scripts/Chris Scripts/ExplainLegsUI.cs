using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplainLegsUI : MonoBehaviour {


    private GameObject currMessage;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            currMessage = UI_Spawner.instance.spawn(UI_Types.ButtonIndicator, Ctrl_Buttons.X, "Spawn Legs", GameObject.Find("Player"), new Vector3(0, 2, 0));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(currMessage);
            currMessage = null;
        }
    }
}
