using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : EnvironmentObject {
    public float distance;
    protected Vector3 movement;
    protected Vector3 start;
    protected Vector3 end;
    protected int way;


    // Use this for initialization
    protected virtual void Start () {
        way = 0; //Which way is it going?
        movement = new Vector3(distance, distance, distance); //Modified by children
        start = this.transform.position;
        end = this.transform.position + movement;
    }

    // Update is called once per frame
    void Update () {
		if(way == 1)
        {
            if(Vector3.Distance(this.transform.position, end) <= 0.2)
            {
                this.transform.position = end;
                way = 0;
            }
            else
            {
                this.transform.Translate(movement*Time.deltaTime);
            }
        }
        if(way == -1)
        {
            if (Vector3.Distance(this.transform.position, start) <= 0.2)
            {
                this.transform.position = start;
                way = 0;
            }
            else
            {
                this.transform.Translate(-1 * movement * Time.deltaTime);
            }
        }
	}

    public override void Actuate()
    {
        way = 1;
        
    }

    public override void Revert()
    {
        way = -1;
    }
}
