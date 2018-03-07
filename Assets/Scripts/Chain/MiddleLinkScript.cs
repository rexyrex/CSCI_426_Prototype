using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleLinkScript : ChainLinkScript {
    private Rigidbody end;

	// Use this for initialization
	void Start () {
        
	}
	
    // Initialize ends
    public void InitializeMiddleLink(GlobalChainScript controller, Material startMat, GameObject go1, GameObject go2)
    {
        base.GenericInitialize(controller, startMat);
        line.positionCount = 3;
        end = this.GetComponent<Rigidbody>();

        e1 = go1;
        e2 = go2;

        end1 = e1.GetComponent<Rigidbody>();
        end2 = e2.GetComponent<Rigidbody>();
        
        trans1 = e1.GetComponent<Transform>();
        trans2 = e2.GetComponent<Transform>();
    }

    // Update is called once per frame
    protected override void Update () {
        //Render the Line
        line.SetPosition(0, trans1.position);
        line.SetPosition(1, this.transform.position);
        line.SetPosition(2, trans2.position);

        //Determine new length and let controller know
        oldLength = length;
        float length1 = Vector3.Distance(trans1.position, this.transform.position);
        float length2 = Vector3.Distance(this.transform.position, trans2.position);
        length = length1 + length2;
        UpdateLength();

        Vector3 force1 = (this.transform.position - trans1.position)*-1;
        force1 = findForce(end, end1, force1, length1);

        Vector3 force2 = (this.transform.position - trans2.position)*-1;
        force2 = findForce(end, end2, force2, length2);

        Vector3 force;
        if (force1.magnitude > force2.magnitude)force = force1;
        else force = force2;
        end.AddForce(force);

        //check to see if I hit something
        base.Update();
    }

    protected override bool Hits(int axis)
    {
        if(axis == 0)
        {
            return Physics.Raycast(this.transform.position, trans2.position - this.transform.position, out hit);
        }
        else if (axis == 1)
        {
            return Physics.Raycast(this.transform.position, trans2.position - this.transform.position, out hit);
        }
        else
        {
            return false; //TODO
        }
    }
}
