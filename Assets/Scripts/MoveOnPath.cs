using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnPath : MonoBehaviour
{
    public EditorPathScript followPath;
    public PathTrigger trig;

    public int CurrentWayPointID = 0;
    public float speed;
    private float reachDistance = 1.0f;
    public float rotationSpeed = 5.0f;
    public string pathName;

    Vector3 lastPos;
    Vector3 currentPos;
    
    // Start is called before the first frame update
    void Start()
    {
        lastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        float distance = Vector3.Distance(followPath.pathOjs[CurrentWayPointID].position, transform.position);
        transform.position = Vector3.MoveTowards(transform.position, followPath.pathOjs[CurrentWayPointID].position, Time.deltaTime * speed);

        
        if (distance <= reachDistance)
        {
            CurrentWayPointID++;
                
        }


        
    }
}
