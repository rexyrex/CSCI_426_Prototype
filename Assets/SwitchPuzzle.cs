using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPuzzle : EnvironmentObject {

    //For a pressure plate
    public EnvironmentObject[] connectedThings;
    int last;

    // Use this for initialization
    void Start()
    {
        for(int i = 0; i < connectedThings.Length; i++)
        {
            connectedThings[i].Actuate();
        }
        last = Random.Range(0, connectedThings.Length);
        connectedThings[last].Revert();
    }

    public override void Switch()
    {
        int newSwitch = last;
        while(newSwitch == last)
        {
            newSwitch = Random.Range(0, connectedThings.Length);
        }
        last = newSwitch;
        connectedThings[last].Revert();
    }
}
