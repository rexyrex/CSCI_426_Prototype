using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class BoulderBossScript : MonoBehaviour {
	float changeColorModeFreq = 20f;
	float changeColorModeLast;
	Vector3 dashDest;
	bool lastModeWasColor;

	float chaseDuration = 7f;
	float chaseLastTime;

	float changeDashDestFreq = 2.5f;
	float changeDashDestLast;

	GameObject player1Obj;
	GameObject player2Obj;

	public GameObject directionalLight;
	public GameObject spotLight;


	float spawnEnemyFreq = 6f;
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


	public Material lavaMat;

	public GameObject closeEnemyObject;
	public GameObject mediumEnemyObject;
	public GameObject farEnemyObject;

	public GameObject manaObject;

	public enum bossMode {Close, Medium, Far, Invincible,Chase}

	public Transform[] boulderSpawnPoints;

	public Transform[] bossLocs;
	NavMeshAgent bossAgent;

	bossMode colorMode;
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
		changeColorModeLast = Time.time;
		changeDashDestLast = Time.time;
		chaseLastTime = Time.time;
		lastHitTime = Time.time;
		spawnEnemyLast = Time.time;
		changeLocLast = Time.time;
		colorMode = bossMode.Invincible;
		lastModeWasColor = true;
		updateMaterial ();
		bossAgent = GetComponent<NavMeshAgent> ();
		spotLight.GetComponent<Light> ().intensity = 0;
		player1Obj = GameObject.FindGameObjectWithTag ("Player1Tag");
		player2Obj = GameObject.FindGameObjectWithTag ("Player2Tag");


	}

	// Update is called once per frame
	void Update () {
		healthBar.value = health / maxhealth;
		if (Time.time - changeColorModeLast > changeColorModeFreq) {
			changeColorModeLast = Time.time;
			Debug.Log ("Attempting to change mode. CD on chase is " + (Time.time - chaseLastTime) + "bool = " + lastModeWasColor);
			if (lastModeWasColor == true && Time.time - chaseLastTime > chaseDuration) {
				chaseLastTime = Time.time;
				colorMode = bossMode.Chase;
				directionalLight.GetComponent<Light> ().intensity = 0.6f;
				spotLight.GetComponent<Light> ().intensity = 70f;
				lastModeWasColor = false;
				Debug.Log ("chase start");
			} else if (true || colorMode == bossMode.Invincible) { // this always triggers (temporary)
				directionalLight.GetComponent<Light> ().intensity = 1f;
				spotLight.GetComponent<Light> ().intensity = 0f;
				int randIndex = Random.Range (0, 2);
				colorMode = (bossMode)randIndex;
				lastModeWasColor = true;
			} else {
				colorMode = bossMode.Invincible;
				lastModeWasColor = true;
			}
			updateMaterial ();
			Debug.Log ("mode changed to " + colorMode);
		}


		if (lastModeWasColor == false) {
			bossAgent.enabled = false;
		} else {
			bossAgent.enabled = true;
		}

		if (Time.time - changeDashDestLast > changeDashDestFreq && bossAgent.enabled == false) {
			changeDashDestLast = Time.time;
			int playerInd = Random.Range (1, 2);
			Vector3 spotTrans;
			if (playerInd == 1) {
				dashDest = player1Obj.transform.position;
				spotTrans = player1Obj.transform.position;
				spotTrans.y += 2;
				spotLight.transform.position = spotTrans;
			} else {
				dashDest = player2Obj.transform.position;
				spotTrans = player2Obj.transform.position;
				spotTrans.y += 2;
				spotLight.transform.position = spotTrans;
			}


		}


		if (Time.time - changeLocLast > changeLocFreq && bossAgent.enabled == true) {
			changeLocLast = Time.time;
			int locInd = Random.Range (0, bossLocs.Length);
			bossAgent.destination = bossLocs [locInd].position;
		}

		if (bossAgent.enabled == false) {
			float step = 17 * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, dashDest, step);
		}


		if (Time.time - spawnEnemyLast > spawnEnemyFreq && colorMode==bossMode.Invincible) {
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
		switch (colorMode) {
		case bossMode.Close:
			GetComponent<Renderer> ().material = closeMat;
			break;
		case bossMode.Medium:
			GetComponent<Renderer> ().material = medMat;
			break;
		case bossMode.Far:
			GetComponent<Renderer> ().material = farMat;
			break;
		case bossMode.Chase:
			GetComponent<Renderer> ().material = lavaMat;
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
			switch (colorMode) {
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
