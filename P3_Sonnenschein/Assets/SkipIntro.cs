using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine;

public class SkipIntro : MonoBehaviour {

    public float skipTime = 3f;

    private float elapsedTime = 0f;
    private bool hasActivated = false;

    void Update() {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > skipTime)
        {
            if (Input.GetButtonDown("B") && hasActivated)
            {
                VideoPlayer[] vids = FindObjectsOfType<VideoPlayer>();
                foreach (VideoPlayer v in vids)
                {
                    v.enabled = false;
                }
                Destroy(this.gameObject);
            }

            if (hasInput() && !hasActivated)
            {
                GetComponent<Image>().enabled = true;
                hasActivated = true;
            }
        }
    }

    private bool hasInput()
    {
        if (Input.GetButtonDown("Jump") || Input.GetButtonDown("B") || Input.GetButtonDown("X") || Input.GetButtonDown("Y"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
