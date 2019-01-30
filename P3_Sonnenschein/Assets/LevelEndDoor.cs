using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;

public class LevelEndDoor : MonoBehaviour, InterfaceLetherTrigger
{

    public VideoClip end;

    private bool isOpen = false;

    [Header("Custom Door")]

    public bool LoadCustomScene;

    public bool ReturnDoor;

    public bool IsTrigger;

    public string customscene;

    public GameObject SceneSaver;

    public static Vector3 LastPos;

    public static bool Returning;

    private GameObject curMessage = null;

    private void Start()
    {
        if (Returning)
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = LastPos;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAbilities>().CanonStrenght = 14;
            Returning = false;
        }
        if (ReturnDoor)
        {
            customscene = GameObject.Find("SceneSaver").GetComponent<OldScene>().LastScene;
            LoadCustomScene = true;
        }
    }

    public void OnSwitchTrigger()
    {
        transform.GetChild(1).rotation = Quaternion.Euler(0, 150, 0);
        isOpen = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (LoadCustomScene || Returning)
            {
                if (!other.GetComponent<PlayerMovement>().getCanonMode())
                {
                    if ((Input.GetAxis("RT") > 0) || IsTrigger)
                    {
                        if (!ReturnDoor)
                        {
                            LastPos = GameObject.FindGameObjectWithTag("Player").transform.position;
                            string Scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

                            SceneSaver.GetComponent<OldScene>().LastScene = Scene;
                        }

                        PostProcessingProfile PPP = Camera.main.GetComponent<PostProcessingBehaviour>().profile;

                        if (GameObject.FindGameObjectWithTag("Weed") != null)
                        {
                            foreach (GameObject Weeders in GameObject.FindGameObjectsWithTag("Weed")) { Weeders.GetComponent<Weed>().weedls = false; }
                        }

                        //Delete UI Elements
                        UI_Spawner.instance.destroyAllElements();

                        var curHue = PPP.colorGrading.settings;

                        curHue.basic.hueShift = Mathf.Lerp(-180, 180, 0.5f);
                        curHue.basic.saturation = 1f;
                        PPP.colorGrading.settings = curHue;

                        Camera.main.gameObject.transform.GetChild(0).GetComponent<VidRestart>().CustomScene = customscene;
                        Camera.main.gameObject.transform.GetChild(0).GetComponent<VidRestart>().customload = true;

                        Camera.main.gameObject.transform.GetChild(0).GetComponent<Renderer>().enabled = true;

                        if (ReturnDoor)
                        {
                            Returning = true;
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (LoadCustomScene && (other.tag == "Player"))
        {
            if (!other.GetComponent<PlayerMovement>().getCanonMode())
            {
                curMessage = UI_Spawner.instance.spawn(UI_Types.ButtonIndicator, Ctrl_Buttons.RT, "Enter", GameObject.FindGameObjectWithTag("Player"), new Vector3(0, 2, 0));
            }
        }
        if ((isOpen) && (other.tag == "Player"))
        {
            //Enable video
            Camera.main.gameObject.transform.GetChild(0).GetComponent<Renderer>().enabled = true;
            Camera.main.gameObject.transform.GetChild(0).GetComponent<VideoPlayer>().clip = end;
            Camera.main.gameObject.transform.GetChild(0).GetComponent<VideoPlayer>().Play();

            //Disable Controls
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (LoadCustomScene && (other.tag == "Player"))
        {
            if (curMessage) { Destroy(curMessage.gameObject); }
            curMessage = null;
        }
    }
}
