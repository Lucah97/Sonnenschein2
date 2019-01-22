using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionScript : MonoBehaviour {

    public GameObject canvas;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PressedWrong()
    {
            canvas.GetComponent<CanvasScript>().PressedWrong();
    }

    public void PressedRight()
    {
        if (gameObject.name.Equals("Question01") || gameObject.name.Equals("Question02"))
        {
            canvas.GetComponent<CanvasScript>().nextQuestion();
        }
    }

}
