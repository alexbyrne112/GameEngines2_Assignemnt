using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingState : State
{
    //Plane plane;
    private GameObject target;
    Persue p;
    American1 american1;

    public AttackingState(GameObject target)
    {
        //this.searchLayer = searchLayer;
        this.target = target;
    }

    public override void Enter()
    {
        //p = owner.GetComponent<Persue>();
        Boid b = owner.GetComponent<Boid>();
        p = owner.gameObject.AddComponent<Persue>();
        p.targetGO = target;
        b.behaviours.Add(p);
        american1 = owner.GetComponent<American1>();
        
    }
    public override void Think()
    {
        p.targetGO = target;
        Vector3 bulletFire = owner.transform.position + owner.transform.forward;
        GameObject bullet = Object.Instantiate(american1.bulletFromPrefab, bulletFire, owner.transform.rotation);
        
    }
    public override void Exit()
    {
        p.targetGO = null;
    }
}
