using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEntrance : MonoBehaviour
{
    public string lastExitScene;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("LastExitScene") == lastExitScene)
        {
            PlayerScript.instance.transform.position = transform.position;
            PlayerScript.instance.transform.position = transform.eulerAngles;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
