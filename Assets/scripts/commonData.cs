/*
 * Useful hoding place for data we need in other places
 * so we can have a central place to set them
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class commonData : MonoBehaviour {

    // we need an array of all of the audio sources in the virtial environment
    // so we can mute and umute them later without needing to constantly find them
    // so we store it in this 'allAudio' variable
    public static AudioSource[] allAudio;

    // _mapScale accessable and setable in the inspector
    public Vector2 _mapScale;

    // mapScale accessable to all other scripts 
    // as a static variable it can only have 1 value across the entire project)
    public static Vector2 mapScale;

	// Use this for initialization
	void Start () {
        //set mapScale to the value we have set in the inspector
        mapScale = _mapScale;

        // Invoke the 'collectAudioSources()' functions after 2 seconds
        Invoke("collectAudioSources", 2);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void collectAudioSources()
    {
        // add all audiosources to the 'allAudio' arrray
        allAudio = FindObjectsOfType<AudioSource>() as AudioSource[];
    }
}
