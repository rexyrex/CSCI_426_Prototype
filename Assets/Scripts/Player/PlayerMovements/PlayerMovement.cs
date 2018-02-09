using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[System.Serializable]
public class PlayerMovement : MonoBehaviour {
    [SerializeField]
    protected float speed = -1.0f;
    protected Rigidbody rb;

	protected virtual void Start () {
        rb = GetComponent<Rigidbody>();
	}


	protected virtual void Update () {
        
	}

    protected virtual void FixedUpdate () {
        
    }

    protected virtual void MoveInDirection(float x, float y, float z) {
        MoveInDirection(new Vector3(x, y, z));
    }

    protected virtual void MoveInDirection(Vector3 direction) {
        if (speed < 0.0f)
            throw new System.ArgumentNullException("speed has not been set to " +
                                                   "a value in movement script");

        var pos = transform.position + direction * (float)speed * Time.deltaTime;
        rb.MovePosition(pos);
    }
}
