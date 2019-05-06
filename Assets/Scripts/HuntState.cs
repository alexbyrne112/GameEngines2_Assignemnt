using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntState : State
{
    Boid b;
    private Collider[] colliders;
    American2 american2;
    Persue p;
    GameObject enemy;

    public HuntState(GameObject target)
    {
        this.enemy = target;
    }

    public override void Enter()
    {
        american2 = owner.GetComponent<American2>();
        b = owner.gameObject.GetComponent<Boid>();
        p = owner.gameObject.AddComponent<Persue>();
        p.targetGO = enemy;
        p.weight = 1f;
        b.behaviours.Add(p);
    }
    
    public override void Think()
    {
        float enemyHealth = enemy.GetComponent<Russian>().health;
        Vector3 toTarget = (enemy.transform.position - owner.transform.position).normalized;

        //Dot Product for defend or attack state
        if (Vector3.Dot(toTarget, owner.transform.forward) > 0.995f)
        {
            Vector3 bulletFire = owner.transform.position + owner.transform.forward;
            GameObject bullet = Object.Instantiate(american2.bulletFromPrefab, bulletFire, owner.transform.rotation);
        }
        if (enemyHealth <= 0)
        {
            owner.GetComponent<ObstacleAvoidance>().enabled = false;
            b.behaviours.Remove(p);
            Object.Destroy(p);
            Object.Destroy(owner.GetComponent<Persue>());
            Object.Destroy(enemy.GetComponent<Boid>());
            owner.GetComponent<StateMachine>().ChangeState(new SupportState());
        }
    }

    public override void Exit()
    {
        b.behaviours.Remove(p);
        Object.Destroy(p);
    }
}
