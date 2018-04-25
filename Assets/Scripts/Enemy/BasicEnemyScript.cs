using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemySFX))]
public abstract class BasicEnemyScript : MonoBehaviour {
    protected float damageDone = 10;


    protected EnemySFX sfx;
    public abstract void OnHitByChain(float damage, bool isChainActive);


    protected virtual void Start()
    {
        sfx = GetComponent<EnemySFX>();
    }


    public virtual float Damage()
    {
        return damageDone;
    }

}


