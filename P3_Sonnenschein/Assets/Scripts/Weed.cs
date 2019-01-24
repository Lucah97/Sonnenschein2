using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class Weed : MonoBehaviour {

    public PostProcessingProfile PPP;

    public bool weedls;

    public float maximum;
    public float minimum;

    public float HUE;

	// Use this for initialization
	void Start () {
        weedls = false;

	}
	
	// Update is called once per frame
	void Update () {

        if (weedls)
        {
            var curHue = PPP.colorGrading.settings;

            curHue.basic.hueShift = Mathf.Lerp(minimum, maximum, HUE);

            HUE += 0.05f * Time.deltaTime;

            PPP.colorGrading.settings = curHue;

            Debug.Log("HUE" + curHue.basic.hueShift);

            if (HUE >= 1f)
            {
                HUE = 0f;
            }
        }
		


	}


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            weed();
        }
    }

    private void weed()
    {
        weedls = true;

    }

}