using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockWall : MonoBehaviour {
    ClosingWall cw;
    float ID;
    List<GameObject> touching;

	// Use this for initialization
	void Start () {
        touching = new List<GameObject>();
	}
	
    void SetCW(ClosingWall inputcw)
    {
        cw = inputcw;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "block") touching.Add(collision.gameObject);
    }

    // Update is called once per frame
    void Update () {
		
	}

    public bool Connects(float thisID)
    {
        
        return false;
    }
}
