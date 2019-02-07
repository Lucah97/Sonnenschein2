using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotorTurn : MonoBehaviour {

    public float TurnSpeed;

	// WOW THE COMPLEXITY OF THIS SCRIPT IS TOO DAMN HIGH
	void Update () {

        float rotateX = transform.rotation.x;

        rotateX += TurnSpeed * Time.deltaTime;

        transform.rotation = new Quaternion(rotateX, transform.rotation.y, transform.rotation.z, transform.rotation.w);
		
	}
}
