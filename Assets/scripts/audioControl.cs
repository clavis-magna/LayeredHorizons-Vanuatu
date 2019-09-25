using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class audioControl : MonoBehaviour
{
    public AudioMixerSnapshot wet;
    public AudioMixerSnapshot dry;

    bool isWet = false;
    
    // Start is called before the first frame update
    void Start()
    {
        dry.TransitionTo(0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < 0 && !isWet)
        {
            wet.TransitionTo(0.2f);
            GetComponent<AudioSource>().Play();
            isWet = true;
        }
        if(transform.position.y > 0 && isWet)
        {
            dry.TransitionTo(0.2f);
            GetComponent<AudioSource>().Stop();
            isWet = false;
        }
    }
}
