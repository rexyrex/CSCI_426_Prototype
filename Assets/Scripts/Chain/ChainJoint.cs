using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainJoint : MonoBehaviour {
    GlobalChainScript control;
    public float pullDist;

    protected Rigidbody end1;
    protected Rigidbody end2;

    protected Transform trans1;
    protected Transform trans2;

    protected GameObject other;
    public HingeJoint fhj;
    public HingeJoint bhj;

    protected MeshRenderer mesh;
    protected float length;
    protected float oldLength;
    protected Material mat;
    protected bool isChainActive;
    protected float normdist;

    protected RaycastHit hit;

    // Use this for initialization
    void Start () {
		
	}

    public void Initialize(GlobalChainScript controller, Material startMat, GameObject e1, GameObject e2)
    {
        pullDist = 0.5f;

        mat = startMat;
        control = controller;

        end1 = e1.GetComponent<Rigidbody>();
        end2 = e2.GetComponent<Rigidbody>();

        mesh = this.GetComponent<MeshRenderer>();
        mesh.material = mat;

        fhj.anchor.Set(0, 1, 0);
        fhj.connectedBody = e1.GetComponent<Rigidbody>(); 
        fhj.autoConfigureConnectedAnchor = false;
        fhj.connectedAnchor.Set(0, -1, 0);

        bhj.anchor.Set(0, -1, 0);
        bhj.connectedBody = e2.GetComponent<Rigidbody>();
        bhj.autoConfigureConnectedAnchor = false;
        bhj.connectedAnchor.Set(0, 1, 0);

        isChainActive = false;
    }

    public void CenterAnchor(bool front)
    {
        if (front)
        {
            fhj.autoConfigureConnectedAnchor = false;
            fhj.connectedAnchor.Set(0, 0, 0);
        }
        else
        {
            bhj.autoConfigureConnectedAnchor = false;
            bhj.connectedAnchor.Set(0, 0, 0);
        }
        
    }

    protected void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Heres");
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<BasicEnemyScript>().OnHitByChain(1, isChainActive);
        }
    }

    // Update is called once per frame
    void Update () {

    }

    //this object's size
    public void SetSize(float width, float length)
    {
        this.length = length;
        //this.transform.localScale.Set(width, length, width);
        //hj.anchor.Set(0, length / 2, 0);
    }

    //Sets this object's material
    public void SetMat(Material newMat)
    {
        mat = newMat;
        mesh.material = mat;
    }

    //Used to check the state of this chain
    public GlobalChainScript.ChainDistance getChainState()
    {
        return control.getChainState();
    }
}
