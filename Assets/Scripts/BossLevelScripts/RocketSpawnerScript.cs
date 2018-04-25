using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSpawnerScript : MonoBehaviour {

	GameObject p1;
	GameObject p2;
	bool p1Inside;
	bool p2Inside;

	public string killenemytype;

	public GameObject rocket;

	float scaleIncFreq = 2f;
	float scaleLastInc;

	// Use this for initialization
	void Start () {
		p1 = GameObject.FindGameObjectWithTag ("Player1Tag");
		p2 = GameObject.FindGameObjectWithTag ("Player2Tag");
		p1Inside = false;
		p2Inside = false;
		scaleLastInc = Time.time;

	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider other){
		//Debug.Log ("COLLIDEDEDEDED");
		if (other.tag == "Player1Tag" ||  other.tag == "Player2Tag") {
			//FireRocket ();

		}

		if (other.tag == "Enemy") {
			if (other.gameObject.GetComponent<BasicColorEnemyScript> ().getType () == killenemytype) {
				other.gameObject.GetComponent<BasicColorEnemyScript> ().Die ();
				FireRocket ();
			}
		}	
	}

	void FireRocket(){
		Vector3 pos = gameObject.transform.position;
		pos.y += 3;
		Quaternion quat = new Quaternion(0, 0, 0, 0);
		Instantiate (rocket, pos, quat);
	}

	void OnTriggerExit(Collider other){
		//Debug.Log ("COLLIDEDEDEDED");
		if (other.tag == "Player1Tag" ) {
			p1Inside = false;
			//Debug.Log ("ouch");
		}

		if (other.tag == "Player2Tag" ) {
			p2Inside = false;
		}	
	}
}
