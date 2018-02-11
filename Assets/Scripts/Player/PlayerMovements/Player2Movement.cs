using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Movement script for player 2.
/// </summary>
public class Player2Movement : PlayerMovement {
    protected override void Start() {
        base.Start();
        if (speed < 0.0f)
            speed = 150.0f;
    }

    protected override void FixedUpdate() {
        // Which point in the game world is the cursor pointing to?
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit h;

        if (Physics.Raycast(r, out h, 500.0f, LayerMask.NameToLayer("Environment")))
            TurnToward(h.point);

        float moveHorizontal = Input.GetAxis("Horizontal2");
        float moveVertical = Input.GetAxis("Vertical2");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(speed * movement);

        if (Input.GetButtonDown("Jump2"))
        {
            Vector3 up = new Vector3(0.0f, 1.0f, 0.0f);
            rb.AddForce(up * 5, ForceMode.Impulse);
        }
    }

    protected override void Update()
    {
        base.Update();
        if (rb.velocity.y != 0)
        {
            if (!Input.GetButton("Jump2") || rb.velocity.y < 0)
            {
                rb.velocity += new Vector3(0.0f, 1.0f, 0.0f) * Physics.gravity.y * 0.015f * Time.deltaTime * 100;
            }
        }
    }
}
