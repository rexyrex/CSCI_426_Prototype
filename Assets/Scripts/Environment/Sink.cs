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
    public float fadedTime;
    protected float counter;
    bool actuating;
    bool faded;

	// Use this for initialization
	void Start () {
        //movement = new Vector3(0, this.transform.position.y*2+0.1f, 0);
        bc = this.GetComponent<BoxCollider>();
        mat = this.GetComponent<Renderer>();
        activeCol = mat.material.color;
        fadeCol = new Color(activeCol.r, activeCol.g, activeCol.b, 0.5f);
        reverting = false;
        actuating = false;
    }
	
	// Update is called once per frame
	void Update () {
        if(faded)
        {
            counter += Time.deltaTime;
            if (counter > fadedTime)
            {
                reverting = true;
                faded = false;
                counter = 0;
            }
        }

        if (actuating)
        {
            float a = mat.material.color.a;
            a = a - Time.deltaTime / fadeTime;
            if (a < 0)
            {
                Actuate();
                faded = true;
                actuating = false;
                counter = 0;
            }
            Color newCol = new Color(activeCol.r, activeCol.g, activeCol.b, a);
            mat.material.color = newCol;
            counter += Time.deltaTime;
        }

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
                Color newCol = new Color(activeCol.r, activeCol.g, activeCol.b, a + Time.deltaTime / fadeTime);
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

    private void OnCollisionEnter(Collision collision)
    {
        if(!actuating && !reverting)
        {
            counter = 0;
            this.actuating = true;
        }
    }
}
