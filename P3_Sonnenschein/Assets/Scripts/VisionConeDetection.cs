using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]

public class VisionConeDetection : MonoBehaviour
{
    [Header("Cone Properties")]
    [Range(-100f, 100f)]
    public float coneScale = 0f;
    [Range(-100f, 100f)]
    public float coneLength = 0f;
    public Vector2 tilingClamp;
    public Texture2D SuspiciousFill;
    public Texture2D ChaseFill;

    [Header("Detection Properties")]
    public bool resetProgressOnLeave = false;
    [Range(0f, 50f)]
    public float detectionSpeed = 1f;
    public float detectionDecreaseSpeed = 0.7f;
    public Vector2 detectionClamp;
    public string[] acceptedTags;
    public float[] tagSpecificSpeed;

    private GameObject colObj = null;
    private GameObject followObj = null;

    private Transform legs = null;
    private Transform originPoint;
    private GameObject player;

    private float detectionProgress = 0f;

    //### Built-In Functions ###
    private void Start()
    {
        originPoint = transform.GetChild(0);
        player = GameObject.FindGameObjectWithTag("Player");

        setConeDimensions(coneScale, coneLength);
    }

    private void LateUpdate()
    {
        detectionProgress = Mathf.Clamp(detectionProgress, detectionClamp.x, detectionClamp.y);

        //Apply current detection progress to the material
        applyVisualProgress(detectionProgress);

        //Decrease progress
        if ((!colObj) && (!resetProgressOnLeave))
        {
            detectionProgress = Mathf.Lerp(detectionProgress, 0f, Time.deltaTime * detectionDecreaseSpeed);
        }

        //Pass the current detection Progress into the animator
        setAnimatorDistance(detectionProgress);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (acceptedTagsContain(other.tag))
        {
            PlayerAbilities pa = other.GetComponent<PlayerAbilities>() ?? null;
            if ( ((pa) && (!pa.isHidden())) || (!pa) )
            {
                colObj = other.gameObject;
                followObj = other.gameObject;
                resetDetectionProgress();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if ((acceptedTagsContain(other.tag)) && (colObj != null))
        {
            detectionProgress += ((detectionSpeed * Time.deltaTime) * getTagSpecificSpeed(other.tag));
            detectionProgress = Mathf.Clamp(detectionProgress, 0f, 100f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (acceptedTagsContain(other.tag))
        {
            colObj = null;
            resetDetectionProgress();

            applyVisualProgress(detectionProgress);
        }
    }

    //### Custom Functions ###

    public GameObject getColObj()
    {
        return ((colObj != null) ? colObj : new GameObject());
    }

    public GameObject getFollowObj()
    {
        return ((followObj != null) ? followObj : new GameObject());
    }

    public void startFillColor(Color nColor)
    {
        StartCoroutine(fillFullColor(nColor));
    }

    private IEnumerator fillFullColor(Color nColor)
    {
        nColor.a = 32;
        Material curMat = GetComponent<Renderer>().material;
        curMat.SetColor("_TintColor", nColor);

        yield return new WaitForSeconds(0.3f);
    }

    private void setConeDimensions(float addScale, float addLength)
    {
        Vector3 curDimensions = transform.localScale;
        curDimensions.x += addScale;
        curDimensions.y += addLength;
        curDimensions.z += addScale;

        transform.localScale = curDimensions;
    }

    private void applyVisualProgress(float progress)
    {
        //Get material
        Material curMat = GetComponent<Renderer>().material;

        //Assign texture based on NPC State
        curMat.SetTexture("_MainTex", (getAnimatorState() == "Chase") ? ChaseFill : SuspiciousFill);
        curMat.SetColor("_TintColor", new Color(255, 255, 255, 0.0005f));

        float visOffset = -((progress / detectionClamp.y) * Mathf.Abs(tilingClamp.x));
        visOffset = Mathf.Clamp(visOffset, tilingClamp.x, tilingClamp.y);
        
        curMat.SetTextureOffset("_MainTex", new Vector2(visOffset, 0));
    }

    private string getAnimatorState()
    {
        AnimatorStateInfo curState = transform.parent.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
        
        if (curState.IsName("Chase"))
        {
            return "Chase";
        }
        if (curState.IsName("Suspicious") || curState.IsName("Patrol"))
        {
            return "Suspicious";
        }
        return "";
    }

    private void setAnimatorDistance(float curDist)
    {
        Animator anim = transform.parent.GetComponent<Animator>();

        anim.SetFloat("ConeDistance", curDist);
    }

    private bool acceptedTagsContain(string nTag)
    {
        foreach (string s in acceptedTags)
        {
            if (s == nTag) { return true; }
        }
        return false;
    }

    private float getTagSpecificSpeed(string nTag)
    {
        for (int i=0; i<acceptedTags.Length; i++)
        {
            if (acceptedTags[i] == nTag)
            {
                return tagSpecificSpeed[i];
            }
        }
        return 1f;
    }

    private void resetDetectionProgress()
    {
        detectionProgress = (resetProgressOnLeave ? 0f : detectionProgress);
    }

}