using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicColorEnemyScript : MonoBehaviour {

	protected float lifeTimer = 20f;


	protected float damageDone = 10f;

	public abstract void OnHitByChain (float damage, bool isChainActive);

	public abstract void KillOff ();

	public abstract void Die();

	public abstract string getType ();

	public float Damage()
	{
		return damageDone;
	}
}
