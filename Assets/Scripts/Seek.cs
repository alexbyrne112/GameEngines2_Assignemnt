using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Seek : SteeringBehaviour
{
    public GameObject targetGO;
    public Vector3 target = Vector3.zero;

    public Seek(GameObject targetGO)
    {
        this.targetGO = targetGO;
    }

    public void OnDrawGizmos()
    {
        if (isActiveAndEnabled && Application.isPlaying)
        {
            Gizmos.color = Color.cyan;
            if (targetGO != null)
            {
                target = targetGO.transform.position;
            }
            Gizmos.DrawLine(transform.position, target);
        }
    }

    public override Vector3 Calculate()
    {
        return boid.SeekForce(target);
    }

    public void Update()
    {
        if (targetGO!= null)
        {
            target = targetGO.transform.position;
        }
    }
}