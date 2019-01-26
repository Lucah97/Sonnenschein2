using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainPower : MonoBehaviour {

    public PlayerAbilities pAbility;

    public GameObject picture;

    public en_Abil giveAbility;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            picture.SetActive(false);

            pAbility.addAbility(giveAbility);
        }
    }




}
