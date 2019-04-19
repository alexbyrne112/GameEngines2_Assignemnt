using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchState : State
{
    private Collider[] colliders;
    American1 american1;

    public override void Enter()
    {
        american1 = owner.GetComponent<American1>();
        MigMove mm1 = owner.gameObject.AddComponent<MigMove>();
        mm1.weight = 1;
        mm1.frequency = 0.05f;
        mm1.radius = 40;
        mm1.amplitude = 180;
        mm1.distance = 60;
        mm1.axis = MigMove.Axis.Vertical;

        MigMove mm2 = owner.gameObject.AddComponent<MigMove>();
        mm2.weight = 1;
        mm2.frequency = 0.05f;
        mm2.radius = 40;
        mm2.amplitude = 180;
        mm2.distance = 60;
        mm2.axis = MigMove.Axis.Horizontal;

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
            //Boid targetBoid = targetGO.GetComponent<Boid>();
            owner.GetComponent<StateMachine>().ChangeState(new AttackingState(targetGO));
        }


    }
    public override void Exit()
    {

    }
}

     