using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class Weed : MonoBehaviour {

    public PostProcessingProfile PPP;

    public bool weedls;

    public float WeedSpeed = 0.05f;

    public float maximum;
    public float minimum;

    public float HUE;

    public float WeedTime = 2f;

    private float curTime;

	// Use this for initialization
	void Start () {
        weedls = false;

        var curHue = PPP.colorGrading.settings;
        curHue.basic.hueShift = 0f;
        PPP.colorGrading.settings = curHue;
    }
	
	// Update is called once per frame
	void Update () {

        if (weedls)
        {
            var curHue = PPP.colorGrading.settings;

            curHue.basic.hueShift = Mathf.Lerp(minimum, maximum, HUE);

            curHue.basic.saturation = 2f;

            HUE += WeedSpeed * Time.deltaTime;

            PPP.colorGrading.settings = curHue;

            //Debug.Log("HUE" + curHue.basic.hueShift);

            if (HUE >= 1f)
            {
                HUE = 0f;
            }

            curTime += Time.deltaTime;

            if (curTime > WeedTime) { weedls = false; curHue.basic.hueShift = Mathf.Lerp(minimum, maximum, 0.5f); curHue.basic.saturation = 1f; PPP.colorGrading.settings = curHue; }
        }
		


	}


    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            weed();
        }
    }

    private void weed()
    {
        curTime = 0;
        weedls = true;
        transform.GetChild(0).gameObject.SetActive(false);
        gameObject.GetComponent<Collider>().enabled = false;

        //Spawn Cloud effect
        FX_Spawner.instance.spawnFX(en_EffectType.SmokeCloud,
                                    transform.position,
                                    Quaternion.Euler(new Vector3(-90, 0, 0)),
                                    2f);
    }

}