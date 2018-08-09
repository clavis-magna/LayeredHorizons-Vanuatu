using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class readLocationData : MonoBehaviour
{

    public string filename = "locationData.json";
    public GameObject dataCube;
    public GameObject namedLocationMarker;
    // public GameObject precipCube;
    // public TextMesh textOnCube;
    // public LineRenderer line1;


    public int scaleX = 1000;
    public int scaleY = 2000;

    private locationData[] mylocationData;

    // Use this for initialization
    void Start()
    {
        loadData();
    }

    private void loadData()
    {
        //file path
        string dataFilePath = Path.Combine(Application.streamingAssetsPath, filename);

        if (File.Exists(dataFilePath))
        {
            string dataAsJson = File.ReadAllText(dataFilePath);
            locationData[] loadedData = JsonHelper.FromJson<locationData>(dataAsJson);

            int lineIndex = 0;

            for (int i = 0; i < loadedData.Length; i++)
            {
                if (loadedData[i].NT == "N")
                {
                    float[] thisXY = helpers.getXYPos(loadedData[i].lat, loadedData[i].lon, scaleX, scaleY);

                    // check no other point exists here as a quick and dirty way of avoiding overlap
                    // this should be based on a heirachy of location size rating

                    bool somethingInMySpot = false;
                    if (Physics.CheckSphere(new Vector3(thisXY[0], 0, thisXY[1]), 0.09f))
                    {
                        somethingInMySpot = true;
                        //print(somethingInMySpot);
                    }
                    

                    if (loadedData[i].dsg != "PPLC" && loadedData[i].dsg != "PPLA" && !somethingInMySpot)
                    {
                        GameObject thisCube = Instantiate(dataCube, new Vector3(thisXY[0], 0, thisXY[1]), Quaternion.Euler(90, 0, 0));
                        TextMesh nameText = thisCube.GetComponentInChildren<TextMesh>();
                        nameText.text = loadedData[i].fullnamero;
                        lineIndex++;
                    }
                    else if(loadedData[i].dsg == "PPLC" || loadedData[i].dsg == "PPLA")
                    {
                        GameObject thisCube = Instantiate(dataCube, new Vector3(thisXY[0], 0, thisXY[1]), Quaternion.Euler(90, 0, 0));
                        Material cubeMaterial = thisCube.GetComponent<Renderer>().material;
                        cubeMaterial.color = new Color(1, 0, 0.8666f);
                        GameObject thisMarker = Instantiate(namedLocationMarker, new Vector3(thisXY[0], 0.1f, thisXY[1]), Quaternion.Euler(-90, 0, 0));
                        print(loadedData[i].fullnamero);
                        TextMesh nameText = thisMarker.GetComponentInChildren<TextMesh>();
                        nameText.text = loadedData[i].fullnamero;
                        thisMarker.transform.name = loadedData[i].fullnamero;
                        lineIndex++;
                    }
                }
            }

            print("line index is: " + lineIndex);
        }
    }
}







