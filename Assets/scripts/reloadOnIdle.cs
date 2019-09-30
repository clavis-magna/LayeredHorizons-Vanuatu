using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class reloadOnIdle : MonoBehaviour
{
    Vector3 LastPos;
    int notMovedCount = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        LastPos = transform.position;
        StartCoroutine("setLastPos");
    }

    // Update is called once per frame
    void Update()
    {
        if(notMovedCount > 4)
        {
            SceneManager.LoadScene(0);
        }
    }

    IEnumerator setLastPos()
    {
        while (true)
        {
            print("trying");
            if (Vector3.Distance(transform.position, LastPos) > 0.005f)
            {
                print("hasMoved");
            }
            else
            {
                print("has not moved");
                notMovedCount++;
            }
            LastPos = transform.position;
            yield return new WaitForSeconds(5);
        }
    }
}
