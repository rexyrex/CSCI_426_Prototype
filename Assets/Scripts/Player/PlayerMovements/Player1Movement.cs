using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

/// <summary>
/// Movement script for player 1.
/// </summary>
public class Player1Movement : PlayerMovement {
    public float pullIntensity;

    protected override void Awake()
    {
        if (playerId < 0) playerId = 0;
        base.Awake();
    }

    protected override void Start () {
        base.Start();
        if (speed < 0.0f)
            speed = 20.0f;
    }

    protected override void FixedUpdate() {
        base.FixedUpdate();

        GlobalDataController.gdc.p1pos = this.transform.position;
    }
        
    protected override void Update()
    {
        base.Update();
    }
}
