using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Script : GenericPlayerScript {
    protected override void Start()
    {
        base.Start();
        otherPlayer = GameObject.FindGameObjectWithTag("Player2Tag");
    }

	// Update is called once per frame
	protected override void Update () {
        currentHealth = GlobalDataController.gdc.p1currentHealth;
        base.Update();
		if (GlobalDataController.gdc.p1currentHealth <= 0)
		{
			GlobalDataController.gdc.gameover = true;
			Destroy(gameObject);
		}

        if (Input.GetButtonDown("Fire3"))
        {
            Vector3 pull = otherPlayer.transform.position - this.transform.position;
            this.GetComponent<Rigidbody>().AddForce(pull * 2, ForceMode.Impulse);
            this.GetComponent<Rigidbody>().AddForce(Vector3.up * 8, ForceMode.Impulse);
        }
    }

    public override void Damage(float value)
    {
        base.Damage(value);
        GlobalDataController.gdc.p1currentHealth -= value;

    }

}
