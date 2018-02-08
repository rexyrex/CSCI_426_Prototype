using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement2 : MonoBehaviour {

    NavMeshAgent playerAgent;
    float horMov = 0;
    float vertMov = 0;

    void Start()
    {
        playerAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void FixedUpdate () {

        horMov = 0;
        vertMov = 0;
        
        if (Input.GetKey(KeyCode.F))
        {
            horMov = -1;
            //Debug.Log("F");
        }

        if (Input.GetKey(KeyCode.G))
        {
            vertMov = -1;
        }

        if (Input.GetKey(KeyCode.T))
        {
            vertMov = 1;
        }

        if (Input.GetKey(KeyCode.H))
        {
            horMov = 1;
        }


        Vector3 movement = new Vector3(horMov, 0.0f, vertMov);

        playerAgent.destination = playerAgent.transform.position + movement;
    }
}
