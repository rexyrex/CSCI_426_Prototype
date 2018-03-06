using UnityEngine;
using Rewired;

/// <summary>
/// Base component for guns.
/// </summary>
[RequireComponent (typeof(Collider))]
[System.Serializable]
public class Gun : MonoBehaviour {
    public int playerId;

    public float damage = 1.0f;
    public float speed = 100.0f;
    public float range = 1000.0f;

    public GameObject projectile;

    [SerializeField]
    protected GunAimDelay gunAimDelay;

    Transform bulletSpawner;
    Rigidbody owner;

    Player player;

    void Awake() {
        playerId = GetComponentInParent<PlayerMovement>().playerId;
        player = ReInput.players.GetPlayer(playerId);
    }

	// Use this for initialization
	protected virtual void Start () {
        if (projectile == null)
            throw new System.ArgumentNullException("Projectile is not assigned.");
        if (projectile.GetComponent<Rigidbody>() == null)
            Debug.Log("No rigidbody on projectile!");

        bulletSpawner = GetComponentInChildren<BulletSpawner>().transform;
        owner = GetComponentInParent<Rigidbody>();
	}
	
	// Update is called once per frame
	protected virtual void Update () {
        if (player.GetButtonDown("Basic Attack")) {
            // Get the bullet spawner object
            GameObject p = Instantiate(projectile, bulletSpawner.position, bulletSpawner.rotation);
            p.transform.SetParent(bulletSpawner);

            // Propel the bullet
            Rigidbody rb = p.GetComponent<Rigidbody>();
            rb.velocity = Vector3.Project(owner.velocity, transform.forward);
            rb.AddForce(p.transform.forward * speed);

            // Destroy after we travel the given distance
            p.GetComponent<Projectile>().DestroyIn = range / speed;
        }
    }
}
