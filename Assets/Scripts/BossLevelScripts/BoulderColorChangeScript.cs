﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderColorChangeScript : MonoBehaviour {
	public enum boulderMode {Close, Medium, Far,Rainbow}

	boulderMode mode;

	public Material closeMat;
	public Material medMat;
	public Material farMat;
	public Material invincibleMat;
	public Material rainbowMat;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		mode = (boulderMode)GlobalDataController.gdc.boulderState;
		updateMaterial ();
	}

	void updateMaterial(){
		switch (mode) {
		case boulderMode.Close:
			GetComponent<Renderer> ().material = closeMat;
			break;
		case boulderMode.Medium:
			GetComponent<Renderer> ().material = medMat;
			break;
		case boulderMode.Far:
			GetComponent<Renderer> ().material = farMat;
			break;
		case boulderMode.Rainbow:
			GetComponent<Renderer> ().material = rainbowMat;
			break;
		default:
			GetComponent<Renderer> ().material = invincibleMat;
			break;
		}
	}
}
