using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplainLegsUI : MonoBehaviour {

    public UI_Types typ;
    public Ctrl_Buttons displayButton;
    public string displayText;

    private GameObject currMessage;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            currMessage = UI_Spawner.instance.spawn(typ, displayButton, displayText, GameObject.Find("Player"), new Vector3(0, 2, 0));
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
