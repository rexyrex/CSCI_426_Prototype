﻿using UnityEngine;

/// <summary>
/// Movement script for player 2.
/// </summary>
public class Player2Movement : PlayerMovement
{
    protected override void Awake()
    {
        if (playerId < 0) playerId = 1;
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        if (speed < 0.0f)
            speed = 15f;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        GlobalDataController.gdc.p2pos = transform.position;
    }

    protected override void Update()
    {
        base.Update();
    }
}
