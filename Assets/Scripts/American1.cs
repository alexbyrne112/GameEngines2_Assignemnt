using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class American1 : MonoBehaviour
{
    //[SerializeField]
    //public GameObject target;
    //public GameObject america1;
    [SerializeField]
    public GameObject bulletFromPrefab;
    public GameObject missileFromPrefab;
    public string side;
    // Start is called before the first frame update
    void Start()
    {
        side = this.gameObject.tag;
        GetComponent<StateMachine>().ChangeState(new SearchState());
        //GetComponent<StateMachine>().ChangeState(new AttackingState(target, this.gameObject));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
