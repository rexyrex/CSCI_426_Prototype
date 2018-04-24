using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTile : MonoBehaviour {
    public Renderer[] mats;
    protected Vector3 movement;
    protected BoxCollider bc;
    protected Color activeCol;
    protected Color currCol;
    protected Color endCol;
    protected bool reverting;
    protected bool actuating;
    public float fadeTime;
    protected float counter;

    // Use this for initialization
    void Start()
    {
        //movement = new Vector3(0, this.transform.position.y*2+0.1f, 0);
        bc = this.GetComponent<BoxCollider>();
        activeCol = new Color(240, 240, 240, 1);
        endCol = new Color(40, 40, 40, 0);
        actuating = false;
        reverting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (actuating)
        {
            counter -= Time.deltaTime;
            float colorVal = currCol.r;
            float ratio = (fadeTime - counter) / fadeTime;
            if(ratio > 1)
            {
                actuating = false;
                reverting = true;
                return;
            }

        }
        if (reverting)
        {
            counter += Time.deltaTime;
            if (counter >= fadeTime)
            {
                reverting = false;
                currCol = activeCol;
                SetCol();
                bc.enabled = true;
            }
            else
            {
                float colorVal = currCol.r;
                currCol = new Color(colorVal, colorVal, colorVal, currCol.a + Time.deltaTime / fadeTime);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player1Tag" || collision.gameObject.tag == "Player2Tag") Actuate();
    }

    public void Actuate()
    {
        bc.enabled = false;
        actuating = true;
        reverting = false;
        counter = fadeTime;
    }

    public void Revert()
    {
        reverting = false;

        counter = 0;
    }

    void SetCol()
    {
        for( int i = 0; i < mats.Length; i++)
        {
            mats[i].material.color = currCol;
        }
    }
}
