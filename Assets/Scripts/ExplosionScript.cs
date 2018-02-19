using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Add this script to an explosion. Plays particlesystem and destroys itself after.
public class ExplosionScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<ParticleSystem> ().Play ();
		Destroy (gameObject, GetComponent<ParticleSystem> ().duration);
		//Destroy (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
