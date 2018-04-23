using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minecart : MonoBehaviour {
    Transform parentTrans;
    public float axis;
    Rigidbody rb;
    float trackMass;
    float derailMass;

    public CartTrigger trigEnd1;
    public CartTrigger trigEnd2;
    public CartTrigger trigMid;

    // Use this for initialization
    void Start () {
        trackMass = 4;
        derailMass = 500;

        parentTrans = this.GetComponentInParent<Transform>();

        rb = this.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        //if (rails.Count == 1 && change) SetConstraints(); // Might need changing
        //else return; // Don't Change anything
    }

    public void NewRail(int id, Rail rail)
    {
        if (id == 0) // Middle Trigger
        {
            if (rail.axis != axis) Turning();
        }
    }

    public void LeaveRail(int id, Rail rail)
    {
        // Checks if the front trigger is on a rail. If not, then the cart has gone over and it should stop.
        if (id == 0) // Middle Trigger
        {
            if (rb.velocity.x > 0)
            {
                if (trigEnd1.transform.position.x > trigEnd2.transform.position.x) //trigEnd1 is in front
                {
                    if (!trigEnd1.OnRail()) rb.velocity = Vector3.zero;
                }
            }
            else if(rb.velocity.x < 0)
            {
                if (trigEnd1.transform.position.x > trigEnd2.transform.position.x) //trigEnd2 is in front
                {
                    if (!trigEnd2.OnRail()) rb.velocity = Vector3.zero;
                }
            }
            else if (rb.velocity.z > 0)
            {
                if (trigEnd1.transform.position.z > trigEnd2.transform.position.z) //trigEnd1 is in front
                {
                    if (!trigEnd1.OnRail()) rb.velocity = Vector3.zero;
                }
            }
            else if (rb.velocity.z < 0)
            {
                if (trigEnd1.transform.position.z > trigEnd2.transform.position.z) //trigEnd2 is in front
                {
                    if (!trigEnd2.OnRail()) rb.velocity = Vector3.zero;
                }
            }
        }
    }

    void Turning()
    {

    }













    // Let's the cart only move in Z
    public void FreezeX()
    {
        rb.constraints = RigidbodyConstraints.None;
        Vector3 v = new Vector3(1, 0, 0);
        transform.rotation = Quaternion.FromToRotation(Vector3.right, v);
        rb.freezeRotation = true;
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
    }

    // Let's the cart only move in X
    public void FreezeZ()
    {
        rb.constraints = RigidbodyConstraints.None;
        Vector3 v = new Vector3(0, 0, 1);
        transform.rotation = Quaternion.FromToRotation(Vector3.forward, v);
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;
    }
}
