using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour {
    public GameObject player1;
    public GameObject player2;
    public float windspeed1;
    public float windspeed2;
    public float direction; //2D, in degrees
    private Vector3 wind;

	// Use this for initialization
	void Start () {
        direction = direction / 180 * Mathf.PI; //Converts to Radians
        float x = Mathf.Cos(direction);
        float y = 0;
        float z = Mathf.Sin(direction);
        wind = new Vector3(x, y, z);
	}
	
	// Checks whether the players are in its bounds and applies a wind to it.
	void Update () {
        if (InBounds(player1)) player1.GetComponent<Rigidbody>().AddForce(wind * windspeed1);
        if (InBounds(player2)) player2.GetComponent<Rigidbody>().AddForce(wind * windspeed2);
	}

    /// <summary>
    /// Checks whether an object is in bounds for wind to hit it
    /// </summary>
    bool InBounds(GameObject player)
    {
        float x = this.transform.position.x;
        float z = this.transform.position.z;

        float px = this.transform.position.x;
        float pz = this.transform.position.z;

        float xSize = this.transform.localScale.x;
        float zSize = this.transform.localScale.z;

        if (px < x - xSize || px > x + xSize) return false;
        if (pz < z - zSize || pz > z + zSize) return false;
        return true;
    }
}
