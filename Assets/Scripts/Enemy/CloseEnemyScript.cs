using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseEnemyScript : BasicEnemyScript {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void OnHitByChain(float damage, bool isChainActive)
	{
		if (GlobalDataController.gdc.chainState == GlobalDataController.ChainDistance.Close) {
			Destroy (gameObject);
		}
	}
}
