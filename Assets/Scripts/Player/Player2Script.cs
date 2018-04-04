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

        if (pulling)
        {
            Vector3 pull = (otherPlayer.transform.position - this.transform.position).normalized;
            this.GetComponent<Rigidbody>().AddForce(pull * forcemod * 20);
        }

        // pull
        // Pull yourself toward the other player
        if (Input.GetButtonDown("Pull2"))
        {
            if (pulling && pullCounter > 1)
            {
                pulling = false;
            }
            else if (Vector3.Distance(otherPlayer.transform.position, this.transform.position) > 5)
            {
                pulling = true;
                pullCounter = 0;
                Vector3 pull = otherPlayer.transform.position - this.transform.position;
                pull = pull.normalized;
                this.GetComponent<Rigidbody>().AddForce(pull * forcemod, ForceMode.Impulse);
                this.GetComponent<Rigidbody>().AddForce(Vector3.up * forcemod, ForceMode.Impulse);
            }
        }

        // push
        // If the other player is pulling and is close enough, will throw them forward
        if (Input.GetButtonDown("Push2"))
        {
            if (otherPlayer.GetComponent<GenericPlayerScript>().IsPulling() && !pulling)
            {
                if (Vector3.Distance(otherPlayer.transform.position, this.transform.position) <= 5)
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
                    otherPlayer.GetComponent<Rigidbody>().AddForce(push.normalized * 4000, ForceMode.Impulse);
                    otherPlayer.GetComponent<Rigidbody>().AddForce(Vector3.up * 500, ForceMode.Impulse);
                }
            }
        }
    }

    public override void Damage(float value)
    {
        base.Damage(value);
        GlobalDataController.gdc.p2currentHealth -= value;

    }
}
