using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Movement : PlayerMovement {
    protected override void Start() {
        base.Start();

        speed = 7.5f;
    }

    protected override void FixedUpdate() {
        if (Input.GetKey(KeyCode.UpArrow))
            MoveInDirection(Vector3.forward);

        if (Input.GetKey(KeyCode.DownArrow))
            MoveInDirection(Vector3.back);

        if (Input.GetKey(KeyCode.LeftArrow))
            MoveInDirection(Vector3.left);

        if (Input.GetKey(KeyCode.RightArrow))
            MoveInDirection(Vector3.right);
    }
}
