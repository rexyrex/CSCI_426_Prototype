using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Movement script for player 1.
/// </summary>
public class Player1Movement : PlayerMovement {
    protected override void Start () {
        base.Start();
        if (speed < 0.0f)
            speed = 200.0f;
    }

    protected override void FixedUpdate() {
        // Which point in the game world is the cursor pointing to?
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit h;

        if (Physics.Raycast(r, out h, 500.0f, LayerMask.NameToLayer("Environment")))
            TurnToward(h.point);

        MoveInDirection(Input.GetAxis("Horizontal1"), 0, Input.GetAxis("Vertical1"));


        if (Input.GetButtonDown("Jump1"))
        {
            
            Vector3 up = new Vector3(0.0f, 1.0f, 0.0f);
            //rb.velocity = up*100;
            rb.AddForce(up * 10, ForceMode.Impulse);
        }
    }
        
    protected override void Update()
    {
        base.Update();
        if (rb.velocity.y < 0)
        {
            Debug.Log("fall 1");
            //Vector3 down = new Vector3(0.0f, -1.0f, 0.0f);
            //rb.AddForce(down * rb.velocity.y*-2000);
            rb.velocity += new Vector3(0.0f, 1.0f, 0.0f) * Physics.gravity.y * 1500f * Time.deltaTime*100;
        }
        else if (!Input.GetButton("Jump1"))
        {
            Debug.Log("fall 2");
            rb.velocity += new Vector3(0.0f, 1.0f, 0.0f) * Physics.gravity.y * 1.5f * Time.deltaTime*100;
        }
    }
}
