using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[System.Serializable]
public class PlayerMovement : MonoBehaviour {
    [SerializeField]
    protected float speed = -1.0f;
    protected Rigidbody rb;
    [SerializeField]
    protected float rotationSpeedScale = 20.0f;

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

    protected void MoveInDirection(Vector3 direction) {
        if (speed < 0.0f)
            throw new System.ArgumentNullException("speed has not been set to " +
                                                   "a value in movement script");

        var pos = transform.position + direction * (float)speed * Time.deltaTime;
        rb.MovePosition(pos);
    }

    protected void TurnToward(Vector3 target) {
        var targetDirection = target - transform.position;
        var forward = transform.forward;
        var localTarget = transform.InverseTransformPoint(target);

        var angle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;

        var eulerAngleVelocity = rotationSpeedScale * new Vector3(0, angle, 0);
        var deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }
}
