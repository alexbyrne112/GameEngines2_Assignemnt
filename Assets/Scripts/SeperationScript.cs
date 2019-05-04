using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeperationScript : SteeringBehaviour
{
    public GameObject ally;
    Boid allyBoid;
    Vector3 seperationVector;

    // Start is called before the first frame update
    void Start()
    {
        allyBoid = ally.GetComponent<Boid>();
    }

    public override Vector3 Calculate()
    {
        seperationVector.x = allyBoid.transform.position.x - boid.transform.position.x;
        seperationVector.y = allyBoid.transform.position.y - boid.transform.position.y;
        seperationVector.z = allyBoid.transform.position.z - boid.transform.position.z;

        //Must be negated in order for the planes to steer away from eachother properly
        seperationVector.x *= -1;
        seperationVector.y *= -1;
        seperationVector.z *= -1;

        //seperationVector.Normalize();
        return boid.ArriveForce(seperationVector);
    }
}
