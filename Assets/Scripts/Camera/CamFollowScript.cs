using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowScript : MonoBehaviour {
    public Camera cam;
    public float camXPos;
    public float camYPos;
    public float camZPos;
    public float camAngle;
    private Vector3 distance;
    // Use this for initialization
    void Start () {
        distance = new Vector3(camXPos, camYPos, camZPos);
        cam.transform.eulerAngles = new Vector3(camAngle, 0, 0);
    }
	
	// Update is called once per frame
	void Update () {
        cam.transform.position = Vector3.Lerp(cam.transform.position, gameObject.transform.position + distance, 4.0f * Time.deltaTime);
        
    }
}
