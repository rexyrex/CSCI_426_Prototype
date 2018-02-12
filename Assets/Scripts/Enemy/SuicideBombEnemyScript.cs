using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideBombEnemyScript : BasicEnemyScript {

    private float explodeDelay = 10f;
    private float timeInitialized = 0f;
    //public ParticleSystem explosion;


	void Start () {
        timeInitialized = Time.time;
	}
	

	void Update () {
		if(Time.time - timeInitialized > explodeDelay)
        {
            var exp = gameObject.transform.GetChild(0).GetComponent<ParticleSystem>();
            exp.Play();
            Destroy(gameObject, exp.main.duration);
        }
	}

	public override void OnHitByChain(float damage, bool isChainActive)
	{
		if (isChainActive) {
			Destroy (gameObject);
		}

	}

}
