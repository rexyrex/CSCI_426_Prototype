using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleTrackScript : MonoBehaviour {

    public Transform p1T;
    public Transform p2T;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.position = p1T.transform.position + (p2T.transform.position - p1T.transform.position) / 2;
	}
}
