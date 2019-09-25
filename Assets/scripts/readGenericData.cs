using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class readGenericData : MonoBehaviour
{
    // list to hold data
    List<Dictionary<string, object>> data;

    // csv filename
    // in streaming assets (include .csv extension)
    public string CSVFileName = "data.csv";

    public bool displayWord;
    public string wordColumnHeader;

    public GameObject textMarker;

    // holders for the map scale set in start method
    private int scaleX;
    private int scaleY;

    // Start is called before the first frame update
    void Start()
    {
        // grab world scale from the commonData script
        // set in the inspector
        scaleX = (int)commonData.mapScale.x;
        scaleY = (int)commonData.mapScale.y;

        // create the file path to the json data as a string
        string dataFilePath = Path.Combine(Application.streamingAssetsPath, CSVFileName);
        // error check (make sure there actually is a file
        if (File.Exists(dataFilePath))
        {
            data = CSVReader.Read(dataFilePath);

            // test read of data
            for (var i = 0; i < data.Count; i++)
            {
                //print("dog " + data[i]["dog"] + " " + "language " + data[i]["language"]);
                if (displayWord)
                {
                    // convert from lat/long to world units
                    // using the helper method in the 'helpers' script
                    float[] thisXY = helpers.getXYPos((float)data[i]["latitude"], (float)data[i]["longitude"], scaleX, scaleY);
                    GameObject thisMarker = Instantiate(textMarker, new Vector3(thisXY[0], 0.05f, thisXY[1]), Quaternion.Euler(-90, 0, 0));
                    TextMesh nameText = thisMarker.GetComponentInChildren<TextMesh>();
                    nameText.text = (string)data[i]["dog"];
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
