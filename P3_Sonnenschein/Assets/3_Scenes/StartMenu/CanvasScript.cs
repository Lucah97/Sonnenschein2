using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CanvasScript : MonoBehaviour {

    public Button[] buttons;
    public Button reset;
    public Image googleImage;
    public GameObject[] Questions;
    int activeQuestion = 0;
    bool buttonsActive = false;
    bool resetButton;
    public VideoPlayer Animatic;

    private bool videoFinished = false;
    

    // Use this for initialization
    void Start () {
        googleImage.gameObject.SetActive(false);
        reset.gameObject.SetActive(false);
        for (int i = 0; i <= Questions.Length-1; i++)
        {
            Questions[i].gameObject.SetActive(false);
        }
        
    }

    

	// Update is called once per frame
	void Update () {
        DisableButtons();
        EndReached(Animatic);
        Debug.Log(buttonsActive);

    }

    public void PressedRight()
    {
        buttonsActive = false;
        Questions[activeQuestion].gameObject.SetActive(true);
     
    }

    public void PressedWrong()
    {
        Questions[activeQuestion].gameObject.SetActive(false);
        googleImage.gameObject.SetActive(true);
        reset.gameObject.SetActive(true);
    }

    void DisableButtons()
    {
            for (int i = 0; i <= buttons.Length - 1; i++)
            {
                buttons[i].gameObject.SetActive(buttonsActive);
            }
    
    }

    public void nextQuestion()
    {
        Questions[activeQuestion].gameObject.SetActive(false);
        activeQuestion += 1;
        Questions[activeQuestion].gameObject.SetActive(true);
    }

    public void ResetButton()
    {
        googleImage.gameObject.SetActive(false);
        buttonsActive = true;
        reset.gameObject.SetActive(false);
    }

    void EndReached(VideoPlayer Animatic)
    {
        if(!Animatic.isPlaying && !videoFinished)
        {
            buttonsActive = true;
            videoFinished = true;
        }
    }

    void LoadGame()
    {
        SceneManager.LoadScene("Tutorial Hiding");
    }
}
