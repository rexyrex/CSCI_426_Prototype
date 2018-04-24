using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinecartRotator : MonoBehaviour {
    public Minecart cart;
    public Rail startRail;
    public GameObject trigEnd1;
    public GameObject trigEnd2;
    public GameObject trigMid;
    int axis;
    List<Rail> rails;

    // Use this for initialization
    void Start () {
        rails = new List<Rail>();
        if (startRail != null) AddRail(startRail);
    }
	
	// Update is called once per frame
	void Update () {

	}

    // Hit a Rail
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Rail")
        {
            AddRail(other.GetComponent<Rail>());
        }
    }

    // Hit a Rail
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Rail")
        {
            RemoveRail(other.GetComponent<Rail>());
        }
    }

    // Found a new rail
    public void AddRail(Rail item)
    {
        rails.Add(item);
    }

    // Moved off of a rail
    public void RemoveRail(Rail item)
    {
        rails.Remove(item);
    }

    // Sets constraints
    void SetConstraints()
    {
        axis = rails[0].axis;
        if (axis == 1)
        {
            cart.FreezeX();
        }
        else if (axis == 0)
        {
            cart.FreezeZ();
        }
    }
}
