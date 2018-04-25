using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketScript : MonoBehaviour {

	Transform target;
	public float speed;
	public GameObject explosion;
	Vector3 targetPos;
	void Start(){
		GameObject bossobj = GameObject.FindGameObjectWithTag ("Boss");
		target = bossobj.transform;
		targetPos = target.position;
		targetPos.y += 1.2f;
	}

	void Update() {
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
	}

	void OnTriggerEnter(Collider other){
		//Debug.Log ("COLLIDEDEDEDED");
		if (other.tag == "Boss" ) {
			int damage = Random.Range (350,700);
			other.gameObject.GetComponent<BoulderBossScript> ().getDamaged (damage);
			Vector3 pos = gameObject.transform.position;
			Quaternion quat = new Quaternion(0, 0, 0, 0);
			Instantiate (explosion, pos, quat);
			Destroy (gameObject);
			//Debug.Log ("ouch");
		}	
	}
		


}
