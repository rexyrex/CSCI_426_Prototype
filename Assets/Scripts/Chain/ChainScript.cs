using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainScript : MonoBehaviour {

    private LineRenderer lineRenderer;

    public Transform p1Trans;
    public Transform p2Trans;

    public Material inactiveMat;
    public Material activeMat;
    public Material toofarMat;

    public float damageThreshold;
    public float pullThreshold;

    bool isChainActive;

	// Use this for initialization
	void Start () {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.material = inactiveMat;
        isChainActive = false;
	}
	
	// Update is called once per frame
	void Update () {

        //Activating and Deactivating the Chain
        if(GlobalDataController.gdc.currentMana >= 100){
            isChainActive = true;
        }
        if (isChainActive && GlobalDataController.gdc.currentMana >=0){
            GlobalDataController.gdc.currentMana -= 0.17f;
            lineRenderer.material = activeMat;
        } else{
            isChainActive = false;
            lineRenderer.material = inactiveMat;
        }

        //Determining the length of the chain
        float dist = Vector3.Distance(p1Trans.position, p2Trans.position);
        float width = 1 - dist / damageThreshold;
        if (width > 0.7f){
            width = 0.7f;
        }
        else if (width < 0.05){
            width = 0.05f;
        }

        //If the players are too far apart...
        if (dist>=damageThreshold){
            GlobalDataController.TetherBreak();
            isChainActive = false;
            
            //GlobalDataController.gdc.currentMana = 1;
            lineRenderer.material = toofarMat;
        }

        if (dist > pullThreshold)
        {
            GlobalDataController.gdc.tetherPull = true;
        }
        else
        {
            GlobalDataController.gdc.tetherPull = false;
        }

        lineRenderer.SetWidth(width, width);
        lineRenderer.SetPosition(0, p1Trans.position);
        lineRenderer.SetPosition(1, p2Trans.position);

        RaycastHit hit;
        if(Physics.Raycast(p1Trans.position, p2Trans.position - p1Trans.position, out hit))
        {
            //Debug.Log("we hit a " + hit.transform.gameObject.tag);
            switch (hit.transform.gameObject.tag)
            {
                case "SphereTag": if(isChainActive) Destroy(hit.transform.gameObject);  break;
                case "Sphere2Tag": Destroy(hit.transform.gameObject); break;
			case "SizeChangeEnemyTag": hit.transform.gameObject.GetComponent<SizeChangeEnemyScript>().OnHitByChain(1,isChainActive); break;
                default: break;
            }
            
        }    
        
	}
}
