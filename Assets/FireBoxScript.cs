using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBoxScript : MonoBehaviour {
    public FireTrapScript trap;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        trap.AddBurn(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        trap.RemoveBurn(other.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        trap.AddBurn(collision.gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        trap.RemoveBurn(collision.gameObject);
    }
}
