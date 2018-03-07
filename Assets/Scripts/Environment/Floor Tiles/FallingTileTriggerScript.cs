using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTileTriggerScript : MonoBehaviour {


	Transform p1Trans;
	Transform p2Trans;
	float triggerSize = 3f;

	public GameObject tileToActivate;

	// Use this for initialization
	void Start () {
		p1Trans = GameObject.FindGameObjectWithTag ("Player1Tag").transform;
		p2Trans = GameObject.FindGameObjectWithTag ("Player2Tag").transform;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		float distanceFromP1 = Vector3.Distance (pos, p1Trans.position);
		float distanceFromP2 = Vector3.Distance (pos, p2Trans.position);

		if (Mathf.Min (distanceFromP1, distanceFromP2) < triggerSize) {
			tileToActivate.GetComponent<FallingTilesScript> ().activateTile ();
		}

	}



}
