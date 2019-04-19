using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Russian : MonoBehaviour
{
    public float health = 100;
    public int bulletDamage = 10;
    public float deathRotationSpeed = 150;
    public bool dead = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (health == 0f)
        {
            dead = true;
            this.gameObject.AddComponent<Rigidbody>();
            Boid b = this.gameObject.GetComponent<Boid>();
            Component[] steeringforces = GetComponents(typeof(SteeringBehaviour));
            foreach(MigMove sf in steeringforces)
            {
                b.behaviours.Remove(sf);
            }

        }
        if(dead == true)
        {
            this.transform.Rotate(Vector3.right, Time.deltaTime * deathRotationSpeed * deathRotationSpeed);
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
    }
}