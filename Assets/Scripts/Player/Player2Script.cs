﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Script : GenericPlayerScript
{
    // Update is called once per frame
    protected override void Update()
    {
        currentHealth = GlobalDataController.gdc.p2currentHealth;
        base.Update();
		if (GlobalDataController.gdc.p2currentHealth <= 0)
		{
			GlobalDataController.gdc.gameover = true;
			Destroy(gameObject);
		}
    }

    public override void Damage(float value)
    {
        base.Damage(value);
        GlobalDataController.gdc.p2currentHealth -= value;

    }
}
