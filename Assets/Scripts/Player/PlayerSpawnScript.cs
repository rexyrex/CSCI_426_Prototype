﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnScript : MonoBehaviour {

	public Transform spawnPoint;
	float spawnDelay = 0.5f;
	float elapsed;
	bool posSet;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Rigidbody> ().useGravity = false;
		elapsed = 0;
		posSet = false;
	}

	void Awake(){
		
	}
	
	// Update is called once per frame
	void Update () {
		elapsed += Time.deltaTime;
		if (elapsed >= spawnDelay && posSet == false) {
			posSet = true;
			SetPos();
		}
	}

	void SetPos(){
		gameObject.GetComponent<Rigidbody> ().useGravity = true;
		gameObject.transform.position = spawnPoint.position;
	}
}
