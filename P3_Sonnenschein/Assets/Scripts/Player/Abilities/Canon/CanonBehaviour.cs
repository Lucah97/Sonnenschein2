﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanonTrajectory))]
[RequireComponent(typeof(LineRenderer))]
public class CanonBehaviour : MonoBehaviour {

    public bool allowInput = true;
    public float timeUntilDespawn = 2f;
    public float shootCoolDown = 0.3f;
    private float elapsedShootCoolDown = 0f;

    [Header("Rotation Properties")]
    public float maxRotation;
    public float rotationSpeed;
    private float curRotation;

    [Header("Aim Properties")]
    public float strength;
    public float velocityMult;
    public Transform shootSmokeSpot;

    [Header("Effect Properties")]
    public float smokeScale = 2.1f;
    public float camShakeStrenth = 3f;
    public float camShakeLength = 1.2f;

    private PlayerMovement pm;
    private CanonTrajectory ct;
    private GameObject model;

    private bool hasShot = false;
    private bool hasResetButton = false;
    private float elapsedTime = 0f;

    //### Built-In Functions ###
    void Start ()
    {
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        ct = GetComponent<CanonTrajectory>();
        model = transform.GetChild(0).GetChild(0).gameObject;

        //Reset rotation variables
        model.transform.rotation = Quaternion.Euler(Vector3.zero);
        curRotation = 0;
	}

	void Update ()
    {
        if (!hasShot)
        {
            elapsedShootCoolDown += Time.deltaTime;

            if (allowInput) { processInput(); }

            //Update canon trajectory
            applyRotation();
            ct.updateTrajectory();
        }
        else
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= timeUntilDespawn)
            {
                //Spawn Cloud effect
                FX_Spawner.instance.spawnFX(en_EffectType.SmokeCloud,
                                            transform.position,
                                            Quaternion.Euler(new Vector3(-90, 0, 0)),
                                            smokeScale);

                Destroy(this.gameObject);
            }
        }
	}

    //### Custom Functions ###
    private void processInput()
    {
        //Horizontal Input for rotation
        float horInput = Input.GetAxis("Horizontal");
        curRotation -= (horInput * rotationSpeed * Time.deltaTime);
        curRotation = Mathf.Clamp(curRotation, -maxRotation, maxRotation);

        //Reset Button
        if ((!hasResetButton) && ((Input.GetAxis("Jump") == 0) && (Input.GetAxis("Y") == 0))) { hasResetButton = true; }

        //Launch Canon
        if ((elapsedShootCoolDown > shootCoolDown) && (hasResetButton) && ((Input.GetAxis("Jump") == 1) || (Input.GetAxis("Y") == 1)))
        {
            //Setup Player after shooting
            pm.enabled = true;
            pm.setAllowIinput(false);
            pm.setVelocity(model.transform.up * (strength * velocityMult));
            pm.transform.parent = null;
            //Enable Rigidbody / Collider
            pm.GetComponent<Rigidbody>().detectCollisions = true;
            pm.setCanonMode(true);
            //Enable Rendering
            pm.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Renderer>().enabled = true;
            pm.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Renderer>().enabled = true;
            //Add camera shake
            Camera.main.GetComponent<CameraMovement>().addCameraShake(camShakeStrenth, camShakeLength);

            transform.GetChild(0).GetComponent<Animator>().SetBool("shoot", true);
            hasShot = true;

            //Spawn Cloud effect
            FX_Spawner.instance.spawnFX(en_EffectType.SmokeCloud,
                                        shootSmokeSpot.position,
                                        Quaternion.Euler(new Vector3(-90, 0, 0)),
                                        0.85f);
            //Spawn Trail effect
            GameObject trail = 
            FX_Spawner.instance.spawnFX(en_EffectType.Trail,
                            pm.transform.position,
                            Quaternion.Euler(pm.transform.up),
                            smokeScale/2.5f);
            trail.transform.parent = pm.transform;
        }
    }

    private void applyRotation()
    {
        Vector3 desiredEulers = new Vector3(0,0,curRotation);
        model.transform.rotation = Quaternion.Euler(desiredEulers);
    }
}
