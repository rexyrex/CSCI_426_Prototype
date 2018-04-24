using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingSpawner : MonoBehaviour {
    public GameObject[] spawnObjects;

    public float distanceFromPlayerToSpawn;
    public int id = 0;
    public float enemySpawnFreq;
    public float waveTime;
    public float difficulty; // 0-3;
    public GameObject explosion;

    float numToSpawn;
    float growthRate;
    int numLeft;
    float lastSpawnedTime = 0f;
    float lastWave = 0.0f;
    int spawnmax = 10;
    bool spawning = false;

    Transform p1T;
    Transform p2T;

    // Use this for initialization
    void Start()
    {
        lastSpawnedTime = 0;
        p1T = GameObject.FindGameObjectWithTag("Player1Tag").transform;
        p2T = GameObject.FindGameObjectWithTag("Player2Tag").transform;

        // Sets the difficulty of the spawner
        if (difficulty <= 0)
        {
            numToSpawn = 2;
            growthRate = 0.2f;
        }
        else if (difficulty == 1)
        {
            numToSpawn = 3;
            growthRate = 0.34f;
        }
        else if (difficulty == 2)
        {
            numToSpawn = 3;
            growthRate = 0.5f;
        }
        else if (difficulty >= 3)
        {
            numToSpawn = 4;
            growthRate = 1f;
        }
    }

    // Update is called once per frame
    void Update()
    {

        // Seeing if players are close enough to be active
        Vector3 pos = this.transform.position;
        float distanceFromP1 = Vector3.Distance(pos, p1T.position);
        float distanceFromP2 = Vector3.Distance(pos, p2T.position);

        // Spawning enemies if desired
        if (spawning)
        {
            if (numLeft > 0)
            {
                if (Time.time - lastSpawnedTime > enemySpawnFreq && Mathf.Min(distanceFromP1, distanceFromP2) < distanceFromPlayerToSpawn)
                {
                    Spawn(pos);
                    numLeft--;
                }
            }
            else
            {
                spawning = false;
            }
        }
        else if (Time.time - lastWave > waveTime && Mathf.Min(distanceFromP1, distanceFromP2) < distanceFromPlayerToSpawn)
        {
            numLeft = (int)numToSpawn;
            if (numLeft > spawnmax) numLeft = spawnmax;
            lastWave = Time.time;
            spawning = true;
            numToSpawn++;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject o = collision.gameObject;
        if (o.tag == "BoulderTag")
        {
            if (o.GetComponent<Boulder>() != null && o.GetComponent<Boulder>().id == this.id)
            {
                Deactivate(o);
            }
        }
    }

    void Spawn(Vector3 pos)
    {
        Quaternion quat = new Quaternion(0, 0, 0, 0);
        lastSpawnedTime = Time.time;
        int objInd = Random.Range(0, spawnObjects.Length - 1);
        GameObject inst = Instantiate(spawnObjects[objInd], pos, quat);
    }

    void Deactivate(GameObject boulder)
    {
        Vector3 pos = gameObject.transform.position;
        Quaternion quat = new Quaternion(0, 0, 0, 0);
        Instantiate(explosion, pos, quat);
        Destroy(boulder);
        Destroy(this.gameObject);
    }
}
