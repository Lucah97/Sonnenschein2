using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureFrameBehaviour : MonoBehaviour {

    public bool spamDirection = true;
    public float spamSpeed = 1;

    public float addTiling;
    public float addOffset;

    public float curSpeed;
    public float endSpeed;
    private float normCurSpeed;
    private float normEndSpeed;

    public float speedDecrease;

    public float minTiling;

    private float curTiling;
    private float curOffset;

    private float origTiling;
    private float origOffset;

    private bool hasSpedUp = false;

    private PlayerMovement pm;

    private Material holeMat;

	void Start () {
        holeMat = transform.GetChild(0).GetComponent<Renderer>().material;
        curTiling = holeMat.GetTextureScale("_MainTex").x;
        curOffset = holeMat.GetTextureOffset("_MainTex").x;

        origTiling = holeMat.GetTextureScale("_MainTex").x;
        origOffset = holeMat.GetTextureOffset("_MainTex").x;

        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        normCurSpeed = curSpeed;
        normEndSpeed = endSpeed;

        //Disable Collider
        pm.GetComponent<Collider>().enabled = false;
        pm.setApplyGravity(false);
    }
	
	void Update () {
        //Adjust texture tiling and offset
        curTiling += (addTiling * curSpeed * Time.deltaTime);
        curOffset += (addOffset * curSpeed * Time.deltaTime);

        holeMat.SetTextureScale("_MainTex", new Vector2(curTiling, curTiling));
        holeMat.SetTextureOffset("_MainTex", new Vector2(curOffset, curOffset));

        //Check if finished
        if (curTiling < minTiling)
        {
            pm.returnToLastZdepth();
            pm.setAllowIinput(true);

            //Enable Collider
            pm.GetComponent<Collider>().enabled = true;
            pm.setApplyGravity(true);

            //Spawn Cloud effect
            FX_Spawner.instance.spawnFX(en_EffectType.SmokeCloud,
                                        transform.position + (new Vector3(0, 0, -2)),
                                        Quaternion.Euler(new Vector3(-90, 0, 0)),
                                        0.62f);

            Destroy(this.gameObject);
        }

        //Adjust speed by jumping
        if (((Input.GetButtonDown("Jump")) || (Input.GetButtonDown("B"))) && (!hasSpedUp))
        {
            curTiling -= (addTiling * curSpeed) * spamSpeed;
            curOffset -= (addOffset * curSpeed) * spamSpeed;

            if (curTiling > 1)
            {
                curTiling = origTiling;
                curOffset = origOffset;
            }

            hasSpedUp = true;
        }
        if ((Input.GetButtonDown("Jump")) || (Input.GetButtonDown("B")))
        {
            hasSpedUp = false;

            //Calculate speed
            curSpeed = Mathf.Lerp(curSpeed, endSpeed, speedDecrease * Time.deltaTime);

            //Adjust endSpeed
            endSpeed = Mathf.Lerp(endSpeed, (normEndSpeed), (Time.deltaTime * speedDecrease));
        }

    }
}
