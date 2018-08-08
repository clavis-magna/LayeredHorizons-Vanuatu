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
        if (other.name == "index_pointer")
        {
            Material cubeMaterial = this.gameObject.GetComponent<Renderer>().material;
            cubeMaterial.color = new Color(1, 0, 0);
            this.gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = true;
            LeanTween.scale(this.gameObject.transform.GetChild(0).gameObject, new Vector3(1, 1, 1), 0.5f).setEase(LeanTweenType.easeInOutCirc);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == "index_pointer")
        {
            Material cubeMaterial = this.gameObject.GetComponent<Renderer>().material;
            cubeMaterial.color = new Color(1, 1, 1);
            Invoke("hide",0.3f);
            LeanTween.scale(this.gameObject.transform.GetChild(0).gameObject, new Vector3(0,0,0), 0.3f).setEase(LeanTweenType.linear);
        }
    }

    void hide()
    {
        this.gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = false;
    }
}
