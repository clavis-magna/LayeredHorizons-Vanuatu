using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class readImage : MonoBehaviour {

    public Texture2D map;
    public GameObject unitCube;
    public GameObject HeightMapParent;
    public int scaleX = 2000;
    public int scaleY = 4000;

    // Use this for initialization
    void Start () {
        //print(map.width + "  " + map.height);
        float latCount = -17;
        float longCount = 162;
            
        for (int i = 0; i < map.width; i++)
        {
            
            for (int j = 0; j < map.height; j++)
            {
                //print(map.GetPixel(i, j));
                if (map.GetPixel(i, j).r != 0)
                {
                    float[] thisXY = helpers.getXYPos(latCount, longCount, scaleX, scaleY);
                    GameObject thisCube = Instantiate(unitCube, new Vector3(thisXY[0], map.GetPixel(i, j).r * 15, thisXY[1]), Quaternion.Euler(0, 0, 0));
                    thisCube.transform.parent = HeightMapParent.transform;
                }

                latCount = latCount + 0.01f;
                if (j == 0)
                {
                    longCount = longCount + 0.01f;
                    latCount = -17;
                    print("resetting lat");
                }

            }

        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
