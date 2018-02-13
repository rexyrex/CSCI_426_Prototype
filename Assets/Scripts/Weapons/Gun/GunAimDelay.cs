using UnityEngine;

/// <summary>
/// Do we have a delay and setup for this gun? (e.g., a sniper rifle should have
/// an aiming delay in which we draw a line to the targeted PC)
/// </summary>
[RequireComponent (typeof(LineRenderer))]
public class GunAimDelay : MonoBehaviour {
    public float aimDelayTime = 0.5f;
    public LineRenderer aimDelayRenderer;

	// Use this for initialization
	protected virtual void Start () {
		
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		
	}
}
