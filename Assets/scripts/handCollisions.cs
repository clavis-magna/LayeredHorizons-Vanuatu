using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handCollisions : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        print(other.name);
        Material cubeMaterial = this.gameObject.GetComponent<Renderer>().material;
        cubeMaterial.color = new Color(1, 0, 0);
        this.gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = true;
    }

    void OnTriggerExit(Collider other)
    {
        print(other.name);
        Material cubeMaterial = this.gameObject.GetComponent<Renderer>().material;
        cubeMaterial.color = new Color(1, 1, 1);
        this.gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = false;
    }
}
