using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicColorEnemyScript : BasicEnemyScript {

	protected float lifeTimer = 20f;

    public GameObject explosion;

    protected override void Start()
    {
        base.Start();

        if (sfx != null)
        {
            Debug.LogErrorFormat("SFX object was not initialized for enemy {0}", gameObject.name);
            sfx.PlaySpawn();
        }
    }

	public abstract void KillOff ();


	public abstract string getType ();

    public override float Damage()
    {
        return damageDone;
    }

    public void Die()
    {
        Vector3 pos = gameObject.transform.position;
        Quaternion quat = new Quaternion(0, 0, 0, 0);
        Instantiate(explosion, pos, quat);
        pos.y += 2;

        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<Renderer>().enabled = false;

        float destroyIn = 0.0f;
        if (sfx != null)
        {
            sfx.PlayPop();
            destroyIn = sfx.PopDelay;
        }

        Destroy(gameObject, destroyIn);
    }
}
