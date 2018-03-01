using UnityEngine;
using Rewired;

[RequireComponent(typeof(Rigidbody))]
[System.Serializable]
public class PlayerMovement : MonoBehaviour {
    /// <summary>
    /// The player identifier. Leave as -1 for the script to assign default 
    /// values.
    /// </summary>
    public int playerId = -1;

    [SerializeField]
    public float speed = -1.0f;
    public float maxSpeed;
    public float acceleration;
    public float jumpHeight;
    protected Rigidbody rb;
    [SerializeField]
    protected float rotationSpeedScale = 20.0f;

    protected Player player;

    protected virtual void Awake () {
        player = ReInput.players.GetPlayer(playerId);
    }

	protected virtual void Start () {
        rb = GetComponent<Rigidbody>();
	}


	protected virtual void Update () {
        
	}

    protected virtual void FixedUpdate () {
        Move();
        Turn();
    }

    void Move() {
        float moveHorizontal = player.GetAxis("Move Horizontal");
        float moveVertical = player.GetAxis("Move Vertical");
        //Making Movement Feel Nicer
        if (moveHorizontal > 0)
        {
            if (rb.velocity.x <= maxSpeed)
            {
                moveHorizontal *= acceleration;
            }
            else
            {
                moveHorizontal = acceleration * moveHorizontal / rb.velocity.x;
            }
        }
        if (moveHorizontal < 0)
        {
            if (rb.velocity.x >= -1 * maxSpeed)
            {
                moveHorizontal *= acceleration;
            }
            else
            {
                moveHorizontal = -1 * acceleration * moveHorizontal / rb.velocity.x;
            }
        }

        if (moveVertical > 0)
        {
            if (rb.velocity.z <= maxSpeed)
            {
                moveVertical *= acceleration;
            }
            else
            {
                moveVertical = acceleration * moveVertical / rb.velocity.z;
            }
        }
        if (moveVertical < 0)
        {
            if (rb.velocity.z >= -1 * maxSpeed)
            {
                moveVertical *= acceleration;
            }
            else
            {
                moveVertical = -1 * acceleration * moveVertical / rb.velocity.z;
            }
        }

        MoveInDirection(moveHorizontal, 0.0f, moveVertical);
    }

    /// <summary>
    /// Moves the player character along the given direction vector using the
    /// rigidbody. Wrapper for MoveInDirection(Vector3 direction)
    /// </summary>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    /// <param name="z">The z coordinate.</param>
    /// <param name="pullIntensity">The pull intensity. (optional, defaults to -1)</param>
    protected virtual void MoveInDirection(float x, float y, float z, float pullIntensity = -1.0f) {
        MoveInDirection(new Vector3(x, y, z), pullIntensity);
    }

    /// <summary>
    /// Moves the player character along the given direction vector using the
    /// rigidbody.
    /// </summary>
    /// <param name="direction">The Direction.</param>
    /// <param name="pullIntensity">The pull intensity. (optional, defaults to -1)</param>
    protected void MoveInDirection(Vector3 direction, float pullIntensity = -1.0f) {
        if (speed < 0.0f)
            throw new System.ArgumentNullException("speed has not been set to " +
                                                   "a value in movement script");

        if (pullIntensity < 1.0f)
        {
            GlobalDataController gdc = GlobalDataController.gdc;
            direction += (gdc.p2pos - gdc.p1pos) / 100 * pullIntensity;
        }

        rb.AddForce(speed * direction);
    }

    void Turn() {
        var target = new Vector3(player.GetAxis("Turn X"), 0.0f, player.GetAxis("Turn Y"));
        target += transform.position;
        TurnToward(target);
    }

    /// <summary>
    /// Turns the player character toward a given point using the rigidbody.
    /// </summary>
    /// <param name="target">The target point.</param>
    protected void TurnToward(Vector3 target) {
        Vector3 targetDirection = target - transform.position;
        Vector3 forward = transform.forward;
        Vector3 localTarget = transform.InverseTransformPoint(target);

        float angle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;

        Vector3 eulerAngleVelocity = rotationSpeedScale * new Vector3(0, angle, 0);
        Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }
}
