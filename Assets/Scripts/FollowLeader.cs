using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowLeader  : SteeringBehaviour
{
    public GameObject leader;
    Boid leaderBoid;

    Vector3 targetPos;
    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        leaderBoid = leader.GetComponent<Boid>();
    }

    public override Vector3 Calculate()
    {
        offset = leaderBoid.transform.position + new Vector3(-40,0,-40);

        float dist = Vector3.Distance(offset, transform.position);
        float time = dist / this.boid.maxSpeed;
        targetPos = offset + (leaderBoid.velocity * time);
        //Changed from Arrive force to SeekForce
        return this.boid.ArriveForce(targetPos);
    }

}