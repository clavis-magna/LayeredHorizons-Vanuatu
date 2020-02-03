using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// Ali making an edit :) 

public class readLocationData : MonoBehaviour
{
    // file name of location json data - must be in streaming assets folder
    public string filename = "locationData.json";

    [Header("GO to represent single location on the map")]
    public GameObject populatedPlaceMarker;

    [Header("GO to represent capital city")]
    public GameObject namedLocationMarker;
    // public GameObject precipCube;
    // public TextMesh textOnCube;
    // public LineRenderer line1;

    // holders for the map scale set in start method
    private int scaleX; 
    private int scaleY; 

    // array to read json location data into
    private locationData[] mylocationData;

    // Use this for initialization
    void Start()
    {
        // grab world scale from the commonData script
        // set in the inspector
        scaleX = (int)commonData.mapScale.x;
        scaleY = (int)commonData.mapScale.y;

        loadData();
    }

    private void loadData()
    {
        // create the file path to the json data as a string
        string dataFilePath = Path.Combine(Application.streamingAssetsPath, filename);

        // error check (make sure there actually is a file
        if (File.Exists(dataFilePath))
        {
            // read the data in
            string dataAsJson = File.ReadAllText(dataFilePath);

            // format the data into the 'loadedData' array using JsonHelper
            locationData[] loadedData = JsonHelper.FromJson<locationData>(dataAsJson);

            // todo: lineIndex is unused - remove completely
            // int lineIndex = 0;

            for (int i = 0; i < loadedData.Length; i++)
            {
                // In the dat NT refers to 'name type'
                // N: approved (The BGN-approved local official name for a geographic feature)
                // V: Variant: A former name, name in local usage, or other spelling found on various sources.
                // for our current purpouse using the 'N' version only if a 'V' exists ignore it
                // todo: This avoids doubles, but we should look at the data and see if this is the approapriate way to deal with this data
                if (loadedData[i].NT == "N")
                {
                    // convert from lat/long to world units
                    // using the helper method in the 'helpers' script
                    float[] thisXY = helpers.getXYPos(loadedData[i].lat, loadedData[i].lon, scaleX, scaleY);

                    // check no other point exists here as a quick and dirty way of avoiding overlap
                    // todo: we should eventually base this on a heirachy of location size rating
                    bool somethingInMySpot = false;
                    if (Physics.CheckSphere(new Vector3(thisXY[0], 0, thisXY[1]), 0.09f))
                    {
                        somethingInMySpot = true;
                    }

                    // in the data dsg refers to DSG: Feature Designation Code
                    // PPL: populated place
                    // PPLQ: populated place abandoned
                    // PPLC: capital
                    // PPLA: first oder administrative division

                    // PPL & PPLQ are displayed using the populatedPlaceMarker gameObject
                    if (loadedData[i].dsg != "PPLC" && loadedData[i].dsg != "PPLA" && !somethingInMySpot)
                    {
                        GameObject thisCube = Instantiate(populatedPlaceMarker, new Vector3(thisXY[0], 0, thisXY[1]), Quaternion.Euler(90, 0, 0));
                        TextMesh nameText = thisCube.GetComponentInChildren<TextMesh>();
                        nameText.text = loadedData[i].fullnamero;
                        //lineIndex++;
                    }
                    // PPLC & PPLA are displayed namedLocationMarker above a populatedPlaceMarker whose color is changed to pink
                    // todo: This pink color needs changing as it is confusing to users between these markers and the language location markers
                    // which are currently the same color
                    else if (loadedData[i].dsg == "PPLC" || loadedData[i].dsg == "PPLA")
                    {
                        // I don't think we want a cube here, just the populated place marker
                        //GameObject thisCube = Instantiate(populatedPlaceMarker, new Vector3(thisXY[0], 0, thisXY[1]), Quaternion.Euler(90, 0, 0));
                        //Material cubeMaterial = thisCube.GetComponent<Renderer>().material;
                        //cubeMaterial.color = new Color(1, 0, 0.8666f);
                        GameObject thisMarker = Instantiate(namedLocationMarker, new Vector3(thisXY[0], 0.12f, thisXY[1]), Quaternion.Euler(-90, 0, 0));
                        print(loadedData[i].fullnamero);
                        TextMesh nameText = thisMarker.GetComponentInChildren<TextMesh>();
                        nameText.text = loadedData[i].fullnamero;
                        thisMarker.transform.name = loadedData[i].fullnamero;
                        //lineIndex++;
                    }
                }
            }

           // print("line index is: " + lineIndex);
        }
    }
}







