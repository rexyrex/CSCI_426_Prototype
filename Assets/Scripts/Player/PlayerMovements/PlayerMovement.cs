using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class PlayerMovement : MonoBehaviour {
	[SerializeField]
	float speed = 3.5f;

	[SerializeField]
	public Keys keys;
	public enum Keys { WASD, ARROWS };
	KeyCode up, down, left, right;

	void Start () {
		if (keys == Keys.WASD) {
			up = KeyCode.W;
			down = KeyCode.S;
			left = KeyCode.A;
			right = KeyCode.D;
		} else if (keys == Keys.ARROWS) {
			up = KeyCode.UpArrow;
			down = KeyCode.DownArrow;
			left = KeyCode.LeftArrow;
			right = KeyCode.RightArrow;
		}
	}


	void Update () {
		if (Input.GetKey (up)) {
			transform.Translate (Vector3.forward * speed * Time.deltaTime);
		}

		if (Input.GetKey (down)) {
			transform.Translate (Vector3.back * speed * Time.deltaTime);
		}

		if (Input.GetKey (left)) {
			transform.Translate (Vector3.left * speed * Time.deltaTime);
		}

		if (Input.GetKey (right)) {
			transform.Translate (Vector3.right * speed * Time.deltaTime);
		}
	}
}
