using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDataController : MonoBehaviour {



    public static GlobalDataController gdc;

	public bool gameover;

    private GameObject player1Reference;
    private GameObject player2Reference;

	public GameObject gameOverCanvas;

	public enum ChainDistance {Close, Medium, Far};
	public bool chainCharged;

    //Player Health
    public float p1maxHealth;
    public float p1currentHealth;
    private float p1defaultHealth = 100f;
    public float p2maxHealth;
    public float p2currentHealth;
    private float p2defaultHealth = 100f;

    //Mana
    public float maxMana;
    public float currentMana;
    private float defaultMana = 40f;

    //Tether Info
    public bool tooFar;
    public float decayRate;
    public bool tetherPull;
	public ChainDistance chainState;

	//boulder state
	public ChainDistance boulderState;
	public ChainDistance lastBoulderState;

    //Player Positions
    public Vector3 p1pos;
    public Vector3 p2pos;

	//Timers
	float timeInitialized;
	public float timeLimit = 120f;
	public float timeLeft;

	//Counter
	public int enemyCount;

    void Awake()
    {
        if (gdc == null)
        {
            DontDestroyOnLoad(gameObject);
            gdc = this;
            setUpStats();
        }
        else if (gdc != this)
        {
            Destroy(gameObject);
        }
    }

    void setUpStats()
    {
		
		timeLeft = timeLimit;
		timeInitialized = Time.time;
		gameover = false;
        UpdatePlayerStats();
        p1currentHealth = p1defaultHealth;
        p2currentHealth = p2defaultHealth;
        currentMana = defaultMana;
        tooFar = false;
		chainState = ChainDistance.Close;
		boulderState = ChainDistance.Close;
		lastBoulderState = ChainDistance.Close;
    }

    public void UpdatePlayerStats()
    {

    }

    public static void TetherBreak()
    {
        gdc.currentMana = 0;
        gdc.p1currentHealth -= gdc.p1maxHealth * gdc.decayRate * Time.deltaTime;
        gdc.p2currentHealth -= gdc.p2maxHealth * gdc.decayRate * Time.deltaTime;
    }

    void Start () {
        player1Reference = GameObject.FindGameObjectWithTag("Player1Tag");
        player2Reference = GameObject.FindGameObjectWithTag("Player2Tag");
		setUpStats ();

    }
	

	void Update () {
		if (currentMana > 100) {
			currentMana = 100;
		}

		if (gameover) {
			gameOverCanvas.SetActive (true);
		}
		if (!gameover) {
			timeLeft = timeLimit - (Time.time - timeInitialized);
		}
		if (timeLeft < 0) {
			gameover = true;
		}
	}
}
