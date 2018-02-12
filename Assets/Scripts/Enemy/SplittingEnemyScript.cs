using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplittingEnemyScript : MonoBehaviour {

    float splitFreq = 4f;
    float lastSplit;
    public GameObject clone;
    Quaternion quat = new Quaternion(0, 0, 0, 0);

    // Use this for initialization
    void Start () {
        lastSplit = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time - lastSplit > splitFreq)
        {
            GameObject inst = Instantiate(clone, gameObject.transform.position, quat);
            lastSplit = Time.time;
        }
	}
}
