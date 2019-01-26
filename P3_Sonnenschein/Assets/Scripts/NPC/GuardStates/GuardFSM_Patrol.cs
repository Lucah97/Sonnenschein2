﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class GuardFSM_Patrol : NPC_Base {

    public float distanceCutOff = 0.3f;
    public float waitBeforeTurn = 0.25f;
    public GameObject[] patrolSpots;

    private int curSpot = 0;
    private bool beginWait = false;
    private float elapsedWaitTime = 0f;

    private void Awake()
    {
        base.Awake();
    }

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        patrolSpots = animator.GetComponent<GimmeDemPatrolSpots>().patrolSpots;
        setValues(0.19f, 8f, Color.blue);

        npc = animator.gameObject;
        npcAnim = npc.transform.GetChild(0).GetComponent<Animator>();
        animator.SetBool("objDestroyed", false);

        //Spawn Symbol
        StateSymbolSpawner spawner = animator.gameObject.GetComponent<StateSymbolSpawner>();
        spawner.spawnSymbol(2);
    }

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (patrolSpots.Length == 0) return;
        npc = animator.gameObject;
        patrolSpots = animator.GetComponent<GimmeDemPatrolSpots>().patrolSpots;

        Debug.Log(npc.name);
        Debug.Log(patrolSpots[0].name);
        if (!beginWait)
        {
            if (Vector3.Distance(patrolSpots[curSpot].transform.position, npc.transform.position) < distanceCutOff)
            {
                beginWait = true;
                //Play stop animation
                npcAnim.SetBool("stopWalk", true);
                npcAnim.SetBool("Walking", false);
            }
        }
        else
        {
            elapsedWaitTime += Time.deltaTime;
            if (elapsedWaitTime > waitBeforeTurn)
            {
                curSpot++;
                if (curSpot >= patrolSpots.Length)
                {
                    curSpot = 0;
                }

                elapsedWaitTime = 0f;
                beginWait = false;

                //Play walking animation
                npcAnim.SetBool("stopWalk", false);
                npcAnim.SetBool("Walking", true);
            }
        }

        npc.GetComponent<NavMeshAgent>().SetDestination(patrolSpots[curSpot].transform.position);
    }

	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	
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
