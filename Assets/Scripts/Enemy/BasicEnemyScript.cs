using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicEnemyScript : MonoBehaviour {
    protected float damageDone = 10;

	public abstract void OnHitByChain (float damage, bool isChainActive);


    public float Damage()
    {
        return damageDone;
    }

}


