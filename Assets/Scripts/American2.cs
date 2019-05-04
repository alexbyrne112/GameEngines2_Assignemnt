using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class American2 : MonoBehaviour
{
    public string side;
    public Collider[] colliders;

    // Start is called before the first frame update
    void Start()
    {
        //side = gameObject.tag;
    }

    // Update is called once per frame
    void Update()
    {
        colliders = Physics.OverlapSphere(transform.position, 5000);
        Transform nearest = null;
        int nearestRef = 0;
        float nearDist = 62500f;
        if (0 < colliders.Length)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (!colliders[i].CompareTag(side))
                {
                    float thisDist = (transform.position - colliders[i].transform.position).sqrMagnitude;
                    if (thisDist < nearDist)
                    {
                        nearDist = thisDist;
                        nearest = colliders[i].transform;
                        nearestRef = i;
                    }
                }
            }
        }
        if (nearest != null)
        {
            GameObject targetGO = colliders[nearestRef].gameObject;
            //Dot Product for defend or attack state 
            if (Vector3.Dot(transform.forward, targetGO.transform.position) < 0)//Behind
            { 
                GetComponent<FollowLeader>().weight = 0.1f;
                GetComponent<SeperationScript>().weight = 2f;
                GetComponent<CohesionScript>().weight = 0.1f;
            }
            else
            {
                GetComponent<FollowLeader>().weight = 1.5f;
                GetComponent<CohesionScript>().weight = 0.5f;
                GetComponent<SeperationScript>().weight = 0f;
            }
        }
        else
        {
            GetComponent<FollowLeader>().weight = 1.5f;
            GetComponent<CohesionScript>().weight = 0.5f;
            GetComponent<SeperationScript>().weight = 0f;
        }

    }
}
