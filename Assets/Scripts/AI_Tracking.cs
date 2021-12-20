using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Tracking : MonoBehaviour
{
    public bool seePlayer = false;
    public GameObject player = null;
    public float turnspeed = 1f;
    public float maxturn = 45;
    public float inSight = 3.5f;
    public float maxSight = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!seePlayer)
        {
            transform.Rotate(0f, turnspeed, 0f);
            if ((transform.rotation.eulerAngles.y > maxturn && transform.rotation.eulerAngles.y < 180) ||
                (transform.rotation.eulerAngles.y < 360 - maxturn && transform.rotation.eulerAngles.y < 180))
            {
                turnspeed *= -1;
            }
        }
        else
        {
            transform.LookAt(player.transform);
        }
        Vector3 sight = player.transform.position - transform.position;
        float dot = Vector3.Dot(sight, transform.right);

        if (dot < inSight && dot > -inSight)
        {
            RaycastHit Hit;
            if (Physics.Raycast(transform.position, (player.transform.position - transform.position).normalized, out Hit, maxSight))
            {
                if (Hit.collider.name == "Player")
                {
                    seePlayer = true;
                }
                else
                {
                    seePlayer = false;
                }
            }
            else
            {
                seePlayer = false;
            }
        }

    }
}
