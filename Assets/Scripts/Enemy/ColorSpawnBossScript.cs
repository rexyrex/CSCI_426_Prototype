using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSpawnBossScript : BasicEnemyScript {
	float changeModeFreq = 10f;
	float changeModeLast;

	float spawnEnemyFreq = 10f;
	float spawnEnemyLast;

	float maxhealth = 10000f;
	float health = 10000f;

	public Slider healthBar;

	private float lastHitTime;
	private float hitFreq = 0.4f;

	public Material closeMat;
	public Material medMat;
	public Material farMat;
	public Material invincibleMat;

	public GameObject closeEnemyObject;
	public GameObject mediumEnemyObject;
	public GameObject farEnemyObject;

	public GameObject manaObject;

	public enum bossMode {Close, Medium, Far, Invincible}

	bossMode mode;

	// Use this for initialization
	void Start () {
		changeModeLast = Time.time;
		lastHitTime = Time.time;
		spawnEnemyLast = Time.time;
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

		if (Time.time - spawnEnemyLast > spawnEnemyFreq) {
			spawnEnemyLast = Time.time;
			Vector3 pos = gameObject.transform.position;
			Vector3 manapos = new Vector3 (pos.x, pos.y + 10, pos.x);
			Quaternion quat = new Quaternion(0, 0, 0, 0);
			int spawnType = Random.Range (0, 2);
			spawnEnemyFreq = Random.Range (1, 10);
			GameObject inst;
			switch (spawnType) {
			case 0:
				inst = Instantiate(closeEnemyObject, pos, quat);
				Instantiate(manaObject, manapos, quat);
				break;
			case 1:
				inst = Instantiate(mediumEnemyObject, pos, quat);
				Instantiate(manaObject, manapos, quat);
				break;
			case 2:
				inst = Instantiate(farEnemyObject, pos, quat);
				Instantiate(manaObject, manapos, quat);
				break;
			default:
				break;
			}


		}

		if (health < 0) {
			GlobalDataController.gdc.gameover = true;
			Destroy (gameObject);
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
		int rand = Random.Range (100,700);
		if (isChainActive) {
			switch (mode) {
			case bossMode.Close:
				if (GlobalDataController.gdc.chainState == GlobalDataController.ChainDistance.Close) {
					getDamaged (rand);
				}
				break;
			case bossMode.Medium:
				if (GlobalDataController.gdc.chainState == GlobalDataController.ChainDistance.Medium) {
					getDamaged (rand);
				}
				break;
			case bossMode.Far:
				if (GlobalDataController.gdc.chainState == GlobalDataController.ChainDistance.Far) {
					getDamaged (rand);
				}
				break;
			default:
				GetComponent<Renderer> ().material = invincibleMat;
				break;
			}
		}
	}

	void getDamaged(int damage){
		if(Time.time - lastHitTime > hitFreq)
		{
			
			//Debug.Log("Health is now: " + health);
			health -= damage;
			DamageTextController.CreateFloatingText (damage.ToString(), gameObject.transform);
			lastHitTime = Time.time;
		}
	}
}