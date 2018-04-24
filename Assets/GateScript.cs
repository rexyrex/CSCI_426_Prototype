using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateScript : EnvironmentObject {
    Renderer rend;
    Collider col;
    public bool isOn;

	// Use this for initialization
	void Start () {
        rend = this.GetComponent<Renderer>();
        col = this.GetComponent<Collider>();
        rend.enabled = isOn;
        col.enabled = isOn;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void Switch()
    {
        isOn = !isOn;
        rend.enabled = isOn;
        col.enabled = isOn;
    }

    public override void Actuate()
    {
        this.Switch();
    }

    public override void Revert()
    {
        this.Switch();
    }
}
