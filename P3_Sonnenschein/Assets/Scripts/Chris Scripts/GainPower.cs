using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainPower : MonoBehaviour {

    public PlayerAbilities pAbility;

    public GameObject picture;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            picture.SetActive(false);

            pAbility.addAbility(en_Abil.PA_HIDE);
        }
    }




}
