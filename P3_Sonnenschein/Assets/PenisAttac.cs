using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenisAttac : MonoBehaviour {

    [Header("Attac Pattern")]

    public float TimeBeforeFirstAttac = 8;
    public float TimeBetweenAttac = 5;
    private float curTime = 0;
    private bool first = true;

    private bool SSK=false;
    private float SteinOriginalY;

    [Header("Warning")]
    public GameObject WarningCylinder;
    public GameObject WarningText;

    public GameObject DealerDick;
    public GameObject DealerDickEichel;

    public int WarnAmount = 3;
    private int CurWarnAmount;

    private float warnTime;
    private bool warn;
    private bool warned = false;

    [Header("Penetration")]
    public GameObject Penis;
    public GameObject Steinkreis;
    public AnimationClip PenetrationAnimation;

    public Penetration Pen;

    private bool Attacking = false;
    private float curAttacTime = 0;

	// Use this for initialization
	void Start () {
        Steinkreis.SetActive(false);
        WarningText.SetActive(false);
        WarningCylinder.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

        //ATTACKPATTERN
        if (first)
        {
            if (curTime < TimeBeforeFirstAttac)
            {
                curTime += Time.deltaTime;
            }
            else
            {
                curTime = 0;
                first = false;
                attac(GameObject.FindGameObjectWithTag("Player").transform.position);
            }
        }
        else
        {
            if(curTime < TimeBetweenAttac)
            {
                curTime += Time.deltaTime;
            }
            else
            {
                curTime = 0;
                attac(GameObject.FindGameObjectWithTag("Player").transform.position);
            }
        }

        //WARNING
        if (warn)
        {
           // Debug.Log("Carefull now!");
            if (CurWarnAmount > 0)
            {
                if (warnTime < 0.5f)
                {
                    if (!WarningCylinder.activeSelf) { WarningCylinder.SetActive(true); }
                    if (!WarningText.activeSelf) { WarningText.SetActive(true); }

                    warnTime += Time.deltaTime;
                }
                else if (warnTime < 1 && warnTime > 0.5f)
                {
                    WarningText.SetActive(false);
                    //WarningCylinder.SetActive(false);
                    warnTime += Time.deltaTime;
                }
                else
                {
                   // Debug.Log("CurWarnAmount" + CurWarnAmount);
                    CurWarnAmount--;
                    warnTime = 0;
                }
            }
            else
            {
               // Debug.Log("Done Warning");
                WarningCylinder.SetActive(false);
                warn = false;
                warned = true;
                attac(transform.position);
            }

        }

        //ATACKING
        if(Attacking)
        {
            //Debug.Log("PENETRATION!");
            if(curAttacTime < PenetrationAnimation.length - 0.7f)
            {
                curAttacTime += Time.deltaTime;
            }
            else
            {
                //Penis.GetComponent<Animation>().Stop();
                Penis.GetComponent<Animator>().SetBool("Penetrate", false);

                sinkSteinKreis();

                warned = false;
                Attacking = false;

                Penis.GetComponent<Penetration>().FirstColl = true;
            }
        }

        //SteinkreisLerp
        if(SSK)
        {
            float Lerp = 0.5f * Time.deltaTime;

            Steinkreis.transform.position = new Vector3(Steinkreis.transform.position.x, Steinkreis.transform.position.y - Lerp, Steinkreis.transform.position.z);

            if(Steinkreis.transform.position.y <= SteinOriginalY - 2)
            {
                SSK = false;
                Steinkreis.transform.position = new Vector3(Steinkreis.transform.position.x, SteinOriginalY, Steinkreis.transform.position.z);
                Steinkreis.SetActive(false);
            }
        }
		
	}

    private void sinkSteinKreis()
    {
        SteinOriginalY = Steinkreis.transform.position.y;
        SSK = true;
    }

    public void attac(Vector3 Position)
    {
        if (!warned)
        {
            transform.position = new Vector3( Position.x, transform.position.y, transform.position.z);
            warning();
        }
        else
        {
            Attacking = true;
            curAttacTime = 0;
            //Penis.GetComponent<Animation>().clip = PenetrationAnimation;
            //Penis.GetComponent<Animation>().Play();
            Penis.GetComponent<Animator>().SetBool("Penetrate", true);

            Steinkreis.SetActive(true);
        }

    }

    private void warning()
    {
        CurWarnAmount = WarnAmount;
        warn = true;
        warnTime = 0;

        DealerDick.GetComponent<Animator>().SetBool("DickStrech", true);
        DealerDickEichel.GetComponent<Animator>().SetBool("DickStrech", true);
    }
    
}
