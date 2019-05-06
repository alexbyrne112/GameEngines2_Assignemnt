using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : MonoBehaviour
{
    public GameObject missileTarget;
    public Rigidbody missileRB;

    public float rotate;
    public float velocity;

    private void FixedUpdate()
    {
        if(missileTarget != null)
        {
            missileRB.velocity = transform.forward * velocity;
            var missileRotation = Quaternion.LookRotation(missileTarget.transform.position - transform.position);
            missileRB.MoveRotation(Quaternion.RotateTowards(transform.rotation, missileRotation, rotate));
            Destroy(this.gameObject, 5);
        }
    }
}