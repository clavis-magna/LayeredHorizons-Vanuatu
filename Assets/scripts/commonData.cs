using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class commonData : MonoBehaviour {

    public static AudioSource[] allAudio;

	// Use this for initialization
	void Start () {
        Invoke("collectAudioSources", 2);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void collectAudioSources()
    {
        // add all audiosources to arrray
        allAudio = FindObjectsOfType<AudioSource>() as AudioSource[];
    }
}
