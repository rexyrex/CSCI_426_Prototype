using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sink : EnvironmentObject {
    protected Vector3 movement;
    protected BoxCollider bc;
    protected Renderer mat;
    protected Color activeCol;
    protected Color fadeCol;
    protected bool reverting;
    public float fadeTime;
    protected float counter;

	// Use this for initialization
	void Start () {
        //movement = new Vector3(0, this.transform.position.y*2+0.1f, 0);
        bc = this.GetComponent<BoxCollider>();
        mat = this.GetComponent<Renderer>();
        activeCol = mat.material.color;
        fadeCol = new Color(activeCol.r, activeCol.g, activeCol.b, 0.5f);
        reverting = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (reverting)
        {
            if (counter >= fadeTime)
            {
                reverting = false;
                mat.material.color = activeCol;
                bc.enabled = true;
            }
            else
            {
                float a = mat.material.color.a;
                Color newCol = new Color(activeCol.r, activeCol.g, activeCol.b, a + 0.5f * Time.deltaTime / fadeTime);
                mat.material.color = newCol;
                counter += Time.deltaTime;
            }
        }
	}

    public override void Actuate()
    {
        //this.transform.Translate(movement*-1);
        mat.material.color = fadeCol;
        bc.enabled = false;
        reverting = false;
        counter = 0;
    }

    public override void Revert()
    {
        //this.transform.Translate(movement);
        reverting = true;
        counter = 0;
    }
}
