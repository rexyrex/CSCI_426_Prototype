using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBoulderScript : MonoBehaviour {

	public string[] canPushObjects;
	HashSet<string> canPush;

	// Use this for initialization
	void Start () {
		canPush = new HashSet<string>();
		foreach (var s in canPushObjects)
			canPush.Add(s);
	}

	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter(Collision collision)
	{
		if (canPush.Contains(collision.gameObject.tag) && GlobalDataController.gdc.chainState == GlobalDataController.gdc.boulderState) {
			gameObject.GetComponent<Rigidbody> ().mass = 3;
		} else {
			gameObject.GetComponent<Rigidbody> ().mass = 100000;
		}
	}
}
