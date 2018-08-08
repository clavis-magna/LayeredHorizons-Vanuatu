using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;

public class showLanguageData : MonoBehaviour {

    public commonData commondata;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "palm_pointer")
        {
            this.gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = true;
            LeanTween.scale(this.gameObject.transform.GetChild(0).gameObject, new Vector3(1,1,1), 0.5f).setEase(LeanTweenType.easeInOutCirc);

            if (this.gameObject.transform.GetComponent<AudioSource>() != null)
            {
                foreach (AudioSource source in commonData.allAudio)
                {
                    source.Pause();
                }
                this.gameObject.transform.GetComponent<AudioSource>().Play();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == "palm_pointer")
        {
            LeanTween.scale(this.gameObject.transform.GetChild(0).gameObject, new Vector3(0,0,0), 0.3f).setEase(LeanTweenType.linear);
            Invoke("hide", 0.3f);

            foreach (AudioSource source in commonData.allAudio)
            {
                if (this.gameObject.transform.GetComponent<AudioSource>() != null)
                {
                    source.Play();
                }
            }
        }
    }

    void hide()
    {
        this.gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = false;
    }
}
