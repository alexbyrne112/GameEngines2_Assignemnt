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
         cohesionVector.x += allyBoid.transform.position.x;
         cohesionVector.y += allyBoid.transform.position.y;
         cohesionVector.z += allyBoid.transform.position.z;

         //Divide by number of neighbours but in this case its 1 neighbour so no point
         //Also recompute for the direction of mass rather then the center of mass itself
         cohesionVector = new Vector3(cohesionVector.x - transform.position.x,
             cohesionVector.y - transform.position.y,
             cohesionVector.z - transform.position.z);
        Vector3 steer = cohesionVector.normalized * 450;

        return boid.ArriveForce(steer);
    }
}
