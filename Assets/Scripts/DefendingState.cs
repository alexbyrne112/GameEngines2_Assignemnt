using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendingState : State
{
    MigMove mm1;
    MigMove mm2;
    SeperationScript SC;
    Boid b;
    GameObject ally;
    private Collider[] colliders;

    public override void Enter()
    {
        b = owner.GetComponent<Boid>();
        mm1 = owner.gameObject.AddComponent<MigMove>();
        mm1.weight = 2f;
        mm1.frequency = 0.05f;
        mm1.radius = 40;
        mm1.amplitude = 180;
        mm1.distance = 60;
        mm1.axis = MigMove.Axis.Horizontal;
        mm2 = owner.gameObject.AddComponent<MigMove>();
        mm2.weight = 2f;
        mm2.frequency = 0.05f;
        mm2.radius = 40;
        mm2.amplitude = 180;
        mm2.distance = 60;
        mm1.axis = MigMove.Axis.Vertical;
        b.behaviours.Add(mm1);
        b.behaviours.Add(mm2);

        //seperation
        ally = owner.GetComponent<American1>().ally;
        SC = owner.gameObject.AddComponent<SeperationScript>();
        SC.ally = ally;
        SC.weight = 1;
        b.behaviours.Add(SC);
    }
    public override void Think()
    {
        //Overlap Shpere for detecting other planes
        colliders = Physics.OverlapSphere(owner.transform.position, 5000);
        Transform nearest = null;
        int nearestRef = 0;
        float nearDist = 62500f;
        if (0 < colliders.Length)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (!colliders[i].CompareTag("American"))
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
            Vector3 toTarget = (targetGO.transform.position - owner.transform.position).normalized;

            //Dot Product for defend or attack state 
            if (Vector3.Dot(toTarget, owner.transform.forward) > 0)
            {
                if (targetGO.GetComponent<Russian>().health > 0)
                {
                    owner.GetComponent<StateMachine>().RevertToPreviousState();
                }
            }
        }
        else
        {
            owner.GetComponent<StateMachine>().RevertToPreviousState();
        }
    }
    public override void Exit()
    {
        Object.Destroy(mm1);
        Object.Destroy(mm2);
        Object.Destroy(SC);
        b.behaviours.Remove(mm1);
        b.behaviours.Remove(mm2);
        b.behaviours.Remove(SC);
    }
}
