using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Russian : MonoBehaviour
{
    public float health = 100;
    public int bulletDamage = 10;
    public int missileDamage = 100;
    public float deathRotationSpeed = 150;
    Rigidbody rb;
    public string side = "Russian";
    public GameObject bulletFromPrefab;
    public GameObject missileFromPrefab;

    public GameObject ExplosionEffect;
    public AudioSource explode;

    // Start is called before the first frame update
    void Start()
    {
        explode = GetComponent<AudioSource>();
        rb = this.gameObject.GetComponent<Rigidbody>();
        GetComponent<StateMachine>().ChangeState(new WanderState());
        StartCoroutine(BarrelRoll());
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0f)
        {

            StartCoroutine(Dead());
            PlaneDeath();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            health = health - bulletDamage;
            Debug.Log("Hit");
            Destroy(other.gameObject);
        }
        else if(other.gameObject.CompareTag("Missile"))
        {
            health = health - missileDamage;
            Destroy(other.gameObject);
        }
    }

    private void PlaneDeath()
    {
        bool played = false;
        if(played == false)
        {
            explode.Play();
            played = true;
        }
        rb.useGravity = true;
        Boid b = this.gameObject.GetComponent<Boid>();
        transform.Rotate(Vector3.up, Time.deltaTime * deathRotationSpeed);
        Instantiate(ExplosionEffect, transform.position, transform.rotation);
        /*Component[] steeringforces = GetComponents(typeof(SteeringBehaviour));
        foreach (SteeringBehaviour sf in steeringforces)
        {
            b.behaviours.Remove(sf);
        }*/
    }

    private IEnumerator Dead()
    {
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
        StopCoroutine(Dead());
    }
    
    private IEnumerator BarrelRoll()
    {
        
        yield return new WaitForSeconds(2f);
        /*float rollDirection = Random.Range(1, 2);
        Debug.Log("Roll");
        if(rollDirection >= 1.5)
        {
            Quaternion newrotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + 350);
            Quaternion roll = transform.rotation ;
            transform.rotation = Quaternion.Slerp(transform.rotation, newrotation, Time.deltaTime * 200);
        }
        else if(rollDirection < 1.5)
        {
            Quaternion newrotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + -350);
            Quaternion roll = transform.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, newrotation, Time.deltaTime * 200);
        }*/
    }
}