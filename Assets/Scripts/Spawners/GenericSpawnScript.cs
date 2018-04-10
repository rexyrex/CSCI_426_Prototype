using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSpawnScript : MonoBehaviour {
	
	public GameObject[] spawnObjects;

	public float distanceFromPlayerToSpawn;

	public float enemySpawnFreq;
    public float waveTime;
    public float difficulty; // 0-3;
    float numToSpawn;
    float growthRate;
    int numLeft;
	float lastSpawnedTime = 0f;
    float lastWave = 0.0f;
    int spawnmax = 10;
    bool spawning = false;

	public Material activeMat;
	public Material inactiveMat;
    LineRenderer tether;
	Renderer rend;

	bool spawnerActive;


	Transform p1T;
	Transform p2T;
	Transform boulderT;
	GameObject boulder;

	// Use this for initialization
	void Start () {
		lastSpawnedTime = 0;
		rend  = GetComponent<Renderer>();
		p1T = GameObject.FindGameObjectWithTag ("Player1Tag").transform;
		p2T = GameObject.FindGameObjectWithTag ("Player2Tag").transform;
        tether = this.GetComponent<LineRenderer>();
        
        boulder = null;
        boulderT = null;

        // Sets the difficulty of the spawner
        if (difficulty <= 0)
        {
            numToSpawn = 2;
            growthRate = 0.2f;
        }
        else if (difficulty == 1)
        {
            numToSpawn = 3;
            growthRate = 0.34f;
        }
        else if (difficulty == 2)
        {
            numToSpawn = 3;
            growthRate = 0.5f;
        }
        else if (difficulty >= 3)
        {
            numToSpawn = 4;
            growthRate = 1f;
        }
	}
	
	// Update is called once per frame
	void Update () {
        
        // Seeing if players are close enough to be active
        Vector3 pos = this.transform.position;
		float distanceFromP1 = Vector3.Distance (pos, p1T.position);
		float distanceFromP2 = Vector3.Distance (pos, p2T.position);

        // Checking whether a boulder is blocking it
        if (boulder != null)
        {
            Vector3[] vertex = { this.transform.position, boulderT.position };
            tether.SetPositions(vertex);
            tether.endWidth = 0.5f/(1+Vector3.Distance(this.transform.position, boulderT.position));
            tether.startWidth = 0.5f / (1 + Vector3.Distance(this.transform.position, boulderT.position));

            HoldBoulder();
            float distanceFromB = Vector3.Distance(pos, boulderT.position);

            if (distanceFromB > 4)
            {
                Activate();
            }
            else
            {
                Deactivate();
            }
            
        }
        else
        {
            Activate();
        }		

        // Spawning enemies if desired
        if (spawning)
        {
            if (numLeft > 0)
            {
                if (Time.time - lastSpawnedTime > enemySpawnFreq && Mathf.Min(distanceFromP1, distanceFromP2) < distanceFromPlayerToSpawn && spawnerActive)
                {
                    Spawn(pos);
                    numLeft--;
                }
            }
            else
            {
                spawning = false;
            }
        }
        else if(Time.time - lastWave > waveTime && Mathf.Min(distanceFromP1, distanceFromP2) < distanceFromPlayerToSpawn && spawnerActive)
        {
            numLeft = (int)numToSpawn;
            if (numLeft > spawnmax) numLeft = spawnmax;
            lastWave = Time.time;
            spawning = true;
            numToSpawn++;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        GameObject o = other.gameObject;
        if (o.tag == "BoulderTag")
        {
            boulder = o;
            boulderT = o.transform;
            tether.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "BoulderTag")
        {
            boulder = null;
            boulderT = null;
            tether.enabled = false;
        }
    }

    private void HoldBoulder()
    {
        /*
        //int sign = 1;
        Vector3 velocity = boulder.GetComponent<Rigidbody>().velocity;
        if (boulderT.position.x > this.transform.position.x) velocity.x = -1 * Mathf.Abs(velocity.x);
        else velocity.x = Mathf.Abs(velocity.x);
        if (boulderT.position.y > this.transform.position.y) velocity.y = -1 * Mathf.Abs(velocity.y);
        else velocity.y = Mathf.Abs(velocity.y);
        if (boulderT.position.z > this.transform.position.z) velocity.z = -1 * Mathf.Abs(velocity.z);
        else velocity.z = Mathf.Abs(velocity.z);

        //if (Vector3.Distance(boulderT.position + velocity, this.transform.position) > Vector3.Distance(this.transform.position, boulderT.position))sign = -1;
        
        //float vel = velocity.magnitude;
        //if (vel < 1) vel = 1;*/

        Rigidbody rb = boulder.GetComponent<Rigidbody>();
        int factor = 50;
        Vector3 movement = this.transform.position - boulderT.position;
        if (spawnerActive)
        {
            movement = movement.normalized;
            rb.velocity += movement * 1.5f;
            /*if (rb.velocity.magnitude < 1f)
            {
                movement = movement.normalized;
                rb.velocity += movement * 10;
            }*/
            if (rb.velocity.magnitude > 12) rb.velocity = rb.velocity / 1.1f;
        }
        else if (rb.velocity.magnitude > 0) rb.velocity = rb.velocity / 3;
        
        
    }

    void Spawn(Vector3 pos){
		
		Quaternion quat = new Quaternion(0, 0, 0, 0);
		lastSpawnedTime = Time.time;
		int objInd = Random.Range (0, spawnObjects.Length-1);
		GameObject inst = Instantiate(spawnObjects[objInd], pos, quat);
	}

    void Activate()
    {
        spawnerActive = true;
        rend.material = activeMat;
    }

    void Deactivate()
    {
        spawnerActive = false;
        rend.material = inactiveMat;
    }
}
