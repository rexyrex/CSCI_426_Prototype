using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBorderSript : MonoBehaviour {

	GameObject p1;
	GameObject p2;
	bool p1Inside;
	bool p2Inside;
	public Canvas hurtCanvas;

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
		//if (Vector3.Distance (p1.transform.position, gameObject.transform.position) < gameObject.GetComponent<SphereCollider>().radius) {
		if (p1Inside) {
			
			p1.GetComponent<Player1Script> ().Damage (0.1f);
			hurtCanvas.enabled = true;
		} else

		//if (Vector3.Distance (p2.transform.position, gameObject.transform.position) < gameObject.GetComponent<SphereCollider>().radius) {
		if (p2Inside) {
			p2.GetComponent<Player2Script> ().Damage (0.1f);
				hurtCanvas.enabled = true;
		} else {
			hurtCanvas.enabled = false;
		}


		if (Time.time - scaleLastInc > scaleIncFreq) {
			scaleLastInc = Time.time;
			transform.localScale += new Vector3(0.03F, 0.03F, 0.03F);

		}



	}

	void OnTriggerEnter(Collider other){
		//Debug.Log ("COLLIDEDEDEDED");
		if (other.tag == "Player1Tag" ) {
			other.GetComponent<Player1Script> ().Damage (10f);
			p1Inside = true;

			//Debug.Log ("ouch");
		}

		if (other.tag == "Player2Tag" ) {
			other.GetComponent<Player2Script> ().Damage (10f);
			p2Inside = true;
		}	
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

	void toggle(bool z){
		if (z) {
			z = false;
		} else {
			z = true;
		}
	}
}
