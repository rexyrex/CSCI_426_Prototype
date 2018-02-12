using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeChangeEnemyScript : BasicEnemyScript {

    private float fullHealth = 50;
    private float health = 50;

    private float timeTillRegenStart = 3f;
    private float regenSpeed = 1f;
    private float regenAmount = 10f;

    private float lastHitTime;
    private float hitFreq = 0.02f;

	// Use this for initialization
	void Start () {
        lastHitTime = Time.time;
        gameObject.transform.localScale = new Vector3(health / fullHealth + 0.5f, health / fullHealth + 0.5f, health / fullHealth + 0.5f);
    }
	
	// Update is called once per frame
	void Update () {
        //Size of enemy based on percentage of health
        //gameObject.transform.localScale.Set(health / fullHealth, health / fullHealth, health / fullHealth);
        //gameObject.transform.localScale = new Vector
        if (health <= 0)
        {
            Destroy(gameObject);
        }
	}

	public override void OnHitByChain(float damage, bool isChainActive)
    {
        //Debug.Log("HIT " + (Time.time - lastHitTime));
        if(Time.time - lastHitTime > hitFreq)
        {
            //Debug.Log("Health is now: " + health);
            health -= damage;
            lastHitTime = Time.time;
            gameObject.transform.localScale = new Vector3(health / fullHealth+0.5f, health / fullHealth + 0.5f, health / fullHealth + 0.5f);
        }
        
    }
}
