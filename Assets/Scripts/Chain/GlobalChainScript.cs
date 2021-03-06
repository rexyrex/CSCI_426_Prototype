﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalChainScript : MonoBehaviour {
    public enum ChainDistance { Close, Medium, Far };

    public GameObject player1;
    public GameObject player2;
    //public GameObject link;
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

    Vector3[] intersections;

    //painful but yes right now we are instantiating them all manually -_-
    public GameObject startNode;
    public GameObject n1;
    public GameObject n2;
    public GameObject n3;
    public GameObject n4;
    public GameObject n5;
    public GameObject n6;
    public GameObject n7;
    public GameObject n8;
    public GameObject n9;
    public GameObject endNode;

    // Use this for initialization
    void Start () {
        //Numlinks needs to be preset
        /*if (numLinks < 0) numLinks = 1;
        if (numLinks % 2 != 1) numLinks++;*/

        idealLength = Vector3.Distance(player1.transform.position, player2.transform.position) / numLinks;
        //nodes = new ChainJoint[numLinks];
        nodeObjects = new GameObject[numLinks];

        //For easy access. Yes painful still I know...
        nodeObjects[0] = startNode;
        nodeObjects[1] = n1;
        nodeObjects[2] = n2;
        nodeObjects[3] = n3;
        nodeObjects[4] = n4;
        nodeObjects[5] = n5;
        nodeObjects[6] = n6;
        nodeObjects[7] = n7;
        nodeObjects[8] = n8;
        nodeObjects[9] = n9;
        nodeObjects[10] = endNode;

        intersections = new Vector3[nodeObjects.Length - 1];

        //All nodes ignore collisions with other nodes and with the players
        for (int i = 0; i < numLinks; i++)
        {
            for (int j = 0; j < numLinks; j++)
            {
                if (i < j)
                {
                    Physics.IgnoreCollision(nodeObjects[i].GetComponent<Collider>(), nodeObjects[j].GetComponent<Collider>());
                }
            }
            Physics.IgnoreCollision(nodeObjects[i].GetComponent<Collider>(), player1.GetComponent<Collider>());
            Physics.IgnoreCollision(nodeObjects[i].GetComponent<Collider>(), player2.GetComponent<Collider>());
        }

        // An artifact of a more efficient iteration...
        /*for (int i = 0; i < numLinks; i++)
        {
            nodeObjects[i] = ((GameObject)Instantiate(link));
        }

        //Determining neighbors and Initializing
        for (int i = 0; i < numLinks; i++)
        {
            GameObject o1;
            GameObject o2;
            ChainJoint cj = nodeObjects[i].GetComponent<ChainJoint>();
            if (i == 0)
            {
                o1 = player1;
                if (numLinks == 1) o2 = player2;
                else o2 = nodeObjects[i + 1];
                //cj.CenterAnchor(true);
            }
            else if (i == numLinks - 1)//Attach player 2
            {
                o1 = nodeObjects[i - 1];
                o2 = player2;
                //cj.CenterAnchor(false);
            }
            else
            {
                o1 = nodeObjects[i - 1];
                o2 = nodeObjects[i - 1];
            }
            
            nodes[i] = cj;
        }*/

        totalDist = Vector3.Distance(player1.transform.position, player2.transform.position);
        idealLength = totalDist / numLinks;
        width = 0.3f;
        oldMat = inactiveMat;
        currentMat = inactiveMat;
        UpdateChain();
	}
	
	// Update is called once per frame
	void Update () {
        //Finding Basic Parameters
        totalDist = Vector3.Distance(player1.transform.position, player2.transform.position);
        idealLength = totalDist/(numLinks+3);
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
        UpdateChain();
    }

    /// <summary>
    /// Directly updates the width, length, and color of the chain.
    /// </summary>
    void UpdateChain()
    {
        float curveLength = 0;// idealLength;// nodeObjects[1].transform.localScale.y;
        for (int i = 0; i < numLinks; i++)
        {
            if (i == 0) curveLength += Vector3.Distance(nodeObjects[i].transform.position, player1.transform.position);
            else curveLength += Vector3.Distance(nodeObjects[i].transform.position, nodeObjects[i - 1].transform.position);
            if (i >= numLinks - 1) curveLength += Vector3.Distance(nodeObjects[i].transform.position, player2.transform.position);
        }

        float idealCurveLength = curveLength / numLinks /2;
        idealLength = idealCurveLength;

        Vector3 scale = new Vector3(width, idealLength, width);
        for(int i = 0; i < numLinks; i++)
        {
            
        }

        //for(int i = 0; i < numLinks; i++)
        //{
        //    nodeObjects[i].GetComponent<MeshRenderer>().material = currentMat;
        //    nodeObjects[i].transform.localScale = scale;
        //    //nodeObjects[i].GetComponent<HingeJoint>().anchor = new Vector3(0, length/magic, 0);
        //    //if(i!=0 && i!=numLinks-1)
        //    //        nodeObjects[i].GetComponent<HingeJoint>().connectedAnchor = new Vector3(0, -1*length / magic, 0);
        //}
    }

    public ChainDistance GetChainState()
    {
        return chainState;
    }

    public bool IsActive()
    {
        return isChainActive;
    }
}
