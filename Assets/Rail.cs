using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rail : MonoBehaviour {
    public int axis;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /*
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "BoulderTag")
        {
            Minecart cart = other.GetComponent<Minecart>();
            if (cart == null) return;
            cart.AddRail(this);
        }    
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "BoulderTag")
        {
            Minecart cart = other.GetComponent<Minecart>();
            if (cart == null) return;
            cart.RemoveRail(this);
        }
    }*/
}
