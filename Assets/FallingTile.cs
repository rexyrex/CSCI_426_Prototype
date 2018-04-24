using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTile : MonoBehaviour {
    public MeshRenderer[] mats;
    protected Vector3 movement;
    protected Collider bc;
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
        bc = this.GetComponent<MeshCollider>();
        activeCol = new Color(100, 100, 100, 1);
        currCol = activeCol;
        actuating = false;
        reverting = false;
        SetCol();
    }

    // Update is called once per frame
    void Update()
    {
        if (actuating)
        {
            counter += Time.deltaTime;
            float ratio = (fadeTime - counter) / fadeTime;
            if(ratio < 0)
            {
                actuating = false;
                reverting = true;
                fadeTime = fadeTime / 2;
                bc.enabled = false;
            }
            else
            {
                float colorVal = 50 + (100 - 50) * ratio;
                currCol = new Color(colorVal, colorVal, colorVal, 1 - ratio);
                SetCol();
            }
        }
        if (reverting)
        {
            counter -= Time.deltaTime;
            float ratio = (fadeTime - counter) / fadeTime;
            if (counter < 0)
            {
                reverting = false;
                fadeTime *= 2;
                currCol = activeCol;
                SetCol();
                bc.enabled = true;
            }
            else
            {
                float colorVal = 50 + (100 - 50) * ratio;
                currCol = new Color(colorVal, colorVal, colorVal, 1 - ratio);
                SetCol();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player1Tag" || collision.gameObject.tag == "Player2Tag")
        {
            if(!actuating && !reverting) this.Actuate();
        }
    }

    public void Actuate()
    {
        actuating = true;
        reverting = false;
        counter = 0;
    }

    void SetCol()
    {
        Debug.Log("seeting color");
        for( int i = 0; i < mats.Length; i++)
        {
            mats[i].material.color = currCol;
        }
    }
}
