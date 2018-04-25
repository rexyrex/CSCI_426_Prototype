using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SplittingEnemyScript : BasicEnemyScript {

    private float splitFreq = 4f;
    private float lastSplit;

	private bool isBreeder;

    public GameObject clone;
    Quaternion quat = new Quaternion(0, 0, 0, 0);

    // Use this for initialization
    protected override void Start () {
        base.Start();

        lastSplit = Time.time;
		if (Random.value < 0.5) {
			isBreeder = false;
			GetComponent<NavMeshAgent>().isStopped = false;
		} else {
			isBreeder = true;
			GetComponent<NavMeshAgent>().isStopped = true;
		}

	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time - lastSplit > splitFreq)
        {
            GameObject inst = Instantiate(clone, gameObject.transform.position, quat);
            lastSplit = Time.time;
        }
	}

	public override void OnHitByChain(float damage, bool isChainActive)
	{
		if (isChainActive) {
			Destroy (gameObject);
		}	
	}
}
