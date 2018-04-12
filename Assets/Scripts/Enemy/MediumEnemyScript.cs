﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumEnemyScript : BasicEnemyScript {

	public GameObject manaObject;
	public GameObject explosion;
	bool dead;
	// Use this for initialization
	void Start () {
		dead = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void OnHitByChain(float damage, bool isChainActive)
	{
		if (GlobalDataController.gdc.chainState == GlobalDataController.ChainDistance.Medium && isChainActive && !dead) {
			dead = true;
			GetComponent<SphereCollider> ().enabled = false;
			Vector3 pos = gameObject.transform.position;

			Quaternion quat = new Quaternion(0, 0, 0, 0);
			Instantiate (explosion, pos, quat);
			pos.y += 2;
			GameObject inst = Instantiate(manaObject, pos, quat);
			Destroy (gameObject);

		}
	}
}
