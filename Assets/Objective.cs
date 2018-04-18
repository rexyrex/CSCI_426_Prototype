using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour {
    public EnvironmentObject[] connectedThings;
    public bool spawners;
    public bool enemies;
    public GameObject[] allEnemies;
    public GameObject[] allSpawners;
    bool spawnersDone;
    bool enemiesDone;

	// Use this for initialization
	void Start () {
        if (enemies) enemiesDone = false;
        else enemiesDone = true;

        if (spawners) spawnersDone = false;
        else spawnersDone = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (spawnersDone && enemiesDone) return;
        if (!enemiesDone)
        {
            bool complete = true;
            for (int i = 0; i < allEnemies.Length; i++)
            {
                if (!(allEnemies[i] == null || allEnemies[i].Equals(null)))
                {
                    complete = false;
                }
            }
            if (complete) enemiesDone = true;
        }
        if (!spawnersDone)
        {
            bool complete = true;
            for (int i = 0; i < allSpawners.Length; i++)
            {
                if (allSpawners[i].GetComponent<GenericSpawnScript>().spawnerActive)
                {
                    complete = false;
                }
            }
            if (complete) spawnersDone = true;
        }
        if (spawnersDone && enemiesDone) Actuate();
    }

    void Actuate()
    {
        for(int i = 0; i < connectedThings.Length; i++)
        {
            connectedThings[i].Actuate();
        }
    }
}
