using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathTrigger : MonoBehaviour
{
    public bool follow;
    //public MoveOnPath Path;

    
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
        if (other.gameObject.CompareTag("PathTrigg"))
        {
            //Path = other.gameObject.GetComponent(MoveOnPath);
            follow = true;
            Debug.Log("Bool works? " + follow);
        }
    }
}
