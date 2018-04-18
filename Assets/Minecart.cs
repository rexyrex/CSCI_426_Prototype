using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minecart : MonoBehaviour {
    Transform parentTrans;
    public float max;
    public float min;
    public float axis;
    public Rail startRail;
    Vector3 localpos;
    Quaternion rotation;
    Quaternion localRot;
    float startPos;
    float startY;
    List<Rail> rails;

	// Use this for initialization
	void Start () {
        parentTrans = this.GetComponentInParent<Transform>();
        float rot = parentTrans.rotation.y;
        startY = parentTrans.position.y;
        rails = new List<Rail>();
        localpos = this.transform.position;
        if(startRail!=null) rails.Add(startRail);
        if (axis == 1)
        {
            startPos = parentTrans.position.x;
            parentTrans.rotation.Set(0, 90, 0, 0);
        }
        else if (axis == 0)
        {
            startPos = parentTrans.position.z;
            parentTrans.rotation.Set(0, 0, 0, 0);
        }
        else axis = -1;

        localRot = this.transform.rotation;
        rotation = parentTrans.rotation;

    }
	
	// Update is called once per frame
	void Update () {
        if (rails.Count == 1)
        {
            axis = rails[0].axis;
            if (axis == 1)
            {
                startPos = parentTrans.position.x;
                rotation = new Quaternion(0, 90, 0, 0);
                parentTrans.rotation = rotation;
            }
            else if (axis == 0)
            {
                startPos = parentTrans.position.z;
                rotation = new Quaternion(0, 0, 0, 0);
                parentTrans.rotation = rotation;
            }
        }

        else return;   
        if (axis == 1)
        {
            float x = startPos;
            float y = startY;
            float z = parentTrans.position.z;
            if (z > max) z = max;
            else if (z < min) z = min;
            parentTrans.position.Set(x, y, z);
        }
        else if (axis == 0)
        {
            float x = startPos;
            float y = startY;
            float z = parentTrans.position.z;
            if (z > max) z = max;
            else if (z < min) z = min;
            parentTrans.position.Set(x, y, z);
        }
        this.transform.position = localpos;
        parentTrans.rotation = rotation;
        this.transform.rotation = localRot;
    }

    public void AddRail(Rail item)
    {
        rails.Add(item);
    }

    public void RemoveRail(Rail item)
    {
        rails.Remove(item);
    }
}
