using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class ChaseBossScript : MonoBehaviour {
    Vector3 dashDest;

    float chaseDuration = 7f;
    float chaseLastTime;

    float changeDashDestFreq = 2.5f;
    float changeDashDestLast;

    GameObject player1Obj;
    GameObject player2Obj;

    public GameObject directionalLight;
    public GameObject spotLight;


    float spawnEnemyFreq = 6f;
    float spawnEnemyLast;

    float changeLocFreq = 10f;
    float changeLocLast;

    float maxhealth = 10000f;
    float health = 10000f;

    public Slider healthBar;

    private float lastHitTime;
    private float hitFreq = 1.2f;
    public float speed = 15f;

    public Material normMat;
    public Material rageMat;

    public GameObject manaObject;

    public enum bossMode { normal, rage }

    public Transform[] bossLocs;
    NavMeshAgent bossAgent;

    bossMode mode;
    public GameObject boulder;

    // Use this for initialization
    void Start()
    {
        changeDashDestLast = Time.time;
        chaseLastTime = Time.time;
        lastHitTime = Time.time;
        changeLocLast = Time.time;
        mode = bossMode.normal;
        updateMaterial();
        bossAgent = GetComponent<NavMeshAgent>();
        bossAgent.enabled = false;
        spotLight.GetComponent<Light>().intensity = 0;
        player1Obj = GameObject.FindGameObjectWithTag("Player1Tag");
        player2Obj = GameObject.FindGameObjectWithTag("Player2Tag");
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = health / maxhealth;

        if (Time.time - changeDashDestLast > changeDashDestFreq && bossAgent.enabled == false)
        {
            changeDashDestLast = Time.time;
            int playerInd = 2;
            Vector3 spotTrans;
            if (playerInd == 1)
            {
                dashDest = player1Obj.transform.position;
                spotTrans = player1Obj.transform.position;
                spotTrans.y += 2;
                spotLight.transform.position = spotTrans;
            }
            else
            {
                dashDest = player2Obj.transform.position;
                spotTrans = player2Obj.transform.position;
                spotTrans.y += 2;
                spotLight.transform.position = spotTrans;
            }
        }

        if (Time.time - changeLocLast > changeLocFreq && bossAgent.enabled == true)
        {
            changeLocLast = Time.time;
            int locInd = Random.Range(0, bossLocs.Length);
            bossAgent.destination = player2Obj.transform.position;
        }

        if (bossAgent.enabled == false)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, dashDest, step);
        }

        if (health < 0)
        {
            GlobalDataController.gdc.gameover = true;
            Destroy(gameObject);
        }


    }

    void updateMaterial()
    {
        switch (mode)
        {
            case bossMode.normal:
                GetComponent<Renderer>().material = normMat;
                break;
            case bossMode.rage:
                GetComponent<Renderer>().material = rageMat;
                break;
        }
    }

    // Damages the players
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player1Tag")
        {
            player1Obj.GetComponent<Player1Script>().Damage(Random.Range(10, 20));
        }
        else if (collision.gameObject.tag == "Player2Tag")
        {
            player2Obj.GetComponent<Player2Script>().Damage(Random.Range(10, 20));
        }
        else if (collision.gameObject.tag == "Trap")
        {
            getDamaged(Random.Range(1000, 2000));
        }
    }

    public void getDamaged(int damage)
    {
        if (Time.time - lastHitTime > hitFreq)
        {

            //Debug.Log("Health is now: " + health);
            health -= damage;
            //DamageTextController.CreateFloatingText (damage.ToString(), gameObject.transform);
            lastHitTime = Time.time;
        }
    }
}
