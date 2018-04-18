using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase : EnvironmentObject {
    public bool ignoreAll;
    public GameObject[] toIgnore;
    public bool startActive;
    MeshRenderer mesh;
    Collider col;
    
	// Use this for initialization
	void Start () {
        mesh = this.GetComponent<MeshRenderer>();
        col = this.GetComponent<Collider>();
        if (startActive) Actuate();
        else Revert();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void Actuate()
    {
        base.Actuate();
        mesh.enabled = false;
        col.enabled = false;
    }

    public override void Revert()
    {
        base.Revert();
        mesh.enabled = true;
        col.enabled = true;
    }
}
