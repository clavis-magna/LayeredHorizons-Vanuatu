using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class readLanguageData : MonoBehaviour
{
    // file name of language json data - must be in streaming assets folder
    public string filename = "languageData.json";

    // game objects for building environment
    [Header("GO represents single language location")]
    public GameObject languageCentroidMarker;
    [Header("GO represents language bounding box")]
    public GameObject languageBoundsCube;
    [Header("GO represents language with related audio")]
    public GameObject audioIcon;
    [Header("GO represents audio bounds in environment")]
    public GameObject ring;

    // holders for the map scale set in start method
    private int scaleX; 
    private int scaleY;

    // array to read json language data into
    private languageData[] mylanguageData;

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
        //file path
        string dataFilePath = Path.Combine(Application.streamingAssetsPath, filename);

        if (File.Exists(dataFilePath))
        {
            string dataAsJson = File.ReadAllText(dataFilePath);
            languageData[] loadedData = JsonHelper.FromJson<languageData>(dataAsJson);

            for (int i = 0; i < loadedData.Length; i++)
            {
                // loanguage location marker
                float[] thisXY = helpers.getXYPos(loadedData[i].latitude, loadedData[i].longitude, scaleX, scaleY);
                GameObject thisDome = Instantiate(languageCentroidMarker, new Vector3(thisXY[0], 0.2f, thisXY[1]), Quaternion.Euler(0, 0, 0));

                // audio
                if (loadedData[i].audiofile != null)
                {
                    // audio source
                    AudioSource thesource = thisDome.GetComponent<AudioSource>();
                    thesource.clip = Resources.Load<AudioClip>("languageAudio/" + loadedData[i].audiofile);
                    thesource.maxDistance = loadedData[i].numberofspeakers / 1000 + 0.01f;
                    thesource.Play();

                    // audio icon
                    GameObject thisAudioIcon = Instantiate(audioIcon, new Vector3(thisXY[0], 0.3f, thisXY[1]), Quaternion.Euler(0, 0, 0));

                    // ring visualisation
                    GameObject ringObject = Instantiate(ring, new Vector3(thisXY[0], 0, thisXY[1]), Quaternion.Euler(-90, 0, 0));
                    ringObject.transform.localScale = new Vector3(loadedData[i].numberofspeakers / 1000 + 0.01f, loadedData[i].numberofspeakers / 1000 + 0.01f, 0.5f);
                }
                else
                {
                    // if no audio destroy the audiosource to save resources
                    AudioSource thesource = thisDome.GetComponent<AudioSource>();
                    Destroy(thesource);
                }

                // limits / bounding box
                float[] NWLimits = helpers.getXYPos(loadedData[i].northlimit, loadedData[i].westlimit, scaleX, scaleY);
                float[] SELimits = helpers.getXYPos(loadedData[i].southlimit, loadedData[i].eastlimit, scaleX, scaleY);

                float theWidth = SELimits[0] - NWLimits[0]; // south - north
                float theHeight = SELimits[1] - NWLimits[1]; // east - west

                GameObject limitsCube = Instantiate(languageBoundsCube, new Vector3(thisXY[0], 0, thisXY[1]), Quaternion.Euler(0, 0, 0));
                limitsCube.transform.localScale = new Vector3(theWidth, loadedData[i].numberofspeakers/1000+0.01f, theHeight);


                // language data
                TextMesh thisTextMesh = thisDome.transform.GetChild(0).gameObject.GetComponent<TextMesh>();
                thisTextMesh.text = "Language Name: "+loadedData[i].name + "\n Country: " + loadedData[i].country + "\n Number of Speakers: " + loadedData[i].numberofspeakers; // + "\n" + hasAudio;  
            }
        }
    }
}







