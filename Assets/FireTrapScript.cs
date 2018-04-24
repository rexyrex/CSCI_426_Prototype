using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrapScript : EnvironmentObject {
    MeshRenderer mesh;
    bool isOn;
    bool grown;
    float counter;
    List<GameObject> burning;

    // Use this for initialization
    void Start () {
        burning = new List<GameObject>();
        mesh = this.GetComponent<MeshRenderer>();
        counter = 0;
        mesh.enabled = false;
        isOn = false;
        grown = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (isOn)
        {
            counter += Time.deltaTime;
            if (!grown)
            {
                float size = this.transform.localScale.x + Time.deltaTime*2;
                if (size >= 1) grown = true;
                this.transform.localScale = new Vector3(size, size, size);
            }
            Burn();
            if (counter >= 6) Revert();
        }
        else if (!grown)
        {
            float size = this.transform.localScale.x - Time.deltaTime * 2;
            if (size <= 0.1)
            {
                grown = true;
                mesh.enabled = false;
            }
            this.transform.localScale = new Vector3(size, size, size);
        }
    }

    public override void Actuate()
    {
        this.transform.localScale = new Vector3(0, 0, 0);
        isOn = true;
        grown = false;
        //"about to enable mesh"
        mesh.enabled = true;
        Debug.Log("was mesh enabled: "+ mesh.enabled);
    }

    public override void Revert()
    {
        isOn = false;
        grown = false;
    }

    public override void Switch()
    {
        if (!isOn) this.Actuate();
    }

    public void AddBurn(GameObject other)
    {
        Debug.Log("something to burn: " + other.tag);
        if (other.tag == "Player1Tag" || other.tag == "Player2Tag" || other.tag == "Boss") burning.Add(other.gameObject);
        else if (other.tag == "breakable") Destroy(other.gameObject);
    }

    public void RemoveBurn(GameObject other)
    {
        if (burning.Contains(other.gameObject)) burning.Remove(other.gameObject);
    }

    void Burn()
    {
        for(int i = 0; i < burning.Count; i++)
        {
            Debug.Log("Burning " + burning[i].tag);
            if (burning[i].tag == "Boss") burning[i].GetComponent<ChaseBossScript>().getDamaged(1500);
            else if (burning[i].tag == "Player1Tag") burning[i].GetComponent<Player1Script>().Damage(0);
            else burning[i].GetComponent<Player2Script>().Damage(0);
        }
    }
}
