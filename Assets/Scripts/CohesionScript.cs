using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CohesionScript : SteeringBehaviour
{
    public GameObject ally;
    Boid allyBoid;
    Vector3 cohesionVector = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        allyBoid = ally.GetComponent<Boid>();
    }

    // Update is called once per frame
    public override Vector3 Calculate()
    {
        Vector3 direction = ally.transform.position - transform.position;
        Vector3 desiredPos = direction.normalized * boid.maxSpeed;
        cohesionVector = desiredPos - boid.velocity;
        return cohesionVector;
    }
}
