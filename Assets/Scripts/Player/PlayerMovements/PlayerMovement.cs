using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour {
    NavMeshAgent playerAgent;

    void Start () {
        playerAgent = GetComponent<NavMeshAgent>();
    }
	

	void Update () {
		
	}

    void FixedUpdate()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        //if (!playerAgent.pathPending) {
        //if (!isJumping) {
        //if (!GlobalPlayerData.gpd.isPaused())
        //{

            playerAgent.destination = playerAgent.transform.position + movement;
        //}
        //}
        //}
        //rb.MovePosition (rb.position + speed*movement * Time.deltaTime);

        //rb.AddForce (movement * speed);

        /*if (Input.GetKeyDown (KeyCode.Space)) {

		} else {
			isJumping = false;
			playerAgent.enabled = true;
		}*/


    }
}
