using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyMovement : MonoBehaviour {

    Transform p1Trans;
    Transform p2Trans;

    GameObject[] objs;

    NavMeshAgent playerAgent;
	float destChangeFreq = 2f;
	float lastDestChange;


    void Start () {
		lastDestChange = Time.time;
        playerAgent = GetComponent<NavMeshAgent>();
        p1Trans = GameObject.FindGameObjectsWithTag("Player1Tag")[0].transform;
        p2Trans = GameObject.FindGameObjectsWithTag("Player2Tag")[0].transform;
    }


	

	void FixedUpdate () {
       
		if (playerAgent.isActiveAndEnabled) {
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
		if (Time.time - lastDestChange > destChangeFreq) {
			
			playerAgent.destination = dest.position;
		}
	}
}
