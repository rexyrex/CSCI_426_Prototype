using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseEnemyScript : BasicEnemyScript {
	public GameObject manaObject;
	public GameObject explosion;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void OnHitByChain(float damage, bool isChainActive)
	{
		if (GlobalDataController.gdc.chainState == GlobalDataController.ChainDistance.Close && isChainActive) {

			Vector3 pos = gameObject.transform.position;
			Quaternion quat = new Quaternion(0, 0, 0, 0);
			Instantiate (explosion, pos, quat);
			GameObject inst = Instantiate(manaObject, pos, quat);

			Destroy (gameObject);
		}
	}
}
