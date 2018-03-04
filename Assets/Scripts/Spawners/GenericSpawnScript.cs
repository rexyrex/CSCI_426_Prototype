using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSpawnScript : MonoBehaviour {
	
	public GameObject[] spawnObjects;

	public float distanceFromPlayerToSpawn;

	public float enemySpawnFreq;
	float lastSpawnedTime = 0f;

	Transform p1T;
	Transform p2T;

	// Use this for initialization
	void Start () {
		lastSpawnedTime = 0;
		p1T = GameObject.FindGameObjectWithTag ("Player1Tag").transform;
		p2T = GameObject.FindGameObjectWithTag ("Player2Tag").transform;
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		Quaternion quat = new Quaternion(0, 0, 0, 0);
		float distanceFromP1 = Vector3.Distance (pos, p1T.position);
		float distanceFromP2 = Vector3.Distance (pos, p2T.position);


		if (Time.time - lastSpawnedTime > enemySpawnFreq && Mathf.Min(distanceFromP1,distanceFromP2) < distanceFromPlayerToSpawn) {
			lastSpawnedTime = Time.time;
			int objInd = Random.Range (0, spawnObjects.Length);
			GameObject inst = Instantiate(spawnObjects[objInd], pos, quat);
		}

	}
}
