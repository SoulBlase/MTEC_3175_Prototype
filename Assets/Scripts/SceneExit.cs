using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class SceneExit : MonoBehaviour
{
    public string loadScene;
    public string exitScene;

    //private CinemachineFreeLook vCam;
    



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerPrefs.SetString("LastExitName", exitScene);
        SceneManager.LoadScene(loadScene);

        if (other.gameObject.CompareTag("Player"))
        {
            PlayerPrefs.SetString("LastExitScene", exitScene);
            SceneManager.LoadScene(loadScene);
            Debug.Log("Teleport works");
        }
    }
    /*
    private void TeleportCamera()
    {
        vCam.Follow = null;
        vCam.LookAt = null;

        StartCoroutine(UpdateCameraFrameLater());
    }

    private IEnumerator UpdateCameraFrameLater()
    {
        yield return null;

        vCam.Follow = Targettofollow;
        vCam.LookAt = *YourLookAtTarget;
    }*/


}
