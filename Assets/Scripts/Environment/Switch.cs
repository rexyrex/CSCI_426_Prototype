using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    //For a pressure plate
    public GameObject connectedThing;
    protected EnvironmentObject thing;
    protected Press plate;
    protected bool on;

    // Use this for initialization
    void Start()
    {
        thing = connectedThing.GetComponent<EnvironmentObject>();
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
                thing.Actuate();
                on = true;
            }

            else{
                thing.Revert();
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