using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Press : EnvironmentObject {
    protected Vector3 movement;

    // Use this for initialization
    void Start () {
        float yDist = transform.localScale.y / 2;
        movement = new Vector3(0, yDist, 0);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void Actuate()
    {
        this.transform.Translate(movement*-1);
    }

    public override void Revert()
    {
      this.transform.Translate(movement);
    }
}
