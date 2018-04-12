using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBoulderScript : MonoBehaviour {

	public string[] canPushObjects;
	HashSet<string> canPush;

	public GameObject explosionClose;
	public GameObject explosionMed;
	public GameObject explosionFar;

	GameObject boss;
	float distanceFromBoss;
	float distanceLimit = 50;

	// Use this for initialization
	void Start () {

		boss = GameObject.FindGameObjectWithTag ("Boss");
		distanceFromBoss = Vector3.Distance (boss.transform.position, transform.position);
		canPush = new HashSet<string>();
		foreach (var s in canPushObjects)
			canPush.Add(s);
	}

	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (boss.transform.position, transform.position) > distanceLimit) {
			boss.GetComponent<BoulderBossScript> ().spawnNewBoulder ();
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if (canPush.Contains(collision.gameObject.tag) && GlobalDataController.gdc.chainState == GlobalDataController.gdc.boulderState && GlobalDataController.gdc.chainCharged) {
			gameObject.GetComponent<Rigidbody> ().mass = 5;
		} else {
			gameObject.GetComponent<Rigidbody> ().mass = 100000;
		}

		if (collision.gameObject.tag == "Boss" ) {




			//Destroy (gameObject);
		}
	}

	public void destroyBoulder(int explosiontype){
		Vector3 pos = gameObject.transform.position;
		Quaternion quat = new Quaternion(0, 0, 0, 0);
		switch (explosiontype) {
		case 1:
			Instantiate (explosionClose, pos, quat);
			break;
		case 2:
			Instantiate (explosionMed, pos, quat);
			break;
		case 3:
			Instantiate (explosionFar, pos, quat);
			break;
		default :
			break;
		}

		//GameObject inst = Instantiate(manaObject, pos, quat);
		Destroy (gameObject);
	}
}
