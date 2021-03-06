﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour {

    [Header("Ability Status")]
    public en_Abil[] currentAbilities = new en_Abil[3];

    [Header("Prefabs")]
    public GameObject legsPrefab;
    public GameObject canvasPrefab;
    public GameObject canonPrefab;

    [Header("Leg Variables")]
    public float legSpawnDistance;
    public float legSpawnRadius;

    [Header("Hide Canvas Variables")]
    public float addZdepth;
    public float canvasCoolDown;
    private float elapsedCanvasCoolDown;

    [Header("Canon Variables")]
    public Vector3 canonSpawnOffset;
    public float CanonStrenght = 14;

    //debug
    private void Update()
    {
        //Legs
        if ((Input.GetKeyDown(KeyCode.U)))
        {
            useAbility(en_Abil.PA_LEGS, -1);
        }
        if ((Input.GetKeyDown(KeyCode.I)))
        {
            useAbility(en_Abil.PA_LEGS, 1);
        }
        if (Input.GetButtonDown("X"))
        {
            useAbility(en_Abil.PA_LEGS, -(int)GetComponent<PlayerMovement>().getPlayerDirection().x);
        }
        //Hide
        if ((Input.GetButtonDown("B")) || (Input.GetKeyDown(KeyCode.O)))
        {
            useAbility(en_Abil.PA_HIDE, 0);
        }
        //Canon
        if ((Input.GetButtonDown("Y")) || (Input.GetKeyDown(KeyCode.P)))
        {
            useAbility(en_Abil.PA_CANON, 0);
        }

        //Cool Down Timers
        if ((GameObject.FindGameObjectWithTag("Canvas") == null))
        {
            elapsedCanvasCoolDown += Time.deltaTime;
        }
        else
        {
            elapsedCanvasCoolDown = 0f;
        }
    }
    //

    //### Ability Status Functions ###
    public void addAbility(en_Abil a)
    {
        if (!hasAbility(a))
        {
            for (int i = 0; i < currentAbilities.Length; i++)
            {
                if ((currentAbilities[i] == en_Abil.PA_NONE))
                {
                    currentAbilities[i] = a;
                    break;
                }
            }
        }
    }

    public void removeAbility(en_Abil a)
    {
        if (hasAbility(a))
        {
            for (int i = 0; i < currentAbilities.Length; i++)
            {
                if (currentAbilities[i] == a)
                {
                    currentAbilities[i] = en_Abil.PA_NONE;
                    break;
                }
            }
        }
    }

    public bool hasAbility(en_Abil a)
    {
        bool ret = false;
        foreach(en_Abil e in currentAbilities)
        {
            if (e == a) { ret = true; }
        }

        return ret;
    }

    //### Using Abilities ###
    public void useAbility(en_Abil a, int sup)
    {
        //Check if player is on ground
        if ((GetComponent<PlayerMovement>().isGrounded(true)) && (hasAbility(a)))
        {
            switch (a)
            {
                case (en_Abil.PA_LEGS):
                    spawnLegs(sup);
                    break;

                case (en_Abil.PA_HIDE):
                    spawnHideCanvas();
                    break;
                case (en_Abil.PA_CANON):
                    spawnCanon();
                    break;
            }
        }
        else
        {
            spawnAbilitySymbol(0);
        }
    }

    private void spawnLegs(int dir)
    {
        //Sanitize Input
        dir = Mathf.Clamp(dir, -1, 1);

        //Check if legs already exist
        if ((GameObject.FindGameObjectWithTag("Canvas") == null))
        {
            //Check if Legs exist
            GameObject nLegs = GameObject.FindGameObjectWithTag("Legs");
            if (nLegs != null)
            {
                nLegs.GetComponent<LegBehaviour>().deSpawn();
            }

            Vector3 desiredPosition = transform.position;
            desiredPosition.x += ((legSpawnDistance * transform.localScale.x) * dir);

            //Check if legs collide with surroundings
            Collider[] hitColliders = Physics.OverlapSphere(desiredPosition, legSpawnRadius);
            bool hasFoundWall = false;
            int i = 0;

            while (i < hitColliders.Length)
            {
                if (hitColliders[i].CompareTag("Solid"))
                {
                    hasFoundWall = true;
                }
                i++;
            }

            if (!hasFoundWall)
            {
                GameObject legs = Instantiate(legsPrefab, desiredPosition, legsPrefab.transform.rotation);
                legs.GetComponent<LegBehaviour>().movementDirection = ((dir < 0) ? false : true);

                //Spawn Cloud effect
                FX_Spawner.instance.spawnFX(en_EffectType.SmokeCloud,
                                            legs.transform.position,
                                            Quaternion.Euler(new Vector3(-90, 0, 0)),
                                            0.5f);

                return;
            }
        }

        spawnAbilitySymbol(0);
    }

    private void spawnHideCanvas()
    {
        if ((GameObject.FindGameObjectWithTag("Canvas") == null) && (GameObject.FindGameObjectWithTag("Canon") == null))
        {
            if (elapsedCanvasCoolDown >= canvasCoolDown)
            {
                float curZ = transform.position.z;
                curZ += addZdepth;

                PlayerMovement pm = GetComponent<PlayerMovement>();

                Vector3 canvasPosition = transform.position;
                canvasPosition.z += (addZdepth / 2);
                Instantiate(canvasPrefab, canvasPosition, canvasPrefab.transform.rotation);

                pm.setZdepth(curZ, true);
                pm.setAllowIinput(false);
                pm.freezeVelocity();
            }
        }
    }

    private void spawnCanon()
    {
        if ((GameObject.FindGameObjectWithTag("Canon") == null) && (GameObject.FindGameObjectWithTag("Canvas") == null))
        {
            GameObject nCanon = Instantiate(canonPrefab, transform.position + canonSpawnOffset, canonPrefab.transform.rotation);
            Vector3 nPlayerPos = transform.position;
            nPlayerPos += ((new Vector3(Mathf.Abs(canonSpawnOffset.x),
                                        Mathf.Abs(canonSpawnOffset.y),
                                        Mathf.Abs(canonSpawnOffset.z)) * 2));
            nCanon.GetComponent<CanonBehaviour>().strength = CanonStrenght;
            nCanon.GetComponent<CanonTrajectory>().strength = CanonStrenght;
            transform.position = nPlayerPos;
            transform.parent = nCanon.transform.GetChild(0).GetChild(0).transform;
            GetComponent<PlayerMovement>().freezeVelocity();
            GetComponent<PlayerMovement>().enabled = false;
            //Disable Rigidbody and Colliders
            GetComponent<Rigidbody>().detectCollisions = false;
            GetComponent<PlayerMovement>().setColliderActive(false);
            //Disable Rendering
            transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Renderer>().enabled = false;
            transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Renderer>().enabled = false;
            //Delete UI Messages
            UI_Spawner.instance.destroyAllElements();

            //Spawn Cloud effect
            FX_Spawner.instance.spawnFX(en_EffectType.SmokeCloud,
                                        nCanon.transform.position,
                                        Quaternion.Euler(new Vector3(-90, 0, 0)),
                                        0.85f);
        }
    }

    private void spawnAbilitySymbol(int index)
    {
        GetComponent<StateSymbolSpawner>().spawnSymbol(index);
    }

    public bool isHidden()
    {
        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
        if (canvas != null)
        { return true; } else { return false; }
    }
}

public enum en_Abil
{
    PA_NONE,
    PA_LEGS,
    PA_HIDE,
    PA_CANON
};