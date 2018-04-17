using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    //For a pressure plate
    public EnvironmentObject[] connectedThings;
    //protected EnvironmentObject thing;
    protected Press plate;
    protected bool on;

    // Use this for initialization
    void Start()
    {
        //thing = connectedThings.GetComponent<EnvironmentObject>();
        plate = this.GetComponentInParent<Press>();
        on = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Heres");
        if (other.gameObject.tag == "Player2Tag" || other.gameObject.tag == "Player1Tag")
        {
            //Debug.Log("Activate");
            plate.Actuate();

            if (!on)
            {
                for(int i = 0; i < connectedThings.Length; i++) connectedThings[i].Switch();
                on = true;
            }

            else{
                for(int i = 0; i < connectedThings.Length; i++) connectedThings[i].Switch();
                on = false;
            }
            
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player2Tag" || other.gameObject.tag == "Player1Tag")
        {
            //Debug.Log("Deactivate");
            plate.Revert();
        }
    }
}