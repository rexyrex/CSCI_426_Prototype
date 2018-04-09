﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSpawnScript : MonoBehaviour {
	
	public GameObject[] spawnObjects;

	public float distanceFromPlayerToSpawn;

	public float enemySpawnFreq;
    public float waveTime;
    public float difficulty; // 0-3;
    float numToSpawn;
    float growthRate;
    int numLeft;
	float lastSpawnedTime = 0f;
    float lastWave = 0.0f;
    int spawnmax = 10;
    bool spawning = false;

	public Material activeMat;
	public Material inactiveMat;

	Renderer rend;

	bool spawnerActive;


	Transform p1T;
	Transform p2T;
	Transform[] boulderT;
	GameObject[] boulders;

	// Use this for initialization
	void Start () {
		lastSpawnedTime = 0;
		rend  = GetComponent<Renderer>();
		p1T = GameObject.FindGameObjectWithTag ("Player1Tag").transform;
		p2T = GameObject.FindGameObjectWithTag ("Player2Tag").transform;

		boulders = GameObject.FindGameObjectsWithTag ("BoulderTag");

		boulderT = new Transform[boulders.Length];

		int counter = 0;
		foreach (GameObject b in boulders) {
			boulderT [counter] = b.transform;
			counter++;
		}

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
	void Update () {
		
		Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		float distanceFromP1 = Vector3.Distance (pos, p1T.position);
		float distanceFromP2 = Vector3.Distance (pos, p2T.position);

		float minDistanceFromB = float.MaxValue;
		Transform minT = boulderT [0];
		foreach (Transform t in boulderT) {
			if (Vector3.Distance (pos, t.position) < minDistanceFromB) {
				minT = t;
				minDistanceFromB = Vector3.Distance (pos, t.position);
			}
				
		}


		float distanceFromB = Vector3.Distance (pos, minT.position);

		if (distanceFromB > 2) {
			spawnerActive = true;
			rend.material = activeMat;
		} else {
			spawnerActive = false;
			rend.material = inactiveMat;
		}

        if (spawning)
        {
            if (numLeft > 0)
            {
                if (Time.time - lastSpawnedTime > enemySpawnFreq && Mathf.Min(distanceFromP1, distanceFromP2) < distanceFromPlayerToSpawn && spawnerActive)
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
        else if(Time.time - lastWave > waveTime && Mathf.Min(distanceFromP1, distanceFromP2) < distanceFromPlayerToSpawn && spawnerActive)
        {
            numLeft = (int)numToSpawn;
            lastWave = Time.time;
            spawning = true;
            numToSpawn++;
        }
		



	}

	void Spawn(Vector3 pos){
		
		Quaternion quat = new Quaternion(0, 0, 0, 0);
		lastSpawnedTime = Time.time;
		int objInd = Random.Range (0, spawnObjects.Length-1);
		GameObject inst = Instantiate(spawnObjects[objInd], pos, quat);
	}
}
