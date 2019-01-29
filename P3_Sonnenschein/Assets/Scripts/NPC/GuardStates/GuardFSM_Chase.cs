using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class GuardFSM_Chase : NPC_Base {

    public float chaseMaxDistance;

    private void Awake()
    {
        base.Awake();
    }

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        setValues(0.55f, 7f, Color.red);

        //Alerted animation
        npcAnim.SetBool("alerted", true);
        npcAnim.SetBool("stopWalk", false);
        npcAnim.SetBool("Walking", true);

        //Spawn Symbol
        StateSymbolSpawner spawner = animator.gameObject.GetComponent<StateSymbolSpawner>();
        spawner.spawnSymbol(1);

        //Vision Cone Flash Color
        vcd.startFillColor(Color.red);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Check deleted Obj
        if (!followObj)
        {
            animator.SetBool("objDestroyed", true);
        }
        else
        {
            if (Vector3.Distance(npc.transform.position, followObj.transform.position) > chaseMaxDistance)
            {
                followObj = null;
                animator.SetBool("objDestroyed", true);
            }
            else
            {
                NavMeshPath path = new NavMeshPath();
                Vector3 followPos = new Vector3(followObj.transform.position.x, npc.transform.position.y, followObj.transform.position.z);
                if (npc.GetComponent<NavMeshAgent>().CalculatePath(followPos, path))
                {
                    npc.GetComponent<NavMeshAgent>().SetDestination(followPos);
                }
                else
                {
                    followObj = null;
                    animator.SetBool("objDestroyed", true);
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
