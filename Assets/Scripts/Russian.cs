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

    public GameObject ExplosionEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0f)
        {
            PlaneDeath();
            StartCoroutine(Dead());
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
        rb.useGravity = true;
        Boid b = this.gameObject.GetComponent<Boid>();
        Component[] steeringforces = GetComponents(typeof(SteeringBehaviour));
        foreach (MigMove sf in steeringforces)
        {
            Destroy(sf);
            b.behaviours.Remove(sf);
        }
        transform.Rotate(Vector3.up, Time.deltaTime * deathRotationSpeed);
        Instantiate(ExplosionEffect, transform.position, transform.rotation);
    }

    private IEnumerator Dead()
    {
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
        StopCoroutine(Dead());
    }
}