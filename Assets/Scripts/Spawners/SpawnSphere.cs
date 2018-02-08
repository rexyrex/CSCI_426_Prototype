using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSphere : MonoBehaviour {

    public GameObject sphereObject;
    public GameObject sphere2Object;
    public GameObject manaObject;

    //public Transform spawnLoc;

    float enemySpawnFreq = 3f;
    float enemySpawnCounter = 0f;

    float manaSpawnFreq = 2.5f;
    float manaSpawnCounter = 0f;

    float enemy2SpawnFreq = 2.4f;
    float enemy2SpawnCounter = 0f;

    bool spawnToggle;

    void Start () {
        spawnToggle = false;
	}
	

	void Update () {

        if (spawnToggle)
        {
            enemySpawnCounter += Time.deltaTime;
            manaSpawnCounter += Time.deltaTime;
            enemy2SpawnCounter += Time.deltaTime;
        }
        



        float randx = Random.Range(-11, 11);
        float randz = Random.Range(-11, 11);

        Vector3 pos = new Vector3(randx, 0.2f, randz);
        Quaternion quat = new Quaternion(0, 0, 0, 0);

        if (manaSpawnCounter >= manaSpawnFreq)
        {
            manaSpawnCounter = 0f;
            GameObject inst = Instantiate(manaObject, pos, quat);

        }

        if (enemySpawnCounter >= enemySpawnFreq)
        {
            enemySpawnCounter = 0f;
            GameObject inst = Instantiate(sphereObject, pos, quat);
        }

        if(enemy2SpawnCounter >= enemy2SpawnFreq)
        {
            enemy2SpawnCounter = 0f;
            GameObject inst = Instantiate(sphere2Object, pos, quat);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {            
            GameObject inst = Instantiate(sphereObject, pos, quat);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            GameObject inst = Instantiate(manaObject, pos, quat);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (spawnToggle)
            {
                spawnToggle = false;
                Debug.Log("Spawn Started!");
            } else
            {
                spawnToggle = true;
                Debug.Log("Spawn Stopped!");
            }
        }
    }



}
