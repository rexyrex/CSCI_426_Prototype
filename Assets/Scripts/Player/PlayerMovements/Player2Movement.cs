using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

/// <summary>
/// Movement script for player 2.
/// </summary>
public class Player2Movement : PlayerMovement {
    protected chargeweapon weapon;
    protected Vector3 direction;

    protected override void Awake()
    {
        if (playerId < 0) playerId = 1;
        base.Awake();
    }

    protected override void Start() {
        base.Start();
        if (speed < 0.0f)
            speed = 15f;
        weapon = GameObject.FindGameObjectWithTag("ChargeWeapon").GetComponent<chargeweapon>();
    }

    protected override void FixedUpdate() {
        base.FixedUpdate();

        GlobalDataController.gdc.p2pos = this.transform.position;
    }

    protected override void Update()
    {
        base.Update();
    }
}
