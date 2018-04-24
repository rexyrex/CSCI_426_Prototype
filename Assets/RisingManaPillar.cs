using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingManaPillar : BreakScript {
    float mana = 30;
    bool grown;
    float fullSize;
    float growTime = 2;
    float startheight;
    int id;
    public GameObject explosion;
    public Color[] mats;
    GlobalDataController.ChainDistance killDist;

    private void Awake()
    {
        this.transform.position = this.transform.position - new Vector3(0, 30*this.transform.localScale.y, 0);
        startheight = this.transform.position.y;
    }

    // Use this for initialization
    void Start()
    {
        grown = false;
        fullSize = 30 * this.transform.localScale.y;
        id = Random.Range(0, mats.Length);
        if (id > 2) id = 2;
        if (id == 0) killDist = GlobalDataController.ChainDistance.Close;
        else if (id == 1) killDist = GlobalDataController.ChainDistance.Medium;
        else if (id == 2) killDist = GlobalDataController.ChainDistance.Far;
        this.GetComponent<MeshRenderer>().material.color = mats[id];
    }

    // Update is called once per frame
    void Update()
    {
        if (!grown)
        {
            this.transform.Translate(Vector3.up * Time.deltaTime * 10);
            if (this.transform.position.y >= startheight + fullSize) grown = true;
        }
    }

    // Break the object
    public override void Break()
    {
        if(GlobalDataController.gdc.chainState == killDist)
        {
            Instantiate(explosion);
            Destroy(this.gameObject);
        }
    }
}

