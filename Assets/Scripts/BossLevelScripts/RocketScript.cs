using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketScript : MonoBehaviour {

	Transform target;
	public float speed;
	public GameObject explosion;

	void Start(){
		GameObject bossobj = GameObject.FindGameObjectWithTag ("Boss");
		target = bossobj.transform;
	}

	void Update() {
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, target.position, step);
	}

	void OnTriggerEnter(Collider other){
		//Debug.Log ("COLLIDEDEDEDED");
		if (other.tag == "Boss" ) {
			Vector3 pos = gameObject.transform.position;
			Quaternion quat = new Quaternion(0, 0, 0, 0);
			Instantiate (explosion, pos, quat);
			Destroy (gameObject);
			//Debug.Log ("ouch");
		}	
	}
		


}
