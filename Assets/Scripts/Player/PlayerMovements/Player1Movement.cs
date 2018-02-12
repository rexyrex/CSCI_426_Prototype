﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Movement script for player 1.
/// </summary>
public class Player1Movement : PlayerMovement {
    public float pullIntensity;

    protected override void Start () {
        base.Start();
        if (speed < 0.0f)
            speed = 20.0f;
    }

    protected override void FixedUpdate() {
        // Turning the Player
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit h;
        if (Physics.Raycast(r, out h, 500.0f, LayerMask.NameToLayer("Environment")))
            TurnToward(h.point);

        //Moving the Player
        //Debug.Log(rb.velocity.x);
        float moveHorizontal = Input.GetAxis("Horizontal1");
        float moveVertical = Input.GetAxis("Vertical1");
        //Making Movement Feel Nice

        MoveInDirection(moveHorizontal, 0.0f, moveVertical, pullIntensity);

        if (Input.GetButtonDown("Jump1"))
        {
            Vector3 up = new Vector3(0.0f, 1.0f, 0.0f);
            rb.AddForce(up * jumpHeight, ForceMode.Impulse);
        }

        GlobalDataController.gdc.p1pos = this.transform.position;

    }
        
    protected override void Update()
    {
        base.Update();

        if (Mathf.Approximately(rb.velocity.y, 0))
        {
            if (!Input.GetButton("Jump1") || rb.velocity.y < 0)
            {
                rb.velocity += new Vector3(0.0f, 1.0f, 0.0f) * Physics.gravity.y * 0.02f * Time.deltaTime * 100;
            }
        }
    }
}
