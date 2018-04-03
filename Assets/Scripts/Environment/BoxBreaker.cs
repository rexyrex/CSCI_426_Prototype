using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBreaker : BreakScript {
    private GameObject whole;
    public GameObject broken;

	// Use this for initialization
	void Start () {
        whole = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        
	}


    public override void Break()
    {
        Instantiate(broken, whole.transform.position, whole.transform.rotation);
        Destroy(whole);
    }
}
