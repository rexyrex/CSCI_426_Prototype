using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMana : MonoBehaviour {

    public GameObject manaType; // set what type of mana you want to spawn
    public float spawnfreq = 5f; // time between spawns
    public GameObject[] spawned;
    public int max = 5;
    float lastTime = 0.0f;
    float prx = 5;
    float nrx = -5;
    float prz = 5;
    float nrz = -5;
    float y;
    /*float prx;
    float nrx;
    float y;
    float prz;
    float nrz;*/
    Quaternion quat = new Quaternion(0, 0, 0, 0);

    // Use this for initialization
    void Start () {
        spawned = new GameObject[max];
        Transform t = this.GetComponentInParent<Transform>();
        y = t.position.y + 0.5f * t.localScale.y;
        //prx = 15; // t.position.x + 0.5f * t.localScale.x;
        //nrx = -15; // t.position.x - 0.5f * t.localScale.x;
        //Debug.Log(prx);
        //prz = 15; // t.position.z + 0.5f * t.localScale.z;
        //nrz = -15; // t.position.z - 0.5f * t.localScale.z;
    }
	
	// Update is called once per frame
	void Update () {
        if (manaType == null) return;
        if (Time.time - lastTime < spawnfreq) return;
        else
        {
            for (int i = 0; i < max; i++)
            {
                if (spawned[i] == null || spawned[i].Equals(null))
                {
					int scale = 2;
					float x = Random.Range(nrx*scale, prx*scale);
					float z = Random.Range(nrz*scale, prz*scale);

					Debug.Log (x + "," + z);
					spawned[i] = Instantiate(manaType, gameObject.transform.position + new Vector3(x, y, z), quat);
                    lastTime = Time.time;
                    break;
                }
            }
        }
	}
}
