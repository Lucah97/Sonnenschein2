using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OldScene : MonoBehaviour {

    public string LastScene;

    public GameObject Door;

    private void Start()
    { 
        DontDestroyOnLoad(this.gameObject);
    }
}
