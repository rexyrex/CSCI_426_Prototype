using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SplittingEnemyScript : MonoBehaviour {

    private float splitFreq = 4f;
    private float lastSplit;

	private bool isBreeder;

    public GameObject clone;
    Quaternion quat = new Quaternion(0, 0, 0, 0);

    // Use this for initialization
    void Start () {
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
}
