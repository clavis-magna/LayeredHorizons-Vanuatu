using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;

public class leapNav : MonoBehaviour {

    Controller controller;
    public float normalMoveSpeed = 0.5f;
    public float riseSpeed = 0.5f;

    // Use this for initialization
    void Start () {
        controller = new Controller();
    }
	
	// Update is called once per frame
	void Update () {
        Frame frame = controller.Frame();
        if (frame.Hands.Count > 0)
        {
            // palm up or down
            bool palmUp;
            if (frame.Hands[0].PalmNormal[0] > -0.5 && frame.Hands[0].PalmNormal[0] < 0.5 && frame.Hands[0].PalmNormal[1] < 0)
            {
                palmUp = true;
            }
            else
            {
                palmUp = false;
            }

            //print(frame.Hands[0]);
            if (frame.Hands[0].IsRight)
            {
                //how many extended fingers
                int extendedFingers = 0;
                for (var f = 0; f < frame.Hands[0].Fingers.Count; f++)
                {
                    var finger = frame.Hands[0].Fingers[f];
                    if (finger.IsExtended)
                    {
                        extendedFingers++;
                    }
                }

                if (frame.Hands[0].Fingers[1].IsExtended && extendedFingers == 1)
                {
                    //print("right index finger extended");
                    transform.position += Camera.main.transform.forward * normalMoveSpeed * Time.deltaTime;
                }
                else if(frame.Hands[0].Fingers[0].IsExtended && extendedFingers == 1)
                {
                    transform.position += Camera.main.transform.up * riseSpeed * Time.deltaTime;
                }
                else if(extendedFingers == 5 && palmUp)
                {
                    transform.position += -Camera.main.transform.up * riseSpeed * Time.deltaTime;
                }
                else if (extendedFingers == 5 && !palmUp)
                {
                    transform.position += Camera.main.transform.forward * normalMoveSpeed * 4 * Time.deltaTime;
                }
            }
          
        }
    }
}
