using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelParticleSystem : MonoBehaviour {

    private ParticleSystem ps;

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void LateUpdate()
    {
        if (ps)
        {
            if (!ps.IsAlive()) { Destroy(this.gameObject); }
        }
    }

}
