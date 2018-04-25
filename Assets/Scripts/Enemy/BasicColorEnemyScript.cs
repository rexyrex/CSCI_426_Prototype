using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicColorEnemyScript : BasicEnemyScript {

	protected float lifeTimer = 20f;

    public GameObject explosion;

    protected override void Start()
    {
        base.Start();

        sfx.PlaySpawn();
    }

	public abstract void KillOff ();

    public virtual void Die() {
        Vector3 pos = gameObject.transform.position;
        Quaternion quat = new Quaternion(0, 0, 0, 0);
        Instantiate(explosion, pos, quat);
        pos.y += 2;

        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<Renderer>().enabled = false;

        sfx.PlayPop();

        print(sfx.PopDelay);
        Destroy(gameObject, sfx.PopDelay);
    }

	public abstract string getType ();

	public override float Damage()
	{
		return damageDone;
	}
}
