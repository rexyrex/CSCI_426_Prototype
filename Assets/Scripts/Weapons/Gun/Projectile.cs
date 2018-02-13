using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody rb;

    float destroyIn;
    public float DestroyIn
    {
        get 
        {
            return destroyIn;
        }

        set
        {
            destroyIn = value;
            Invoke("DestroyThis", destroyIn);
        }
    }

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnCollisionEnter(Collision collision) {
        rb.freezeRotation = false;
        rb.useGravity = true;
        CancelInvoke("DestroyThis");
        DestroyIn = destroyIn;
    }

    void DestroyThis() {
        Destroy(gameObject);
    }
}
