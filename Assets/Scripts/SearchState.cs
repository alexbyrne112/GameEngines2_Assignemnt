using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchState : State
{
    private Collider[] colliders;
    American1 american1;
    Path patrolPath;
    //Boid b;
    PathFollow pf;
    /*
    public SearchState(GameObject patrolPathGO)
    {
        this.patrolPath = patrolPathGO;
    }*/

    public override void Enter()
    {
        if (owner.gameObject.GetComponent<PathFollow>() != null)
        {
            Debug.Log("component");
            pf.enabled = true;
        }
        else
        {
            patrolPath = GameObject.FindGameObjectWithTag("AmericanPath").GetComponent<Path>();
            Debug.Log("No component");
            pf = owner.gameObject.AddComponent<PathFollow>();
            pf.path = patrolPath;
        }

        american1 = owner.GetComponent<American1>();
        
    }
    public override void Think()
    {
        //Overlap Shpere for detecting other planes
        colliders = Physics.OverlapSphere(owner.transform.position, 500);
        Transform nearest = null;
        int nearestRef = 0;
        float nearDist = 9999f;
        if(0 < colliders.Length)
        {
            for(int i = 0; i < colliders.Length; i++)
            {
                if(!colliders[i].CompareTag(american1.side))
                {
                    float thisDist = (owner.transform.position - colliders[i].transform.position).sqrMagnitude;
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
            if (Vector3.Dot(owner.transform.forward, targetGO.transform.position) > 0)
            {
                owner.GetComponent<StateMachine>().ChangeState(new AttackingState(targetGO));
            }
            else
            {
                //owner.GetComponent<StateMachine>().ChangeState(new DefendingState());
            }
            //GameObject targetGO = colliders[nearestRef].gameObject;
            //Boid targetBoid = targetGO.GetComponent<Boid>();
            //owner.GetComponent<StateMachine>().ChangeState(new AttackingState(targetGO));
        }


    }
    public override void Exit()
    {
        pf.enabled = false;
    }
}

     