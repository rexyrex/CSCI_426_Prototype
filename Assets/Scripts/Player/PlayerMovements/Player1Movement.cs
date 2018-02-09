using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Movement script for player 1.
/// </summary>
public class Player1Movement : PlayerMovement {
    [SerializeField]
    protected Transform Player2;

    protected override void Start () {
        base.Start();
        if (speed < -0.0f)
            speed = 7.5f;
    }

    protected override void FixedUpdate() {
        // Which point in the game world is the cursor pointing to?
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit h;

        if (Physics.Raycast(r, out h, 500.0f, LayerMask.NameToLayer("Environment")))
            TurnToward(h.point);

        MoveInDirection(Input.GetAxis("Horizontal1"), 0, Input.GetAxis("Vertical1"));
    }
}
