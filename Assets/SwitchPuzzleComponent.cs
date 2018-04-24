using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPuzzleComponent : EnvironmentObject {
    //For a pressure plate
    public EnvironmentObject[] connectedThings;
    protected Press plate;
    protected bool on;
    public Color onColor;
    public Color offColor;

    // Use this for initialization
    void Start()
    {
        plate = this.GetComponentInParent<Press>();
        on = false;
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (!on) return;
        if (other.gameObject.tag == "Player2Tag" || other.gameObject.tag == "Player1Tag")
        {
            this.Actuate();
            for (int i = 0; i < connectedThings.Length; i++) connectedThings[i].Switch();
        }
    }

    // Can't use anymore
    public override void Actuate()
    {
        plate.gameObject.GetComponent<Renderer>().material.color = onColor;
        plate.Actuate();
        on = false;
    }

    // Can use again
    public override void Revert()
    {
        plate.gameObject.GetComponent<Renderer>().material.color = offColor;
        plate.Revert();
        on = true;
    }
}