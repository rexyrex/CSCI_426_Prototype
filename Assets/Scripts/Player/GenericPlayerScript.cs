using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenericPlayerScript : MonoBehaviour {
    public float forcemod;
    public float currentHealth;
    public float maxHealth;
    public float defaultHealth;

    public Slider manaBar;

    public Slider healthBar;

    protected float pullCounter;
    protected bool pulling;
    protected GameObject otherPlayer;
    protected float mass;

	// Use this for initialization
	protected virtual void Start () {
        currentHealth = defaultHealth;
        pulling = false;
        pullCounter = 0;
        mass = this.GetComponent<Rigidbody>().mass;
	}
	
	// Update is called once per frame
	protected virtual void Update () {
        manaBar.value = GlobalDataController.gdc.currentMana/100;
        healthBar.value = currentHealth / 100;
        if(pullCounter<20)pullCounter += Time.deltaTime;
    }


    protected void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "SphereTag")
        {
            Damage(20);
            Destroy(col.gameObject);
        }

        if (col.gameObject.tag == "Sphere2Tag")
        {
            Damage(10);
            Destroy(col.gameObject);
        }

		if (col.gameObject.tag == "Enemy")
		{
			Damage(10);
			Destroy(col.gameObject);
		}

        if (col.gameObject.tag == "ManaTag")
        {
            GlobalDataController.gdc.currentMana += 30;
            Destroy(col.gameObject);
        }

        if(col.gameObject.tag == "Player2Tag" || col.gameObject.tag == "Player1Tag")
        {
            if(pulling && otherPlayer.GetComponent<GenericPlayerScript>().IsPulling())
            {
                Debug.Log("Explode");
            }

            pulling = false;
        }
    }

    public virtual void Damage(float value)
    {
        
    }

    public bool IsPulling()
    {
        return pulling;
    }

    public void Pulled()
    {
        pulling = false;
    }
}
