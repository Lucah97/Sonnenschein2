﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]

public class LegBehaviour : MonoBehaviour {

    public float movementSpeed;
    public bool movementDirection;

    private float curTime = 0f;
    public float maxTime;
    public float timeAfterLeadingEnemy = 2.3f;
    public float speedMultAfterLeadingEnemy = 2.6f;

    private int curBumps = 0;
    public int maxBumps;

    public float collisionMult;

    public GameObject despawnCloud;

    private bool leadingEnemy = false;

    private Rigidbody rb;
    private StateSymbolSpawner sss;

    //### Built-In Functions ###
    void Start() {
        rb = GetComponent<Rigidbody>();
        sss = GetComponent<StateSymbolSpawner>();
    }

    void Update () {
        legMovement();
        processTime();

        if (curBumps > (maxBumps))
        {
            deSpawn();
        }
	}

    //Hit Wall
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Solid"))
        {
            float curY = transform.position.y;
            float colHeight = GetComponent<CapsuleCollider>().height * transform.localScale.y;
            colHeight *= collisionMult;

            bool turnAround = false;
            foreach (ContactPoint p in collision.contacts)
            {
                float curYcontact = p.point.y;
                if (Mathf.Abs(curY-curYcontact) < (colHeight / 2))
                {
                    turnAround = true;
                }
            }

            if (turnAround)
            {
                //Spawn Indicator Symbol
                GameObject nSym = sss.spawnSymbol(0);
                TextMesh nText = nSym.GetComponent<TextMesh>();
                nText.text = (maxBumps - curBumps).ToString();

                //Reverse Direction / Count Bumps
                movementDirection = !movementDirection;
                curBumps++;
            }
        }
    }

    //Hit Enemy
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && (!leadingEnemy))
        {
            leadingEnemy = true;
            movementSpeed *= speedMultAfterLeadingEnemy;
            maxTime = curTime + timeAfterLeadingEnemy;
        }
    }

    //### Custom Functions ###
    private void legMovement() {
        int movDir = (movementDirection ? 1 : -1);
        Vector3 curVel = rb.velocity;
        Vector3 newVel = new Vector3(((movementSpeed * transform.localScale.x) * movDir), curVel.y, 0);
        transform.GetChild(0).rotation = Quaternion.Euler(new Vector3(0f, movDir * 90f, 0f));

        rb.velocity = newVel;
    }

    private void processTime()
    {
        curTime += Time.deltaTime;

        if (curTime > maxTime)
        {
            deSpawn();
        }
    }

    public void deSpawn()
    {
        //Disable Collider
        GetComponent<Collider>().enabled = false;

        //Reset Tag
        this.tag = "Untagged";

        //Spawn Cloud effect
        if (despawnCloud != null)
        {
            Instantiate(despawnCloud, transform.position, despawnCloud.transform.rotation);
        }

        //> Later: Leg Animation <//

        Destroy(this.gameObject);
    }
}
