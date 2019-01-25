using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonStronger : MonoBehaviour {

    public float CanonStrenght = 50;

	// Use this for initialization
	void Start () {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAbilities>().CanonStrenght = CanonStrenght;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
