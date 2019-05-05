using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class American1 : MonoBehaviour
{
    [SerializeField]
    public GameObject bulletFromPrefab;
    public GameObject missileFromPrefab;
    public string side;
    public GameObject ally;

    public float EnemyHealthLife;

    public bool attack = false;
    public GameObject attackTarget;
    public int missileCount = 1;
    // Start is called before the first frame update
    void Start()
    {
        side = "American";
        GetComponent<StateMachine>().ChangeState(new SearchState());
    }

    // Update is called once per frame
    void Update()
    {
        if(attack == true && missileCount > 0)
        {
            StartCoroutine(FireMissile());
            missileCount = 0;
        }
        //EnemyHealthLife = GameObject.FindGameObjectWithTag("Russian").GetComponent<Russian>().health;
        GameObject GO = GameObject.FindGameObjectWithTag("Russian");
        if(GO != null)
        {
            EnemyHealthLife = GO.GetComponent<Russian>().health;
        }
    }

    private IEnumerator FireMissile()
    {
        yield return new WaitForSeconds(5f);
        Vector3 MissileFire = this.transform.position - this.transform.up * 2;
        GameObject Missile = Object.Instantiate(missileFromPrefab, MissileFire, this.transform.rotation);
        Seek missileSeek = Missile.GetComponent<Seek>();
        missileSeek.missileTarget = attackTarget;
        attack = false;
        StopCoroutine(FireMissile());
    }
}
