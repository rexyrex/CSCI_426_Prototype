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
    public HingeJoint hj;
    public HingeJoint phj;

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

    public void Initialize(GlobalChainScript controller, Material startMat, GameObject other)
    {
        pullDist = 0.5f;

        mat = startMat;
        control = controller;

        mesh = this.GetComponent<MeshRenderer>();
        mesh.material = mat;

        hj.connectedBody = other.GetComponent<Rigidbody>();
        hj.anchor.Set(0, length / 2, 0);

        isChainActive = false;
    }

    public void CenterAnchor()
    {
        hj.autoConfigureConnectedAnchor = false;
        hj.connectedAnchor.Set(0, 0, 0);
    }

    public void AttachPlayer(GameObject player)
    {
        phj.connectedBody = player.GetComponent<Rigidbody>();
        phj.anchor.Set(0, -1 * length / 2, 0);
        phj.autoConfigureConnectedAnchor = false;
        phj.connectedAnchor.Set(0, 0, 0);
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
        this.transform.localScale.Set(width, length, width);
        hj.anchor.Set(0, length / 2, 0);
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
