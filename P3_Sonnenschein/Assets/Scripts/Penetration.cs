using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penetration : MonoBehaviour {

    public float shootStrength = 2;
    public float StraveStrength = 1;

    public Transform PenisTip;

    public bool FirstColl = true;

    bool freezePlayer = false;
    float curTime = 0;

    float VFXTime = 0;
    bool VFX=false;
    float SideStrave;
    Vector3 PenisMid;


    public GameObject VFXTrail;

    GameObject Player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (freezePlayer)
        {
            if(curTime < 0.4f)
            {
                curTime += Time.deltaTime;

                if (PenisTip.position.y > Player.transform.position.y)
                {
                    Player.transform.position = new Vector3(PenisTip.position.x, PenisTip.position.y, Player.transform.position.z);
                }
            }
            else
            {
                freezePlayer = false;
                curTime = 0;
                shootOff();
            }
        }

        //VFX
        if (VFX)
        {
            if (VFXTime < 0.3f)
            {
                VFXTime += Time.deltaTime;
            }
            else if(VFXTime > 0.3f && VFXTime < 1)
            {
                VFXTime += Time.deltaTime;
                var VFXdespawner = VFXTrail.GetComponent<ParticleSystem>().main;

                VFXdespawner.startLifetime = 0;
                //VFXTrail.GetComponent<ParticleSystem>().main.startLifetime = VFXdespawner.startLifetime;
            }
            else
            {
                VFX = false;
                VFXTime = 0;
                VFXTrail.SetActive(false);
                var VFXdespawner = VFXTrail.GetComponent<ParticleSystem>().main;
                VFXdespawner.startLifetime = 1.5f;
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && FirstColl)
        {
            other.GetComponent<PlayerMovement>().allowInput = false;
            
            other.GetComponent<PlayerMovement>().freezeVelocity();

            other.GetComponent<PlayerMovement>().setApplyGravity(false);

            Player = other.gameObject;

            PenisMid = new Vector3(gameObject.transform.parent.position.x, Player.transform.position.y, Player.transform.position.z);
            SideStrave = Vector3.Distance(PenisMid, Player.transform.position) * StraveStrength;

            freezePlayer = true;
            curTime = 0;
            FirstColl = false;
        }
    }

    void shootOff()
    {
        //resetPlayerValues
        Player.GetComponent<PlayerMovement>().allowInput = true;
        Player.GetComponent<PlayerMovement>().freezeVelocity();
        Player.GetComponent<PlayerMovement>().setApplyGravity(true);

        //Where to shoot
        

        
        

        Vector3 ShootDir;

        if (PenisMid.x > Player.transform.position.x)
        {
            ShootDir = new Vector3(Player.transform.position.x - 5, Player.transform.position.y + 8, Player.transform.position.z);
        }
        else
        {
            ShootDir = new Vector3(Player.transform.position.x + 5, Player.transform.position.y + 8, Player.transform.position.z);
        }

        //Shoot
        Player.GetComponent<Rigidbody>().AddForce(ShootDir * shootStrength, ForceMode.Impulse);
        VFX = true;
        VFXTrail.SetActive(true);
    }
}
