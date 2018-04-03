using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleanup : MonoBehaviour {
    float counter;
    public float timeToDelete;

	// Use this for initialization
	void Start () {
        counter = 0;
        if (timeToDelete <= 0) timeToDelete = 4;
	}
	
	// Update is called once per frame
	void Update () {
        counter += Time.deltaTime;
        if (counter >= timeToDelete)
        {
            Clean();
            timeToDelete += 0.1f;
        }
	}

    // Delete all children
    void Clean()
    {
        foreach(Transform item in this.transform)
        {
            Destroy(item.gameObject);
            return;
        }
        Destroy(this);
    }
}
