﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class GuardFSM_Suspicious : NPC_Base {

    private Vector3[] susPoints;
    private int curSpot;
    private StateSymbolSpawner spawner;
    private bool beginWait = false;
    private float elapsedWaitTime = 0f;
    

    public float accuracy = 2f;
    public float distanceCutOff = 0.3f;
    public float waitBeforeTurn = 1f;

    private void Awake()
    {
        base.Awake();
    }

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        setValues(0.23f, 7f, Color.yellow);

        //Spawn Symbol
        spawner = animator.gameObject.GetComponent<StateSymbolSpawner>();
        spawner.spawnSymbol(0);

        //Alerted animation
        npcAnim.Play("Alerted");
        npcAnim.SetBool("alerted", true);
        npcAnim.SetBool("stopWalking", false);
        npcAnim.SetBool("Walking", true);

        //Vision Cone Flash Color
        vcd.startFillColor(Color.yellow);

        //Creating suspicious points
        susPoints = new Vector3[Random.Range(5, 8)];
        for (int i=0; i<susPoints.Length; i++)
        {
            susPoints[i] = followObj.transform.position;
            susPoints[i].y = npc.transform.position.y;
            susPoints[i].x += Random.Range(-accuracy, accuracy);
        }
        susPoints[0] = followObj.transform.position;
        susPoints[0].y = npc.transform.position.y;
        curSpot = 0;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (susPoints.Length == 0) return;

        if (!beginWait)
        {
            if (Vector3.Distance(susPoints[curSpot], npc.transform.position) < distanceCutOff)
            {
                beginWait = true;
                //Play stop animation
                npcAnim.SetBool("Walking", false);
            }                               
        }
        else
        {
            elapsedWaitTime += Time.deltaTime;
            if (elapsedWaitTime > waitBeforeTurn)
            {
                curSpot++;

                elapsedWaitTime = 0f;
                beginWait = false;

                //Play walking animation
                npcAnim.Play("StartWalk");
                npcAnim.SetBool("Walking", true);

                if (curSpot >= susPoints.Length-1)
                {

                    animator.SetBool("hasSearched", true);
                }
                else
                {
                    spawner.spawnSymbol(0);
                }

                if (curSpot < susPoints.Length)
                {
                    npc.GetComponent<NavMeshAgent>().SetDestination(susPoints[curSpot]);
                }
            }
        }
    }

    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{

    //}
    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
