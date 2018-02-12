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
        //Making Movement Feel Nicer
        if(moveHorizontal > 0)
        {
            if (rb.velocity.x <= maxSpeed)
            {
                moveHorizontal *= acceleration;
            }
            else
            {
                moveHorizontal = acceleration*moveHorizontal / rb.velocity.x;
            }
        }
        if (moveHorizontal < 0)
        {
            if (rb.velocity.x >= -1* maxSpeed)
            {
                moveHorizontal *= acceleration;
            }
            else
            {
                moveHorizontal = -1*acceleration * moveHorizontal / rb.velocity.x;
            }
        }

        if (moveVertical > 0)
        {
            if (rb.velocity.z <= maxSpeed)
            {
                moveVertical *= acceleration;
            }
            else
            {
                moveVertical = acceleration * moveVertical / rb.velocity.z;
            }
        }
        if (moveVertical < 0)
        {
            if (rb.velocity.z >= -1*maxSpeed)
            {
                moveVertical *= acceleration;
            }
            else
            {
                moveVertical = -1* acceleration * moveVertical / rb.velocity.z;
            }
        }

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        if (GlobalDataController.gdc.tetherPull)
        {
            movement.x += (GlobalDataController.gdc.p2pos.x - GlobalDataController.gdc.p1pos.x) / 100 * pullIntensity;
            movement.y += (GlobalDataController.gdc.p2pos.y - GlobalDataController.gdc.p1pos.y) / 100 * pullIntensity;
            movement.z += (GlobalDataController.gdc.p2pos.z - GlobalDataController.gdc.p1pos.z) / 100 * pullIntensity;
        }
        
        rb.AddForce(speed * movement);

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
        if (rb.velocity.y != 0)
        {
            if (!Input.GetButton("Jump1") || rb.velocity.y<0)
            {
                rb.velocity += new Vector3(0.0f, 1.0f, 0.0f) * Physics.gravity.y * 0.02f * Time.deltaTime * 100;
            }
        }
    }
}
