using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Effect_Spawner : MonoBehaviour {

    public static Effect_Spawner instance;

    public GameObject[] effectObjects;

    private void Awake()
    {
        //Singleton Instance
        if ((instance != this) && (instance != null))
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public GameObject spawnFX(en_EffectType fxType, Vector3 fxPos, Quaternion fxRot, float fxScaleMult)
    {
        GameObject nFX = Instantiate(effectObjects[(int)fxType]);
        nFX.transform.position = fxPos;
        nFX.transform.rotation = fxRot;
        nFX.transform.localScale = (nFX.transform.localScale * fxScaleMult);

        return nFX;
    }
}

public enum en_EffectType
{
    SmokeCloud,
    FireWorks,
    OtherStuff
};
