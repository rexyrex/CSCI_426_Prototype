using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTilesScript : MonoBehaviour {

	public GameObject nextTile;
	bool activate;
	Rigidbody rb;
	public Material redMat;

	public float timeTillNextTile = 1f;
	float activationTime;
	float activateCounter;

	// Use this for initialization
	void Start () {
		activate = false;
		rb = GetComponent<Rigidbody> ();
		rb.useGravity = false;
		activationTime = float.MaxValue;
	}
	
	// Update is called once per frame
	void Update () {
		if (activate) {
            Debug.Log(transform.name);
			activateCounter += Time.deltaTime;
			GetComponent<Renderer> ().material = redMat;
		}

		if (activateCounter > timeTillNextTile) {
            Debug.Log("fall");
            rb.constraints = RigidbodyConstraints.None;
			rb.useGravity = true;
			activateNextTile ();
		}
	}

	public void activateNextTile(){
		if (nextTile != null) {
			
			nextTile.GetComponent<FallingTilesScript> ().activateTile ();
		}

	}

	public void activateTile(){
		activate = true;
	}
}
