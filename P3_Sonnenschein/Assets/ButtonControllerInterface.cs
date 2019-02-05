using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ButtonControllerInterface : MonoBehaviour {

    public string axis;

    private int selectionAmount;
    private Transform highLight;

    private bool axisReset = false;
    private int curSelection = 0;

	void Start () {
        selectionAmount = transform.GetComponentsInChildren<Button>().Length - 1;
        highLight = transform.GetChild(transform.childCount - 1);
        highLight.gameObject.SetActive(false);
	}

	void LateUpdate () {
        if (axis != "NONE")
        {
            if (transform.GetChild(0).gameObject.activeSelf)
            {
                highLight.gameObject.SetActive(true);
                handleAxisInput();
                handleButtonPress();
            }
            else
            {
                highLight.gameObject.SetActive(false);
            }
        }
        else
        {
            if (Input.GetButtonDown("Jump"))
            {
                GetComponent<Button>().onClick.Invoke();
            }
        }
	}

    private void handleAxisInput()
    {
        if (Input.GetAxis(axis) == 0) { axisReset = true; }

        if ((Input.GetAxis(axis) != 0) && (axisReset))
        {
            float input = Input.GetAxis(axis);
            input = ((input < 0) ? -1 : 1);
            curSelection += (int)input;
            curSelection = Mathf.Clamp(curSelection, 0, selectionAmount);

            axisReset = false;
        }

        highLight.position = transform.GetChild(curSelection).position;
    }

    private void handleButtonPress()
    {
        if (Input.GetButtonDown("Jump"))
        {
            transform.GetChild(curSelection).GetComponent<Button>().onClick.Invoke();
        }
    }
}
