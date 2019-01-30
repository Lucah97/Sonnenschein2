using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRotationLerp : MonoBehaviour {

    public Transform[] stations;
    public float lerpSpeed = 2f;
    public float timeUntilMove = 3.5f;
    private bool BeginTime = false;
    public float BeginTimer = 0;
    private float curTime = 0;

    private float elapsedTime = 0f;
    private int curIndex = 0;

    private void Start()
    {
        
    }


    // Update is called once per frame
    void Update () {

        if (!BeginTime)
        {
            if(curTime < BeginTimer)
            {
                curTime += Time.deltaTime;
            }
            else { BeginTime = true; curTime = 0; }
        }
        else
        {

            transform.rotation = Quaternion.Lerp(transform.rotation, stations[curIndex].rotation, Time.deltaTime * lerpSpeed);

            elapsedTime += Time.deltaTime;
            if (elapsedTime > timeUntilMove)
            {
                elapsedTime = 0;
                if (curIndex < (stations.Length - 1))
                {
                    curIndex++;
                }
                else
                {
                    curIndex = 0;
                }
            }
        }
	}
}
