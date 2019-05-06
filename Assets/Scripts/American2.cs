using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class American2 : MonoBehaviour
{
    public string side;
    private Collider[] colliders;
    public GameObject targetGO;
    public GameObject bulletFromPrefab;
    public GameObject missileFromPrefab;

    public float EnemyHealthLife;

    // Start is called before the first frame update
    void Start()
    {
        side = "American";
        GetComponent<StateMachine>().ChangeState(new SupportState());
        //side = gameObject.tag;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject GO = GameObject.FindGameObjectWithTag("Russian");
        if (GO != null)
        {
            EnemyHealthLife = GO.GetComponent<Russian>().health;
        }
    }
}
