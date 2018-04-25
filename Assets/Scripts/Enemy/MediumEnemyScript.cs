﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumEnemyScript : BasicColorEnemyScript {

	public GameObject manaObject;
	bool dead;
	private float spawnTime;
	// Use this for initialization
    protected override void Start () {
        base.Start();

		dead = false;
		spawnTime = Time.time;
	}

	public override string getType()
	{
		return "medium";
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - spawnTime > lifeTimer) {
			KillOff ();
		}
	}

	public override void KillOff(){
		Destroy (gameObject);
	}

	public override void OnHitByChain(float damage, bool isChainActive)
	{
		if (GlobalDataController.gdc.chainState == GlobalDataController.ChainDistance.Medium && isChainActive && !dead) {
			dead = true;
			GetComponent<SphereCollider> ().enabled = false;
			Die ();

		}
	}
}
