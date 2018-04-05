using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Script : GenericPlayerScript {
    bool spinning;
    float spinRadius;
    float spinCounter;

    protected override void Start()
    {
        base.Start();
        otherPlayer = GameObject.FindGameObjectWithTag("Player2Tag");
        spinning = false;
        spinRadius = 0;
        spinCounter = 0;
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

        // Checks whether to continue spinning the character
        if (spinning)
        {
            spinCounter += Time.deltaTime;
            Spin();
        }

        if (pulling)
        {
            Vector3 pull = (otherPlayer.transform.position - this.transform.position).normalized;
            this.GetComponent<Rigidbody>().AddForce(pull * forcemod * 20);
        }

        // pull
        // Pull yourself toward the other player
        if (player.GetButtonDown("pull"))
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
                this.GetComponent<Rigidbody>().AddForce(pull*forcemod, ForceMode.Impulse);
                this.GetComponent<Rigidbody>().AddForce(Vector3.up*forcemod, ForceMode.Impulse);
            }
        }

        // push
        // If the other player is pulling, you will get swung around them
        if(player.GetButtonDown("push"))
        {
            if (/*otherPlayer.GetComponent<GenericPlayerScript>().IsPulling() &&*/ !pulling)
            {
                Debug.Log("Spinning");
                spinning = true;
                spinCounter = 0;
                spinRadius = Vector3.Distance(otherPlayer.transform.position, this.transform.position);
                Vector3 centripetal = otherPlayer.transform.position - this.transform.position;
                centripetal.y = 0;
                centripetal = centripetal.normalized;
                
                Vector3 angular = new Vector3(centripetal.z, 0, centripetal.x*-1);
                angular = angular.normalized;
                
                this.GetComponent<Rigidbody>().AddForce(angular*forcemod, ForceMode.Impulse);
                centripetal *= Mathf.Pow(this.GetComponent<Rigidbody>().velocity.magnitude, 2) / spinRadius;
                this.GetComponent<Rigidbody>().AddForce(centripetal*forcemod, ForceMode.Impulse);

                otherPlayer.GetComponent<GenericPlayerScript>().Pulled();
            }
        }
    }

    public override void Damage(float value)
    {
        base.Damage(value);
        GlobalDataController.gdc.p1currentHealth -= value;

    }

    void Spin()
    {
        if (spinCounter >= 1 || Vector3.Distance(this.transform.position, otherPlayer.transform.position) <2.5)
        {
            spinCounter = 0;
            spinning = false;

            Vector3 centripetal = otherPlayer.transform.position - this.transform.position;
            centripetal.y = 0;
            centripetal = centripetal.normalized;

            Vector3 angular = this.GetComponent<Rigidbody>().velocity * -1;
            this.GetComponent<Rigidbody>().AddForce(angular * forcemod * 0.5f, ForceMode.Impulse);

            return;
        }
        else
        {
            Vector3 centripetal = otherPlayer.transform.position - this.transform.position;
            centripetal = centripetal.normalized;

            Vector3 angular = new Vector3(centripetal.z, 0, centripetal.x * -1);
            angular = angular.normalized;

            this.GetComponent<Rigidbody>().AddForce(angular * forcemod * 50);
            centripetal *= Mathf.Pow(this.GetComponent<Rigidbody>().velocity.magnitude, 2) / spinRadius;
            this.GetComponent<Rigidbody>().AddForce(centripetal * forcemod * 0.4f);
        }

    }
}
