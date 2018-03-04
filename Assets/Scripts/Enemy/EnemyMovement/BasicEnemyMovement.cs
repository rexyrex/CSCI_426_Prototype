using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyMovement : MonoBehaviour {

    Transform p1Trans;
    Transform p2Trans;

    GameObject[] objs;

    NavMeshAgent playerAgent;
	float destChangeFreq = 4f;
	float lastDestChange;

	float dFromPToActivate = 15;


    void Start () {
		lastDestChange = Time.time;
        playerAgent = GetComponent<NavMeshAgent>();
        p1Trans = GameObject.FindGameObjectsWithTag("Player1Tag")[0].transform;
        p2Trans = GameObject.FindGameObjectsWithTag("Player2Tag")[0].transform;
    }




	void FixedUpdate () {
		float distanceFromP1 = Vector3.Distance (transform.position, p1Trans.position);
		float distanceFromP2 = Vector3.Distance (transform.position, p2Trans.position);

       
		if (playerAgent.isActiveAndEnabled && Mathf.Min(distanceFromP1, distanceFromP2) < dFromPToActivate) {
			if (Vector3.Distance(gameObject.transform.position, p1Trans.position) < Vector3.Distance(gameObject.transform.position, p2Trans.position)){
				setDest (p1Trans);
				//playerAgent.destination = p1Trans.position;
			} else
			{
				setDest (p2Trans);
				//playerAgent.destination = p2Trans.position;
			}
		}  
    }

	void setDest(Transform dest){
		float distFromDest=playerAgent.remainingDistance; 
		bool arrived = distFromDest!=Mathf.Infinity && playerAgent.pathStatus==NavMeshPathStatus.PathComplete && playerAgent.remainingDistance==0;


		if (Time.time - lastDestChange > destChangeFreq || arrived) {
			lastDestChange = Time.time;
			playerAgent.destination = dest.position;
		}
	}
}
