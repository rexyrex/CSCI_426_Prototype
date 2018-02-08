using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Movement : PlayerMovement {
    protected override void Start () {
        base.Start();
        if (speed < -0.0f)
            speed = 7.5f;
    }

    protected override void FixedUpdate() {
        if (Input.GetKey(KeyCode.W))
            MoveInDirection(Vector3.forward);

        if (Input.GetKey(KeyCode.S))
            MoveInDirection(Vector3.back);

        if (Input.GetKey(KeyCode.A))
            MoveInDirection(Vector3.left);

        if (Input.GetKey(KeyCode.D))
            MoveInDirection(Vector3.right);
    }
}
