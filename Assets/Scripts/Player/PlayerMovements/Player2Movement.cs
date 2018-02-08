using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Movement : PlayerMovement {

	// Use this for initialization
	public override void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update ();

		if (Input.GetKey (KeyCode.UpArrow)) {
			transform.Translate (Vector3.forward * speed * Time.deltaTime);
		}

		if (Input.GetKey (KeyCode.DownArrow)) {
			transform.Translate (Vector3.back * speed * Time.deltaTime);
		}

		if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.Translate (Vector3.left * speed * Time.deltaTime);
		}

		if (Input.GetKey (KeyCode.RightArrow)) {
			transform.Translate (Vector3.right * speed * Time.deltaTime);
		}
	}
}
