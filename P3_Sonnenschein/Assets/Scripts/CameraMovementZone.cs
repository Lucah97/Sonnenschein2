using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]

public class CameraMovementZone : MonoBehaviour {

    [Header("Camera Properties")]
    public bool Orthogonal;
    public bool followHorizontal;
    public bool followVertical;
    public bool followPlayerVelocity;
    public float playerVelocityMultiplier;
    public Vector2 clipHorizontal;
    public Vector2 clipVertical;
    public Vector2 cameraOffset;
    public float cameraDistance;
    public float cameraSmoothTime;
    [Header("Canon Override Properties")]
    [Range(0f, 1f)]
    public float overrideMovementDistance = 1f;
    public float overrideCameraDistance = 2f;

    [Header("Miscellaneous Properties")]
    public Light[] attachedLights;

    //### Built-In Functions ###
    void OnTriggerEnter(Collider other)
    {
        //Sory fur meßing wiff yo cude, you're's truthly ugahndan nuckles. *cluck* *cluck* *cluck* alsu sory fur pad inglish.
        if(Orthogonal)
        {
            if (other.CompareTag("Player")) { Camera.main.orthographic = true; }
        }
        else
        {
            if (other.CompareTag("Player")) { Camera.main.orthographic = false; }
        }

        //Assign this CMZ to the Camera
        if (other.CompareTag("Player"))
        {
            CameraMovement.instance.assignCMZ(this);
        }
    }

    //### Custom Functions ###
    public void setLightsActive(bool state)
    {
        for (int i=0; i<attachedLights.Length; i++)
        {
            attachedLights[i].enabled = state;
        }
    }


    //lul its me again
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && Orthogonal) { Camera.main.orthographic = false; }
    }
}
