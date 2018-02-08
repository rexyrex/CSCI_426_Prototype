using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[System.Serializable]
public class PlayerMovement : MonoBehaviour {
    [SerializeField]
    protected float speed = 4.5f;
    protected Rigidbody rb;

	protected virtual void Start () {
        rb = GetComponent<Rigidbody>();
	}


	protected virtual void Update () {
        
	}

    protected virtual void FixedUpdate () {
        
    }

    protected virtual void MoveInDirection(Vector3 direction) {
        rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
    }
}
