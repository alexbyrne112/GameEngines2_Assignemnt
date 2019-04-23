using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendingState : State
{
    MigMove mm1;
    MigMove mm2;
    Boid b;
    private Collider[] colliders;

    public override void Enter()
    {
        b = owner.GetComponent<Boid>();
        mm1 = owner.gameObject.AddComponent<MigMove>();
        mm1.weight = 1.5f;
        mm1.frequency = 0.05f;
        mm1.radius = 40;
        mm1.amplitude = 180;
        mm1.distance = 60;
        mm1.axis = MigMove.Axis.Horizontal;
        mm2 = owner.gameObject.AddComponent<MigMove>();
        mm2.weight = 1.5f;
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
        colliders = Physics.OverlapSphere(owner.transform.position, 500);
        if(colliders.Length == 0)
        {
            owner.GetComponent<StateMachine>().RevertToPreviousState();
        }
    }
    public override void Exit()
    {
        b.behaviours.Remove(mm1);
        b.behaviours.Remove(mm2);
    }
}
