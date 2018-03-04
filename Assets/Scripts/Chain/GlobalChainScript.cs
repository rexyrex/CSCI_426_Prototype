﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalChainScript : MonoBehaviour {
    public enum ChainDistance { Close, Medium, Far };

    public GameObject player1;
    public GameObject player2;
    public GameObject link;
    public float damageDistanceThreshold;
    public int numLinks;
    public Material activeMat;
    public Material activeCloseMat;
    public Material activeMediumMat;
    public Material activeFarMat;
    public Material inactiveMat;
    public Material tooFarMat;

    private ChainDistance chainState;
    private float totalDist;
    private float width;
    private float idealLength;
    private Material currentMat;
    private Material oldMat;
    private bool isChainActive;
    private ChainJoint[] nodes;
    private GameObject[] nodeObjects;
    private Quaternion quat = new Quaternion(0, 0, 0, 0);
    private Vector3 startpos = new Vector3(0, 0, 0);


    // Use this for initialization
    void Start () {
        if (numLinks < 0) numLinks = 1;
        if (numLinks % 2 != 1) numLinks++;

        idealLength = Vector3.Distance(player1.transform.position, player2.transform.position) / numLinks;
        nodes = new ChainJoint[numLinks];
        nodeObjects = new GameObject[numLinks];

        //Building objects
        for(int i = 0; i < numLinks; i++)
        {
            nodeObjects[i] = ((GameObject)Instantiate(link));
        }

        //Determining neighbors and Initializing
        for (int i = 0; i < numLinks; i++)
        {
            GameObject other;
            ChainJoint cj = nodeObjects[i].GetComponent<ChainJoint>();
            if (i == 0)
            {
                other = player1;
                cj.CenterAnchor();
            }
            else 
            {
                other = nodeObjects[i - 1];
                
                if (i == numLinks - 1)//Attach player 2
                {
                    //cj.AttachPlayer(player2);
                }
            }
            cj.Initialize(this, inactiveMat, other);
            cj.SetSize(0.6f, idealLength);
            nodes[i] = cj;
        }
        
        //All nodes ignore collisions with other nodes and with the players
        for(int i = 0; i < numLinks; i++)
        {
            for(int j = 0; j < numLinks; j++)
            {
                if (i < j)
                {
                    Physics.IgnoreCollision(nodeObjects[i].GetComponent<Collider>(), nodeObjects[j].GetComponent<Collider>());
                    Debug.Log(i + " " + j);
                }
            }
            Physics.IgnoreCollision(nodeObjects[i].GetComponent<Collider>(), player1.GetComponent<Collider>());
            Physics.IgnoreCollision(nodeObjects[i].GetComponent<Collider>(), player2.GetComponent<Collider>());
        }

        oldMat = inactiveMat;
        currentMat = inactiveMat;
	}
	
	// Update is called once per frame
	void Update () {
        //Finding Basic Parameters
        totalDist = Vector3.Distance(player1.transform.position, player2.transform.position);
        idealLength = totalDist/numLinks;
        width = 1 - totalDist / damageDistanceThreshold;
        if (width > 0.5f)
        {
            width = 0.5f;
        }
        else if (width < 0.05)
        {
            width = 0.05f;
        }

        // Do they have full mana?
        if (GlobalDataController.gdc.currentMana >= 100)
        {
            isChainActive = true;
        }

        // Are they too far?
        if (totalDist >= damageDistanceThreshold)
        {
            //GlobalDataController.TetherBreak();
            isChainActive = false;
            currentMat = tooFarMat;
        }

        // Set Material
        if (isChainActive && GlobalDataController.gdc.currentMana >= 0)
        {
            if (totalDist < damageDistanceThreshold / 3 + 1)
            {
                currentMat = activeCloseMat;
                chainState = ChainDistance.Close;
                GlobalDataController.gdc.chainState = GlobalDataController.ChainDistance.Close;
            }
            else if (totalDist < damageDistanceThreshold * 2 / 3 + 1)
            {
                currentMat = activeMediumMat;
                chainState = ChainDistance.Medium;
                GlobalDataController.gdc.chainState = GlobalDataController.ChainDistance.Medium;
            }
            else if (totalDist < damageDistanceThreshold)
            {
                currentMat = activeFarMat;
                chainState = ChainDistance.Far;
                GlobalDataController.gdc.chainState = GlobalDataController.ChainDistance.Far;
            }
            else
            {
                currentMat = activeMat;
            }
        }
        else
        {
            currentMat = inactiveMat;
        }

        //Update all chains
        if (oldMat != currentMat)
        {
            oldMat = currentMat;
            for(int i = 0; i < numLinks; i++)
            {
                nodes[i].SetMat(currentMat);
            }
        }
        for (int i = 0; i < numLinks; i++)
        {
            nodes[i].SetSize(width, idealLength);
        }
    }

    public ChainDistance getChainState()
    {
        return chainState;
    }
}
