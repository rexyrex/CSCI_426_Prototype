using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockWall : MonoBehaviour {
    ClosingWall cw;
    float ID;
    List<GameObject> touching;
    int touchingWall; //0 is not touching, 1 is wall1, 2 is wall2

	// Use this for initialization
	void Start () {
        touching = new List<GameObject>();
	}
	
    /// <summary>
    /// sets the wall so we can stop it
    /// </summary>
    public void SetCW(ClosingWall inputcw)
    {
        cw = inputcw;
    }

    /// <summary>
    /// Sets the ID
    /// </summary>
    public void SetID(float inputID)
    {
        ID = inputID;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Block") touching.Add(collision.gameObject);
        else if (collision.gameObject.tag == "ClosingWall") touchingWall = 1;
        else if (collision.gameObject.tag == "ClosingWall2") touchingWall = 2;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Block") touching.Remove(collision.gameObject);
        else if (collision.gameObject.tag == "ClosingWall") touchingWall = 0;
        else if (collision.gameObject.tag == "ClosingWall2") touchingWall = 0;
    }

    // Update is called once per frame
    void Update () {
        if (touchingWall!=0)
        {
            for(int i = 0; i < touching.Count; i++)
            {
                List<float> ids = new List<float>();
                ids.Add(ID);
                if (touching[i].GetComponent<BlockWall>().Connects(ids, touchingWall)) cw.Stop();
            }
        }
	}

    /// <summary>
    /// Checks whether there is a path of blocks between the walls.
    /// </summary>
    public bool Connects(List<float> ids, int originWall)
    {
        if (touchingWall!=0 && touchingWall!=originWall) return true;

        //Add this to the list of ID's
        ids.Add(this.ID);

        //Check whether each node has a path to the wall.
        bool doesConnect = false;
        for(int i = 0; i < touching.Count; i++)
        {
            bool seenBefore = false;
            BlockWall bw = touching[i].GetComponent<BlockWall>();
            //Check whether the next node has already been seen
            for (int j = 0; j < ids.Count; j++)
            {
                if (bw.ID == ids[j]) seenBefore = true;
            }

            //If the node hasn't been seen check if there's a path.
            if (!seenBefore) if (bw.Connects(ids, originWall)) return true;// doesConnect = true;
        }

        return doesConnect;
    }
}
