using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chargeweapon : MonoBehaviour {
    private GameObject player;
    private bool isAttacking;
    public float chargeDistance;
    private float counter;
    private Rigidbody playerbody;

	// Use this for initialization
	void Start () {
        isAttacking = false;
        counter = 0;
        player = GameObject.FindGameObjectWithTag("Player2Tag");
        playerbody = player.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (isAttacking)
        {
            counter+=Time.deltaTime;
            if (counter >  2)
            {
                isAttacking = false;
            }
        }
	}

    public void Fire(Vector3 Direction)
    {
        Vector3 dir = Direction - player.transform.position;
        //float norml = Vector3.Distance(player.transform.position, Direction);
        isAttacking = true;
        counter = 0;
        playerbody.AddForce(dir.normalized*chargeDistance, ForceMode.Impulse);
    }

    public bool Attacking()
    {
        return isAttacking;
    }
}
