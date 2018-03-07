using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderScript : MonoBehaviour {

	List<string> pushableList;
	List<string> unpushableList;

	// Use this for initialization
	void Start () {
		pushableList = new List<string> (new string[] { "" });
		unpushableList = new List<string> (new string[] { "" });
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision)
	{
		if (pushableList.Contains( collision.gameObject.tag)){// || collision.gameObject.tag == "Player2Tag") {
			gameObject.GetComponent<Rigidbody> ().mass = 3;
		} else {
			gameObject.GetComponent<Rigidbody> ().mass = 100000;
		}
	}

}
