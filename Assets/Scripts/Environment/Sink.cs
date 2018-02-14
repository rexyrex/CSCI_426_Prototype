﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sink : EnvironmentObject {
    protected Vector3 movement;
    protected BoxCollider bc;
    protected Renderer mat;
    protected Color activeCol;
    protected Color fadeCol;

	// Use this for initialization
	void Start () {
        //movement = new Vector3(0, this.transform.position.y*2+0.1f, 0);
        bc = this.GetComponent<BoxCollider>();
        mat = this.GetComponent<Renderer>();
        activeCol = mat.material.color;
        fadeCol = new Color(activeCol.r, activeCol.g, activeCol.b, 0.5f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void Actuate()
    {
        //this.transform.Translate(movement*-1);
        mat.material.color = fadeCol;
        bc.enabled = false;
    }

    public override void Revert()
    {
        //this.transform.Translate(movement);
        mat.material.color = activeCol;
        bc.enabled = false;
    }
}
