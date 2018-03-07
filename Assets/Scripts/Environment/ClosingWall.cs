using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosingWall : MonoBehaviour {
    public GameObject wall1;
    public GameObject wall2;
    public GameObject block;
    public float spawnFrequency;
    public float time;
    public Material blockColor;

    public int axis; //0 for x, 1 for z

    // Bounds for spawning
    float[] xBound;
    float[] yBound;
    float[] zBound;
    float timeCounter;
    Vector3 wallMove;
    bool closing;
    float IDcounter;

    Random rand;
    

	// Use this for initialization
	void Start () {
        if (axis == 0) wallMove = new Vector3(5, 0, 0);
        else if (axis == 1) wallMove = new Vector3(0, 0, 5);

        xBound = new float[2];
        xBound[0] = this.transform.position.x - this.transform.localScale.x*5;
        xBound[1] = this.transform.position.x + this.transform.localScale.x*5;

        yBound = new float[2];
        yBound[0] = this.transform.position.y + 5;
        yBound[1] = this.transform.position.y + 20;

        zBound = new float[2];
        zBound[0] = this.transform.position.z - this.transform.localScale.z*5;
        zBound[1] = this.transform.position.z + this.transform.localScale.z*5;

        Random.InitState((int)(Time.deltaTime));
        int number = Random.Range(1, 4);
        Spawn(number);
        timeCounter = 0;
        IDcounter = 0;

        closing = true;
    }
	
	// Update is called once per frame
	void Update () {
        // Update time
        timeCounter += Time.deltaTime;

        // Check if the walls should move
        if (!closing) return;
        if (Vector3.Distance(wall1.transform.localPosition, wall2.transform.localPosition) <= 1)
        {
            closing = false;
            return;
        }

        // If enough time has passed, spawn a random number of blocks
        if (timeCounter >= spawnFrequency)
        {
            timeCounter = 0;
            int number = Random.Range(1, 4);
            Spawn(number);
        }

        // Move the walls
        float speedScale = 1 / time * Time.deltaTime;
        wall1.transform.Translate(wallMove * speedScale);
        wall2.transform.Translate(wallMove * -1 * speedScale);

        // Resize bounds
        if (axis == 0)
        {
            xBound[0] += wallMove.x * speedScale * 5;
            xBound[1] -= wallMove.x * speedScale * 5;
        }
        else
        {
            zBound[0] += wallMove.z * speedScale * 5;
            zBound[1] -= wallMove.z * speedScale * 5;
        }
    }

    /// <summary>
    /// Spawns a new block that can be used to stop the wall.
    /// </summary>
    void Spawn(int num)
    {
        for(int i = 0; i < num; i++)
        {
            float x = Random.Range(xBound[0], xBound[1]);
            float y = Random.Range(yBound[0], yBound[1]);
            float z = Random.Range(zBound[0], zBound[1]);
            Vector3 location = new Vector3(x, y, z);

            x = Random.Range(1, 3);
            y = Random.Range(1, 3);
            z = Random.Range(1, 3);
            Vector3 size = new Vector3(x, y, z);

            GameObject go = Instantiate(block);
            go.GetComponent<BlockWall>().SetCW(this);
            go.GetComponent<BlockWall>().SetID(IDcounter);
            go.GetComponent<MeshRenderer>().material = blockColor;
            go.transform.position = location;
            go.transform.localScale = size;

            IDcounter++;
        }
    }

    /// <summary>
    /// Tells the walls to stop closing.
    /// </summary>
    public void Stop()
    {
        closing = false;
    }
}
