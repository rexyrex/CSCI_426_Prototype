using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumEnemyScript : BasicColorEnemyScript {

	public GameObject manaObject;
	public GameObject explosion;
	bool dead;
	private float spawnTime;
	// Use this for initialization
	void Start () {
		dead = false;
		spawnTime = Time.time;
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

	public override void Die(){
		Vector3 pos = gameObject.transform.position;
		Quaternion quat = new Quaternion(0, 0, 0, 0);
		Instantiate (explosion, pos, quat);
		pos.y += 2;
		//GameObject inst = Instantiate(manaObject, pos, quat);

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
