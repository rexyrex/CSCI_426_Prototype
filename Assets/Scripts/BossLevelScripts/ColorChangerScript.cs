using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		Debug.Log ("COLLIDEDEDEDED");
		if (other.tag == "Chain" && GlobalDataController.gdc.chainCharged) {
			GlobalDataController.gdc.boulderState = GlobalDataController.gdc.chainState;
		}			
	}

	void OnCollisionEnter(Collision other){
		Debug.Log ("COLLIDEDEDEDED");
		if (other.collider.tag == "Chain"&& GlobalDataController.gdc.chainCharged) {
			GlobalDataController.gdc.boulderState = GlobalDataController.gdc.chainState;
		}			
	}
}
