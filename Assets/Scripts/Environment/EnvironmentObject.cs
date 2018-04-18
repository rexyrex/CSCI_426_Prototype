using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentObject : MonoBehaviour {
    public bool isActuated;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual void Actuate()
    {
        isActuated = true;
    }

    public virtual void Revert()
    {
        isActuated = false;
    }

    public virtual void Switch()
    {
        if (isActuated) Revert();
        else Actuate();
    }
}
