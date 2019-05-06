using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RussianHuntState : State
{
    Boid b;
    private Collider[] colliders;
    Russian russian;
    Persue p;
    GameObject enemy;

    public RussianHuntState(GameObject target)
    {
        this.enemy = target;
    }

    public override void Enter()
    {
        russian = owner.GetComponent<Russian>();
        b = owner.gameObject.GetComponent<Boid>();
        p = owner.gameObject.AddComponent<Persue>();
        p.targetGO = enemy;
        p.weight = 1f;
        b.behaviours.Add(p);
    }

    public override void Think()
    {
        Vector3 toTarget = (enemy.transform.position - owner.transform.position).normalized;

        //Dot Product for defend or attack state
        if (Vector3.Dot(toTarget, owner.transform.forward) > 0.995f)
        {
            Vector3 bulletFire = owner.transform.position + owner.transform.forward;
            GameObject bullet = Object.Instantiate(russian.bulletFromPrefab, bulletFire, owner.transform.rotation);
        }
    }

    public override void Exit()
    {
        b.behaviours.Remove(p);
        Object.Destroy(p);
    }
}
