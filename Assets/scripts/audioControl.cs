using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class audioControl : MonoBehaviour
{
    public AudioMixerSnapshot wet;
    public AudioMixerSnapshot dry;
    public Light mainLight;

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
            mainLight.intensity = 0;
            mainLight.color = new Color(0,0,0.7f);
            isWet = true;
        }
        if(transform.position.y > 0 && isWet)
        {
            dry.TransitionTo(0.2f);
            GetComponent<AudioSource>().Stop();
            mainLight.intensity = 1;
            mainLight.color = new Color(1, 0.95f, 0.84f);
            isWet = false;
        }
    }
}
