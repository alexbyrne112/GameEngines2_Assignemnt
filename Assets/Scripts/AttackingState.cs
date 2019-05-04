using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingState : State
{
    //Plane plane;
    private GameObject target;
    Persue p;
    American1 american1;
    Boid b;

    public AttackingState(GameObject target)
    {
        //this.searchLayer = searchLayer;
        this.target = target;
    }

    public override void Enter()
    {
        //add persue and target tp persue and add it to steering list
        b = owner.GetComponent<Boid>();
        p = owner.gameObject.AddComponent<Persue>();
        p.targetGO = target;
        p.weight = 2;
        b.behaviours.Add(p);
        american1 = owner.GetComponent<American1>();
        american1.attack = true;
        american1.missileCount = 1;
        american1.attackTarget = target;
        
     }
    public override void Think()
    {
        float enemyHealth = target.GetComponent<Russian>().health;
        Vector3 toTarget = (target.transform.position - owner.transform.position).normalized;

        //Dot Product for defend or attack state 
        if (Vector3.Dot(toTarget, owner.transform.forward) >0.995f)
        {
            Vector3 bulletFire = owner.transform.position + owner.transform.forward;
            GameObject bullet = Object.Instantiate(american1.bulletFromPrefab, bulletFire, owner.transform.rotation);
        }
        if(enemyHealth <= 0)
        {
            Object.Destroy(owner.GetComponent<Persue>());
            Object.Destroy(target.GetComponent<Boid>());
            b.behaviours.Remove(p);
            american1.attack = false;
            owner.GetComponent<StateMachine>().RevertToPreviousState();
        }
    }

    public override void Exit()
    {
        Object.Destroy(owner.GetComponent<Persue>());
        Object.Destroy(target.GetComponent<Boid>());
        b.behaviours.Remove(p);
        american1.attack = false;
    }
}
