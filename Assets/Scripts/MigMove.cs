using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MigMove : SteeringBehaviour
{
    public float frequency = 0.3f;
    public float radius = 10.0f;

    private float theta = 0;
    private float amplitude = 180;
    private float distance = 160;

    public enum Axis { Horizontal, Vertical };

    public Axis axis = Axis.Horizontal;

    Vector3 target;
    Vector3 worldTarget;
    
    public override Vector3 Calculate()
    {
        float n = (Mathf.PerlinNoise(theta, 1) * 2) - 1;
        float angle = n * amplitude * Mathf.Deg2Rad;
        Vector3 rotate = transform.rotation.eulerAngles;
        rotate.x = 0;

        if (axis == Axis.Horizontal)
        {
            target.x = Mathf.Sin(angle);
            target.z = Mathf.Cos(angle);
            target.y = 0;
            rotate.z = 0;
        }
        else
        {
            target.y = Mathf.Sin(angle);
            target.z = Mathf.Cos(angle);
        }
        target *= radius;
        Vector3 localTarget = target + (Vector3.forward * distance);
        worldTarget = transform.position + Quaternion.Euler(rot) * localTarget;

        theta += frequency * Time.deltaTime * Mathf.PI * 2.0f;

        return boid.SeekForce(worldTarget);


    }
}