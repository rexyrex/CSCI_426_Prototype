﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSpawnScript : MonoBehaviour {
	
	public GameObject[] spawnObjects;

	public float distanceFromPlayerToSpawn;

	public float enemySpawnFreq;
	float lastSpawnedTime = 0f;

	public Material activeMat;
	public Material inactiveMat;

	Renderer rend;

	bool spawnerActive;


	Transform p1T;
	Transform p2T;
	Transform boulderT;

	// Use this for initialization
	void Start () {
		lastSpawnedTime = 0;
		rend  = GetComponent<Renderer>();
		p1T = GameObject.FindGameObjectWithTag ("Player1Tag").transform;
		p2T = GameObject.FindGameObjectWithTag ("Player2Tag").transform;
		boulderT = GameObject.FindGameObjectWithTag ("BoulderTag").transform;
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		float distanceFromP1 = Vector3.Distance (pos, p1T.position);
		float distanceFromP2 = Vector3.Distance (pos, p2T.position);
		float distanceFromB = Vector3.Distance (pos, boulderT.position);

		if (distanceFromB > 2) {
			spawnerActive = true;
			rend.material = activeMat;
		} else {
			spawnerActive = false;
			rend.material = inactiveMat;
		}

		if (Time.time - lastSpawnedTime > enemySpawnFreq && Mathf.Min (distanceFromP1, distanceFromP2) < distanceFromPlayerToSpawn && spawnerActive) {
			Spawn(pos);
		}



	}

	void Spawn(Vector3 pos){
		
		Quaternion quat = new Quaternion(0, 0, 0, 0);
		lastSpawnedTime = Time.time;
		int objInd = Random.Range (0, spawnObjects.Length-1);
		GameObject inst = Instantiate(spawnObjects[objInd], pos, quat);
	}
}