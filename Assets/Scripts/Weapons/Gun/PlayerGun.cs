using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : Gun {
	
	// Update is called once per frame
	protected override void Update () {
        if (Input.GetButtonDown("Fire1")) Fire();
	}
}
