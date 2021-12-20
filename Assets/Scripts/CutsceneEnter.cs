using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneEnter : MonoBehaviour
{
    public GameObject thePlayer;
    public GameObject mainCam;
    public GameObject cutsceneCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        cutsceneCamera.SetActive(true);
        thePlayer.SetActive(false);
        mainCam.SetActive(false);
    }

}
