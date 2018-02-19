using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ColorSpawnBossMovement : MonoBehaviour {
	NavMeshAgent playerAgent;

	float changeDestFreq = 5f;
	float changeDestLast;




	// Use this for initialization
	void Start () {
		playerAgent = GetComponent<NavMeshAgent>();
		changeDestLast = Time.time;
		playerAgent.destination =  (RandomNavSphere (new Vector3 (0, 0, 0), 50f, -1));
	}

	Vector3 RandomNavSphere (Vector3 origin, float distance, int layermask) {
		Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;

		randomDirection += origin;

		NavMeshHit navHit;

		NavMesh.SamplePosition (randomDirection, out navHit, distance, layermask);

		return navHit.position;
	}

	// Update is called once per frame
	void Update () {
		if (Time.time - changeDestLast > changeDestFreq) {
			changeDestLast = Time.time;
			//Debug.Log (changeDestLast);
			playerAgent.destination =  (RandomNavSphere (new Vector3 (0, 0, 0), 50f, -1));
		}
	}
}
