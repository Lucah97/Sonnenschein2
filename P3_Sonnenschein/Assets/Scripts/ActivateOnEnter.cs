using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOnEnter : MonoBehaviour {

    public GameObject[] ActivateItems;

	// Use this for initialization
	void Start () {
        foreach (GameObject Item in ActivateItems)
        {
            Item.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            foreach (GameObject Item in ActivateItems)
            {
                Item.SetActive(true);
            }
        }
    }
}
