using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanonTrajectory))]
[RequireComponent(typeof(LineRenderer))]
public class CanonBehaviour : MonoBehaviour {

    public bool allowInput = true;
    public float timeUntilDespawn = 2f;

    [Header("Rotation Properties")]
    public float maxRotation;
    public float rotationSpeed;
    private float curRotation;

    [Header("Aim Properties")]
    public float strength;
    public float velocityMult;
    public Transform shootSmokeSpot;

    private PlayerMovement pm;
    private CanonTrajectory ct;
    private GameObject model;

    private bool hasShot = false;
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
                                            2.1f);

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

        //Launch Canon
        if (Input.GetAxis("Jump") == 1)
        {
            pm.enabled = true;
            pm.setAllowIinput(false);
            pm.setCanonMode(true);
            pm.setVelocity(model.transform.up * (strength * velocityMult));
            pm.transform.parent = null;

            transform.GetChild(0).GetComponent<Animator>().SetBool("shoot", true);
            hasShot = true;

            //Spawn Cloud effect
            FX_Spawner.instance.spawnFX(en_EffectType.SmokeCloud,
                                        shootSmokeSpot.position,
                                        Quaternion.Euler(new Vector3(-90, 0, 0)),
                                        0.85f);
        }
    }

    private void applyRotation()
    {
        Vector3 desiredEulers = new Vector3(0,0,curRotation);
        model.transform.rotation = Quaternion.Euler(desiredEulers);
    }
}
