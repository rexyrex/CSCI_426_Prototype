﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Movement script for player 2.
/// </summary>
public class Player2Movement : PlayerMovement {
    protected chargeweapon weapon;
    protected Vector3 direction;

    protected override void Start() {
        base.Start();
        if (speed < 0.0f)
            speed = 15f;
        weapon = GameObject.FindGameObjectWithTag("ChargeWeapon").GetComponent<chargeweapon>();
    }

    protected override void FixedUpdate() {
        // Which point in the game world is the cursor pointing to?
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit h;

        if (Physics.Raycast(r, out h, 500.0f, LayerMask.NameToLayer("Environment")))
        {
            direction = h.point;
            TurnToward(direction);
        }

        MoveInDirection(Input.GetAxis("Horizontal2"), 0.0f, Input.GetAxis("Vertical2"));

        if (Input.GetButtonDown("Jump2"))
        {
            Vector3 up = new Vector3(0.0f, 1.0f, 0.0f);
            rb.AddForce(up * jumpHeight, ForceMode.Impulse);
        }

        if (Input.GetButtonDown("Fire2"))
        {
            Debug.Log("FIRE!");
            weapon.Fire(direction);
        }

        GlobalDataController.gdc.p2pos = transform.position;
    }

    protected override void Update()
    {
        base.Update();
        if (Mathf.Approximately(rb.velocity.y, 0))
        {
            if (!Input.GetButton("Jump2") || rb.velocity.y < 0)
            {
                rb.velocity += new Vector3(0.0f, 1.0f, 0.0f) * Physics.gravity.y * 0.02f * Time.deltaTime * 100;
            }
        }
    }
}
