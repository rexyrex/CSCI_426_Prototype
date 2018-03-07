using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalLinkScript : ChainLinkScript
{

    // Use this for initialization
    void Start()
    {
        
    }

    // Initialize ends
    public void InitializeNormalLink(GlobalChainScript controller, Material startMat, GameObject e)
    {
        base.GenericInitialize(controller, startMat);
        line.positionCount = 2;

        end1 = this.GetComponent<Rigidbody>();
        end2 = e.GetComponent<Rigidbody>();

        trans1 = this.GetComponent<Transform>();
        trans2 = e.GetComponent<Transform>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        //rendering the chain
        line.SetPosition(0, trans1.position);
        line.SetPosition(1, trans2.position);

        //Determine new length and let controller know
        oldLength = length;
        length = Vector3.Distance(trans1.position, trans2.position);
        UpdateLength();

        //Physics on Chain

        Vector3 force = (trans2.position - trans1.position);
        force = findForce(end1, end2, force, length);
        force *= (Vector3.Distance(trans2.position, trans1.position) - normdist);
        //Debug.Log(normdist + ":        " + force);
        end1.AddForce(force);
        

        /*if(length > pullBackDist)
        {
            end2.AddForce((trans2.position - trans1.position) / 10);
        }*/

        //check to see if I hit something
        base.Update();
    }
}