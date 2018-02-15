using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperGun : Gun {
    [SerializeField]
    protected Transform player1;
    [SerializeField]
    protected Transform player2;
    [SerializeField]
    protected float chanceToHitClosest = 0.75f;
    [SerializeField]
    protected float aimDelay = 2.0f;

    Random rand;
    SniperController sniper;
    LineRenderer lr;

    protected override void Start()
    {
        base.Start();

        rand = new Random();
        sniper = GetComponentInParent<SniperController>();
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;
    }

    protected override void Update()
    {
        float player1Distance = Vector3.Distance(transform.position, player1.position);
        float player2Distance = Vector3.Distance(transform.position, player2.position);

        if (player1Distance < range || player2Distance < range) {
            lr.enabled = true;

            if (player1Distance < player2Distance) {
                sniper.TurnToward(player1.position);
                lr.SetPosition(0, bulletSpawner.position);
                lr.SetPosition(1, player1.position);
                if (!IsInvoking("Fire")) Invoke("Fire", aimDelay);
            } else {
                sniper.TurnToward(player2.position);
                lr.SetPosition(0, bulletSpawner.position);
                lr.SetPosition(1, player2.position);

                if (!IsInvoking("Fire")) Invoke("Fire", aimDelay);
            }
        } else {
            lr.enabled = false;
        }
	}
}
