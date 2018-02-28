using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLinkScript : MonoBehaviour {
    GlobalChainScript control;
    public float pullDist;
    public float pullBackDist;

    protected Rigidbody end1;
    protected Rigidbody end2;

    protected Transform trans1;
    protected Transform trans2;

    protected GameObject e1;
    protected GameObject e2;

    protected LineRenderer line;
    protected float length;
    protected float oldLength;
    protected Material mat;
    protected bool isChainActive;
    protected float normdist;

    protected RaycastHit hit;

    // Use this for initialization
    void Start() {

    }

    protected void GenericInitialize(GlobalChainScript controller, Material startMat)
    {
        //pullDist = 0.5f;

        mat = startMat;
        control = controller;

        line = this.GetComponent<LineRenderer>();
        line.material = mat;

        isChainActive = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        hit = new RaycastHit(); //Does this get deleted when it has no pointers?
        if (Hits(0))
        {
            switch (hit.transform.gameObject.tag)
            {
                case "Enemy":
                    hit.transform.gameObject.GetComponent<BasicEnemyScript>().OnHitByChain(1, isChainActive);
                    Debug.Log(isChainActive);
                    break;
                case "Boss":
                    hit.transform.gameObject.GetComponent<BasicEnemyScript>().OnHitByChain(1, isChainActive);
                    break;
                default: break;
            }
            hit = new RaycastHit();
        }
        if (Hits(1))
        {
            switch (hit.transform.gameObject.tag)
            {
                case "Enemy":
                    hit.transform.gameObject.GetComponent<BasicEnemyScript>().OnHitByChain(1, isChainActive);
                    Debug.Log(isChainActive);
                    break;
                case "Boss":
                    hit.transform.gameObject.GetComponent<BasicEnemyScript>().OnHitByChain(1, isChainActive);
                    break;
                default: break;
            }
        }
    }

    //Checks whether the line hits something
    protected virtual bool Hits(int axis)// 0, 1 or 2
    {
        return false;
    }

    //Updates control with this object's length
    protected void UpdateLength()
    {
        control.updateLength(oldLength, length);
    }

    //Sets this object's material
    public void SetMat(Material newMat)
    {
        mat = newMat;
        line.material = mat;
    }

    //Sets this object's width
    public void SetWidth(float width)
    {
        line.SetWidth(width, width);
    }

    //Sets the normal length of a line
    public void SetNormDist(float dist)
    {
        normdist = dist;
    }

    //Used to check the state of this chain
    public GlobalChainScript.ChainDistance getChainState()
    {
        return control.getChainState();
    }

    protected Vector3 findForce(Rigidbody end1, Rigidbody end2, Vector3 force, float length)
    {
        
        float xloc = (end2.position.x - end1.position.x) / 2;
        float yloc = (end2.position.y - end1.position.y) / 2;
        float zloc = (end2.position.z - end1.position.z) / 2;
        //play with speedUp and slowDown
        float speedUp = 50;
        float slowDown = 20f;
        float ratio = length / normdist;
        if (ratio < 0.4) ratio*=0.1f;
        else ratio *= 2;
        speedUp *= ratio;
        slowDown *= ratio;
        
        float x = force.x;
        float y = force.y;
        float z = force.z;

        if (xloc * end1.velocity.x < 0) //then they are going in the wrong direction and should speed up
        {
            x = x*speedUp;
        }
        else
        {
            x = x*slowDown;
        }

        if (yloc * end1.velocity.y < 0) //then they are going in the wrong direction and should speed up
        {
            y = y*speedUp;
        }
        else
        {
            y = y*slowDown;
        }

        if (zloc * end1.velocity.z < 0) //then they are going in the wrong direction and should speed up
        {
            z = z*speedUp;
        }
        else
        {
            z = z*slowDown;
        }

        force = new Vector3(x, y, z);
        return force;
    }
}
