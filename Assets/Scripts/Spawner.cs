using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    Vector3 spawnLoaction;
    public GameObject RussianPrefab;
    public bool StopSpawning = false;
    // Start is called before the first frame update
    void Start()
    {
        //RussianPlane = Instantiate(RussianPrefab, spawnLoaction, Quaternion.identity);
        InvokeRepeating("SpawnPlane", 5, 5);
    }

    public void SpawnPlane()
    {
        Debug.Log("1");
        if(GameObject.Find("Su-34") == null && GameObject.Find("Su-34 1(Clone)") == null)
        {
            Debug.Log("1");
            spawnLoaction = new Vector3(Random.Range(-350, 350), 0, Random.Range(-350, 350));
            Instantiate(RussianPrefab, spawnLoaction, Quaternion.identity);
            if (StopSpawning)
            {
                CancelInvoke("SpawnPlane");
            }
        }
    }
}
