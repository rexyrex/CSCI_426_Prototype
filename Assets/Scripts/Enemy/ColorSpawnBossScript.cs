using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSpawnBossScript : BasicEnemyScript {
	float changeModeFreq = 10f;
	float changeModeLast;

	float maxhealth = 100f;
	float health = 100f;

	public Slider healthBar;

	private float lastHitTime;
	private float hitFreq = 0.02f;

	public Material closeMat;
	public Material medMat;
	public Material farMat;
	public Material invincibleMat;

	public enum bossMode {Close, Medium, Far, Invincible}

	bossMode mode;

	// Use this for initialization
	void Start () {
		changeModeLast = Time.time;
		lastHitTime = Time.time;
		mode = bossMode.Invincible;
		updateMaterial ();
	}
	
	// Update is called once per frame
	void Update () {
		healthBar.value = health / maxhealth;
		if (Time.time - changeModeLast > changeModeFreq) {
			changeModeLast = Time.time;
			if (mode == bossMode.Invincible) {
				int randIndex = Random.Range (0, 2);
				mode = (bossMode)randIndex;
			} else {
				mode = bossMode.Invincible;
			}
			updateMaterial ();
			Debug.Log ("mode changed to " + mode);
		}




	}

	void updateMaterial(){
		switch (mode) {
		case bossMode.Close:
			GetComponent<Renderer> ().material = closeMat;
			break;
		case bossMode.Medium:
			GetComponent<Renderer> ().material = medMat;
			break;
		case bossMode.Far:
			GetComponent<Renderer> ().material = farMat;
			break;
		default:
			GetComponent<Renderer> ().material = invincibleMat;
			break;
		}
	}

	public override void OnHitByChain(float damage, bool isChainActive)
	{
		switch (mode) {
		case bossMode.Close:
			if (GlobalDataController.gdc.chainState == GlobalDataController.ChainDistance.Close) {
				getDamaged (1);
			}
			break;
		case bossMode.Medium:
			if (GlobalDataController.gdc.chainState == GlobalDataController.ChainDistance.Medium) {
				getDamaged (1);
			}
			break;
		case bossMode.Far:
			if (GlobalDataController.gdc.chainState == GlobalDataController.ChainDistance.Far) {
				getDamaged (1);
			}
			break;
		default:
			GetComponent<Renderer> ().material = invincibleMat;
			break;
		}
	}

	void getDamaged(int damage){
		if(Time.time - lastHitTime > hitFreq)
		{
			//Debug.Log("Health is now: " + health);
			health -= damage;
			lastHitTime = Time.time;
		}
	}
}