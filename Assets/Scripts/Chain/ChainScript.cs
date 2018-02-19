using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainScript : MonoBehaviour {

	public enum ChainDistance {Close, Medium, Far};

    private LineRenderer lineRenderer;
	private ChainDistance chainState;

    public Transform p1Trans;
    public Transform p2Trans;

    public Material inactiveMat;
    public Material activeMat;
    public Material toofarMat;


	public Material activeCloseMat;
	public Material activeMediumMat;
	public Material activeFarMat;

    public float damageDistanceThreshold;
    public float pullThreshold;

    bool isChainActive;

	// Use this for initialization
	void Start () {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.material = inactiveMat;
        isChainActive = false;
	}

	public ChainDistance getChainState(){
		return chainState;
	}
	
	// Update is called once per frame
	void Update () {

		//Determining the length of the chain
		float dist = Vector3.Distance(p1Trans.position, p2Trans.position);
		//Debug.Log (dist);
		float width = 1 - dist / damageDistanceThreshold;
		if (width > 0.7f){
			width = 0.7f;
		}
		else if (width < 0.05){
			width = 0.05f;
		}

        //Activating and Deactivating the Chain
        if(GlobalDataController.gdc.currentMana >= 100){
            isChainActive = true;
        }
		if (isChainActive && GlobalDataController.gdc.currentMana >= 0) {
			GlobalDataController.gdc.currentMana -= 0.01f;
			//lineRenderer.material = activeMat;

			if (dist < damageDistanceThreshold/3+1) {
				lineRenderer.material = activeCloseMat;
				chainState = ChainDistance.Close;
				GlobalDataController.gdc.chainState = GlobalDataController.ChainDistance.Close;
				//Debug.Log ("close");
			} else if (dist < damageDistanceThreshold * 2/3+1) {
				lineRenderer.material = activeMediumMat;
				chainState = ChainDistance.Medium;
				GlobalDataController.gdc.chainState = GlobalDataController.ChainDistance.Medium;
				//Debug.Log ("med");
			} else if (dist < damageDistanceThreshold) {
				lineRenderer.material = activeFarMat;
				chainState = ChainDistance.Far;
				GlobalDataController.gdc.chainState = GlobalDataController.ChainDistance.Far;
				//Debug.Log ("far");
			} else {
				lineRenderer.material = activeMat;
				//Debug.Log ("error");
			}
		} else {
			lineRenderer.material = inactiveMat;
		}




        //If the players are too far apart...
        if (dist>=damageDistanceThreshold){
            GlobalDataController.TetherBreak();
            isChainActive = false;
			//Debug.Log ("Too Far");
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
			case "Enemy":
				hit.transform.gameObject.GetComponent<BasicEnemyScript> ().OnHitByChain (1, isChainActive);
				break;
			case "Boss":
				hit.transform.gameObject.GetComponent<BasicEnemyScript> ().OnHitByChain (1, isChainActive);
				break;
				default: break;
            }
            
        }    
        
	}
}
