using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangerScript : MonoBehaviour {

	float lastActive;
	float activeCoolDown = 10.0f;
	public Material activeMat;
	public Material inactiveMat;
	bool isactive;

	// Use this for initialization
	void Start () {
		lastActive = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - lastActive > activeCoolDown) {

			GetComponent<Renderer> ().material = activeMat;
			isactive = true;
		} else {
			isactive = false;
			GetComponent<Renderer> ().material = inactiveMat;
		}
	}

	void OnTriggerEnter(Collider other){
		//Debug.Log ("trig");
		if (other.tag == "Chain" && GlobalDataController.gdc.chainCharged && isactive) {
			lastActive = Time.time;

			GlobalDataController.gdc.boulderState = GlobalDataController.gdc.chainState;
		}			
	}

	/*void OnCollisionEnter(Collision other){
		Debug.Log ("col");
		if (other.collider.tag == "Chain"&& GlobalDataController.gdc.chainCharged) {
			GlobalDataController.gdc.boulderState = GlobalDataController.gdc.chainState;
		}			
	}*/
}
