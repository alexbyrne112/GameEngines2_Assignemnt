using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvadeState : State
{
    SeperationScript SC;
    Boid b;
    GameObject enemy;
    American2 american2;

    public EvadeState(GameObject target)
    {
        this.enemy = target;
    }

    public override void Enter()
    {
        american2 = owner.gameObject.GetComponent<American2>();
        b = owner.gameObject.GetComponent<Boid>();
        SC = owner.gameObject.AddComponent<SeperationScript>();
        SC.ally = GameObject.Find("F-14 Tomcat");
        SC.weight = 2;
        b.behaviours.Add(SC);
    }

    public override void Think()
    {
        if (enemy != null)
        {
           if(Vector3.Distance(enemy.transform.position, owner.transform.position) > 200)
           {
               owner.GetComponent<StateMachine>().ChangeState(new HuntState(enemy));
           }
        }
    }

    public override void Exit()
    {
        b.behaviours.Remove(SC);
        Object.Destroy(SC);
    }
}
