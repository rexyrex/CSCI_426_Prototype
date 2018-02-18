﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDataController : MonoBehaviour {



    public static GlobalDataController gdc;

    private GameObject player1Reference;
    private GameObject player2Reference;

	public enum ChainDistance {Close, Medium, Far};

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

    //Player Positions
    public Vector3 p1pos;
    public Vector3 p2pos;

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
        UpdatePlayerStats();
        p1currentHealth = p1defaultHealth;
        p2currentHealth = p2defaultHealth;
        currentMana = defaultMana;
        tooFar = false;
		chainState = ChainDistance.Close;
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
		
	}
}
