using UnityEngine;
using Rewired;

public class ChargeWeapon : MonoBehaviour {
    int playerId;
    Player player;

    float counter;

    public float chargeDistance;
    public bool isAttacking { get; private set; }

    Rigidbody playerbody;

    void Awake() {
        playerId = GetComponentInParent<PlayerMovement>().playerId;
        player = ReInput.players.GetPlayer(playerId);
    }

	// Use this for initialization
	void Start () {
        isAttacking = false;
        playerbody = GetComponentInParent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (isAttacking)
        {
            counter += Time.deltaTime;
            isAttacking &= counter <= 2;
        } else if (player.GetButtonDown("Basic Attack"))
        {
            Fire();
        }
	}

    void Fire()
    {
        isAttacking = true;
        counter = 0;
        playerbody.AddForce(transform.forward * chargeDistance, ForceMode.Impulse);
    }
}
