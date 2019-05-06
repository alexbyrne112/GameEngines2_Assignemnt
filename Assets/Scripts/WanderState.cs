using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : State
{
    MigMove mm1;
    MigMove mm2;
    Boid b;
    Collider[] colliders;
    Russian russian;

    public override void Enter()
    {
        russian = owner.gameObject.GetComponent<Russian>();
        b = owner.GetComponent<Boid>();
        mm1 = owner.gameObject.AddComponent<MigMove>();
        mm1.weight = 0.5f;
        mm1.frequency = 0.05f;
        mm1.radius = 40;
        mm1.amplitude = 180;
        mm1.distance = 60;
        mm1.axis = MigMove.Axis.Horizontal;
        mm2 = owner.gameObject.AddComponent<MigMove>();
        mm2.weight = 0.5f;
        mm2.frequency = 0.05f;
        mm2.radius = 40;
        mm2.amplitude = 180;
        mm2.distance = 60;
        mm1.axis = MigMove.Axis.Vertical;
        b.behaviours.Add(mm1);
        b.behaviours.Add(mm2);
    }

    public override void Think()
    {

        //Overlap Shpere for detecting other planes
        colliders = Physics.OverlapSphere(owner.transform.position, 500);
        Transform nearest = null;
        int nearestRef = 0;
        string collSide;
        float nearDist = 62500f;
        if (0 < colliders.Length)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                collSide = colliders[i].gameObject.tag;
                if (collSide != russian.side && collSide != "Wall" && collSide != "Bullet")
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
            GameObject targetGO = GameObject.Find("F-14 Tomcat");
            //Dot Product for defend or attack state check if infront
            Vector3 toTarget = (targetGO.transform.position - owner.transform.position).normalized;

            if (Vector3.Dot(toTarget, owner.transform.forward) > 0.3)
            {
                owner.GetComponent<StateMachine>().ChangeState(new RussianHuntState(targetGO));
            }
        }
    }

    public override void Exit()
    {
        b.behaviours.Remove(mm1);
        b.behaviours.Remove(mm2);
        Object.Destroy(mm1);
        Object.Destroy(mm2);
    }
}
