using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Movement : PlayerMovement {
    protected override void Start() {
        base.Start();

        if (speed < 0.0f)
            speed = 4.0f;
    }

    protected override void FixedUpdate() {
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit h;

        if (Physics.Raycast(r, out h, 500.0f, LayerMask.NameToLayer("Environment")))
            TurnToward(h.point);
        
        MoveInDirection(Input.GetAxis("Horizontal2"), 0, Input.GetAxis("Vertical2"));
    }
}
