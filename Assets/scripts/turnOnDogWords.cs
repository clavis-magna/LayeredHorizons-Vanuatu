// this is a quick test
// specific to the dog words but will become more generic

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnOnDogWords : MonoBehaviour
{
    public GameObject dogParent;
    public bool isOn = true;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "index_pointer")
        {
            if (isOn)
            {
                dogParent.SetActive(false);
                isOn = false;
            }
            else
            {
                dogParent.SetActive(true);
                isOn = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == "index_pointer")
        {

        }
    }

}
