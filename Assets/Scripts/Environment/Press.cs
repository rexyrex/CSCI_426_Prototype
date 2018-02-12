using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Press : EnvironmentObject {
    protected Vector3 movement;

    // Use this for initialization
    void Start () {
        movement = new Vector3(0, this.transform.position.y * 2+0.01f, 0);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void Actuate()
    {
        this.transform.Translate(movement * -1);
    }

    public override void Revert()
    {
        this.transform.Translate(movement);
    }
}
