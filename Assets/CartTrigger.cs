using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartTrigger : MonoBehaviour {
    public Minecart cart;
    public int id;
    public Rail startRail;
    List<Rail> rails;

    // Use this for initialization
    void Start () {
        rails = new List<Rail>();
        if (startRail != null) rails.Add(startRail);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Rail" && !rails.Contains(other.GetComponent<Rail>()))
        {
            rails.Add(other.GetComponent<Rail>());
            cart.NewRail(id, other.GetComponent<Rail>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Rail" && rails.Contains(other.GetComponent<Rail>()))
        {
            rails.Remove(other.GetComponent<Rail>());
        }
    }

    public bool OnThisRail(Rail rail)
    {
        if (rails.Contains(rail)) return true;
        else return false;
    }

    public bool OnRail()
    {
        if (rails.Count == 0) return false;
        else return true;
    }
}
