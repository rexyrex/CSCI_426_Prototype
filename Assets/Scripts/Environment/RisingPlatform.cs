using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingPlatform : MovingPlatform {
    public bool up;

	// Use this for initialization
	protected override void Start () {
        base.Start();
        int direction = 1;
        if (!up) direction = -1;
        movement = new Vector3(0, distance*direction, 0);
        end = this.transform.position + movement;
    }
}
