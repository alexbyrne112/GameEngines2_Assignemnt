using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed = 5000f;
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke("StopBullet", 5);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, Time.deltaTime * speed);
    }

    public void StopBullet()
    {
        Destroy(this.gameObject);
    }
}
