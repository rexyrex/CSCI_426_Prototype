using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base component for guns.
/// </summary>
[System.Serializable]
public class Gun : MonoBehaviour {
    public float damage = 1.0f;
    public float range = 10.0f;

    [SerializeField]
    protected GunAimDelay gunAimDelay = null;

	// Use this for initialization
	protected virtual void Start () {
		
	}
	
	// Update is called once per frame
	protected virtual void Update () {
        
	}
}
