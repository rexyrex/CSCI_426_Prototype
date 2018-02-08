using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenericPlayerScript : MonoBehaviour {

    public float currentHealth;
    public float maxHealth;
    public float defaultHealth;

    public Slider manaBar;

    public Slider healthBar;


	// Use this for initialization
	void Start () {
        currentHealth = defaultHealth;
        
	}
	
	// Update is called once per frame
	void Update () {
        manaBar.value = GlobalDataController.gdc.currentMana/100;

        healthBar.value = currentHealth / 100;

    }


    void OnCollisionEnter(Collision col)
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

        if (col.gameObject.tag == "ManaTag")
        {
            GlobalDataController.gdc.currentMana += 20;
            Destroy(col.gameObject);
        }
    }

    public void Damage(float value)
    {
        currentHealth -= value;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
