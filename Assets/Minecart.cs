using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minecart : MonoBehaviour {
    Transform parentTrans;
    public float max;
    public float min;
    public float axis;
    float startPos;

	// Use this for initialization
	void Start () {
        parentTrans = this.GetComponentInParent<Transform>();
        float rot = parentTrans.rotation.y;

        if (axis == 1)
        {
            axis = 1;
            startPos = parentTrans.position.x;
            parentTrans.rotation.Set(0, 90, 0, 0);
        }
        else if (axis == 0)
        {
            axis = 0;
            startPos = parentTrans.position.z;
            parentTrans.rotation.Set(0, 0, 0, 0);
        }
        else axis = -1;

        

    }
	
	// Update is called once per frame
	void Update () {
        if (axis == 1)
        {
            float x = startPos;
            float y = parentTrans.position.y;
            float z = parentTrans.position.z;
            if (z > max) z = max;
            else if (z < min) z = min;
            parentTrans.position.Set(x, y, z);
        }
        else if (axis == 0)
        {
            float x = startPos;
            float y = parentTrans.position.y;
            float z = parentTrans.position.z;
            if (z > max) z = max;
            else if (z < min) z = min;
            parentTrans.position.Set(x, y, z);
        }

    }
}
