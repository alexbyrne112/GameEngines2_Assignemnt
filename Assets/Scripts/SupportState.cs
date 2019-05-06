using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportState : State
{
    FollowLeader FL;
    CohesionScript CS;
    Boid b;
    private Collider[] colliders;
    public GameObject targetGO;
    American2 american2;


    public override void Enter()
    {
        american2 = owner.GetComponent<American2>();
        b = owner.gameObject.GetComponent<Boid>();
        FL = owner.gameObject.AddComponent<FollowLeader>();
        CS = owner.gameObject.AddComponent<CohesionScript>();
        FL.leader = GameObject.Find("F-14 Tomcat");
        CS.ally = GameObject.Find("F-14 Tomcat");
        FL.weight = 1.5f;
        CS.weight = 0.5f;
        b.behaviours.Add(FL);
        b.behaviours.Add(CS);
    }

    public override void Think()
    {
        colliders = Physics.OverlapSphere(owner.transform.position, 500);
        Transform nearest = null;
        int nearestRef = 0;
        float nearDist = 62500f;
        if (0 < colliders.Length)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (!colliders[i].CompareTag(american2.side) && colliders[i].tag != "Wall" && colliders[i].tag != "Bullet")
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
            targetGO = colliders[nearestRef].gameObject;
            Vector3 toTarget = (targetGO.transform.position - owner.transform.position).normalized;
            //Dot Product for defend or attack state 
            if (Vector3.Dot(toTarget, owner.transform.forward) < 0)//Behind
            {
                if (american2.EnemyHealthLife > 0)
                {
                    owner.GetComponent<StateMachine>().ChangeState(new EvadeState(targetGO));
                }
            }
        }
    }

    public override void Exit()
    {
        b.behaviours.Remove(FL);
        b.behaviours.Remove(CS);
        Object.Destroy(FL);
        Object.Destroy(CS);
    }
}
