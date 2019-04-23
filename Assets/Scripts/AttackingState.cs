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
    Boid missileBoid;
    public MonoBehaviour monoB;

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
        
        //monoB.StartCoroutine(FireMissile());

    }
    public override void Think()
    {
        float enemyHealth = target.GetComponent<Russian>().health;
        Vector3 bulletFire = owner.transform.position + owner.transform.forward;
        GameObject bullet = Object.Instantiate(american1.bulletFromPrefab, bulletFire, owner.transform.rotation);
        if(enemyHealth == 0)
        {
            //owner.GetComponent<StateMachine>().ChangeState(new SearchState());
            owner.GetComponent<StateMachine>().RevertToPreviousState();
        }
        Vector3 MissileFire = owner.transform.position - owner.transform.up * 2;
        GameObject Missile = Object.Instantiate(american1.missileFromPrefab, MissileFire, owner.transform.rotation);
        missileBoid = Missile.GetComponent<Boid>();
        Seek missileSeek = Missile.GetComponent<Seek>();
        missileSeek.targetGO = target;
        missileBoid.behaviours.Add(missileSeek);
    }

    public override void Exit()
    {
        b.behaviours.Remove(p);
    }

    private IEnumerator FireMissile()
    {
        yield return new WaitForSeconds(10f);
        Vector3 MissileFire = owner.transform.position - owner.transform.up * 2;
        GameObject Missile = Object.Instantiate(american1.missileFromPrefab, MissileFire, owner.transform.rotation);
        Missile.GetComponent<Seek>().targetGO = target;
    }
}
