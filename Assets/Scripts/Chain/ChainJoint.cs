using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainJoint : MonoBehaviour {
    GlobalChainScript gcs;

    // Use this for initialization
    void Start () {
		
	}

    public void Initialize(GlobalChainScript control)
    {
        gcs = control;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
			other.gameObject.GetComponent<BasicColorEnemyScript>().OnHitByChain(1, GlobalDataController.gdc.chainCharged);
        }else if (other.gameObject.tag == "breakable")
        {
            other.gameObject.GetComponent<BreakScript>().Break();
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Heres");
        if (other.gameObject.tag == "Enemy")
        {
			other.gameObject.GetComponent<BasicEnemyScript>().OnHitByChain(1, GlobalDataController.gdc.chainCharged);
        }
    }

    // Update is called once per frame
    void Update () {

    }

    //Used to check the state of this chain
    public GlobalChainScript.ChainDistance getChainState()
    {
        return gcs.GetChainState();
    }
}
