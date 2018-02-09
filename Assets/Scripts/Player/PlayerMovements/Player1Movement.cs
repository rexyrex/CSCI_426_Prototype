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
        MoveInDirection(Input.GetAxis("Horizontal1"), 0, Input.GetAxis("Vertical1"));
    }
}
