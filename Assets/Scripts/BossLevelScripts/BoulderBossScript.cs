using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class BoulderBossScript : MonoBehaviour {
	float changeModeFreq = 20f;
	float changeModeLast;

	float spawnEnemyFreq = 10f;
	float spawnEnemyLast;

	float changeLocFreq = 10f;
	float changeLocLast;

	float maxhealth = 10000f;
	float health = 10000f;

	public Slider healthBar;

	private float lastHitTime;
	private float hitFreq = 1.2f;

	public Material closeMat;
	public Material medMat;
	public Material farMat;
	public Material invincibleMat;

	public GameObject closeEnemyObject;
	public GameObject mediumEnemyObject;
	public GameObject farEnemyObject;

	public GameObject manaObject;

	public enum bossMode {Close, Medium, Far, Invincible}

	public Transform[] boulderSpawnPoints;

	public Transform[] bossLocs;
	NavMeshAgent bossAgent;

	bossMode mode;
	public GameObject boulder;

	public void spawnNewBoulder(){
		int n = Random.Range (0, boulderSpawnPoints.Length);
		Vector3 pos = boulderSpawnPoints [n].position;
		pos.y += 4;
		Quaternion quat = new Quaternion(0, 0, 0, 0);
		Instantiate (boulder, pos, quat);
	}


	// Use this for initialization
	void Start () {
		changeModeLast = Time.time;
		lastHitTime = Time.time;
		spawnEnemyLast = Time.time;
		changeLocLast = Time.time;
		mode = bossMode.Invincible;
		updateMaterial ();
		bossAgent = GetComponent<NavMeshAgent> ();

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

		if (Time.time - changeLocLast > changeLocFreq) {
			changeLocLast = Time.time;
			int locInd = Random.Range (0, bossLocs.Length);
			bossAgent.destination = bossLocs [locInd].position;
		}

		if (Time.time - spawnEnemyLast > spawnEnemyFreq) {
			spawnEnemyLast = Time.time;
			Vector3 pos = gameObject.transform.position;
			Vector3 manapos = new Vector3 (pos.x, pos.y + 10, pos.x);
			Quaternion quat = new Quaternion(0, 0, 0, 0);
			int spawnType = Random.Range (0, 2);
			//spawnEnemyFreq = Random.Range (1, 10);
			GameObject inst;
			switch (spawnType) {
			case 0:
				inst = Instantiate(closeEnemyObject, pos, quat);
				//Instantiate(manaObject, manapos, quat);
				break;
			case 1:
				inst = Instantiate(mediumEnemyObject, pos, quat);
				//Instantiate(manaObject, manapos, quat);
				break;
			case 2:
				inst = Instantiate(farEnemyObject, pos, quat);
				//Instantiate(manaObject, manapos, quat);
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

	//public override void OnHitByChain(float damage, bool isChainActive)
	void OnCollisionEnter(Collision collision)
	{
		
		int rand = Random.Range (420,1700);
		if (GlobalDataController.gdc.chainCharged && collision.gameObject.tag == "BoulderTag") {
			switch (mode) {
			case bossMode.Close:
				if (GlobalDataController.gdc.boulderState == GlobalDataController.ChainDistance.Close) {
					getDamaged (rand);
					collision.gameObject.GetComponent<ColorBoulderScript> ().destroyBoulder (1);
					spawnNewBoulder ();
				}
				break;
			case bossMode.Medium:
				if (GlobalDataController.gdc.boulderState == GlobalDataController.ChainDistance.Medium) {
					getDamaged (rand);
					collision.gameObject.GetComponent<ColorBoulderScript> ().destroyBoulder (2);
					spawnNewBoulder ();
				}
				break;
			case bossMode.Far:
				if (GlobalDataController.gdc.boulderState == GlobalDataController.ChainDistance.Far) {
					getDamaged (rand);
					collision.gameObject.GetComponent<ColorBoulderScript> ().destroyBoulder (3);
					spawnNewBoulder ();
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
			//DamageTextController.CreateFloatingText (damage.ToString(), gameObject.transform);
			lastHitTime = Time.time;
		}
	}
}
