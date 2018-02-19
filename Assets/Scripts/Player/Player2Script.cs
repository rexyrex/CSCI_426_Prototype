using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Script : GenericPlayerScript
{
    protected override void Start()
    {
        base.Start();
        otherPlayer = GameObject.FindGameObjectWithTag("Player1Tag");
    }

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

        if (Input.GetButtonDown("Fire4"))
        {
            if(Vector3.Distance(otherPlayer.transform.position, this.transform.position) <= 5)
            {
                Vector3 push;
                Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit h;

                if (Physics.Raycast(r, out h, 500.0f, LayerMask.NameToLayer("Environment")))
                {
                    push = h.point;
                }
                else
                {
                    push = Vector3.up;
                }
                push = push - this.transform.position;
                otherPlayer.GetComponent<Rigidbody>().AddForce(push.normalized * 30, ForceMode.Impulse);
                otherPlayer.GetComponent<Rigidbody>().AddForce(Vector3.up * 20, ForceMode.Impulse);
            }
            
        }
    }

    public override void Damage(float value)
    {
        base.Damage(value);
        GlobalDataController.gdc.p2currentHealth -= value;

    }
}
