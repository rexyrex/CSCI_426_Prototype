using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minecart : MonoBehaviour {
    Transform parentTrans;
    public float axis;
    public Rail startRail;
    Quaternion ZRotation;
    Quaternion XRotation;
    List<Rail> rails;
    Rigidbody rb;
    float trackMass;
    float derailMass;

	// Use this for initialization
	void Start () {
        XRotation = new Quaternion(0, 0, 0, 0);
        ZRotation = new Quaternion(0, 90, 0, 0);
        trackMass = 4;
        derailMass = 500;

        parentTrans = this.GetComponentInParent<Transform>();

        rb = this.GetComponent<Rigidbody>();

        rails = new List<Rail>();
        if (startRail != null) AddRail(startRail);

        SetConstraints();
    }
	
	// Update is called once per frame
	void Update () {
        if (rails.Count == 1) SetConstraints(); // Might need changing
        else return; // Don't Change anything
    }

    // Found a new rail
    public void AddRail(Rail item)
    {
        if (rails.Count < 1) rb.mass = trackMass;
        rails.Add(item);
    }

    // Moved off of a rail
    public void RemoveRail(Rail item)
    {
        rails.Remove(item);
        if (rails.Count < 1) rb.mass = derailMass;
    }

    // Sets constraints
    void SetConstraints()
    {
        axis = rails[0].axis;
        if (axis == 1)
        {
            FreezeX();
        }
        else if (axis == 0)
        {
            FreezeZ();
        }
    }

    // Let's the cart only move in Z
    void FreezeX()
    {
        rb.constraints = RigidbodyConstraints.None;
        rb.rotation = ZRotation;
        rb.freezeRotation = true;
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
    }

    // Let's the cart only move in X
    void FreezeZ()
    {
        rb.constraints = RigidbodyConstraints.None;
        rb.rotation = XRotation;
        rb.freezeRotation = true;
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;
    }

}
