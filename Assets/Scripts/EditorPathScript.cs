using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorPathScript : MonoBehaviour
{
    public Color rayColor = Color.white;
    public List<Transform> pathOjs = new List<Transform>();
    Transform[] theArray;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = rayColor;
        theArray = GetComponentsInChildren<Transform>();
        pathOjs.Clear();

        foreach (Transform pathOj in theArray)
        {
            if (pathOj != this.transform)
            {
                pathOjs.Add(pathOj);
            }
        }

        for (int i = 0; i < pathOjs.Count; i++)
        {
            Vector3 position = pathOjs[i].position;
            if (i > 0)
            {
                Vector3 previous = pathOjs[i - 1].position;
                Gizmos.DrawLine(previous, position);
                Gizmos.DrawWireSphere(position, 0.3f);
            }
        }
    }
}
