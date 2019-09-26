using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

public class readGenericData : MonoBehaviour
{
    // list to hold data
    List<Dictionary<string, object>> data;

    // csv filename
    // in streaming assets (include .csv extension)
    public string CSVFileName = "data.csv";

    public bool displayWord;
    public string wordColumnHeader;

    public bool hasAudio;
    public string audioColunmHeader;

    public bool runTest = false;


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

        // this is a test for reading in data file in using webrequest from the streaming assets folder
        if (runTest)
        {
            StartCoroutine("getData");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator getData()
    {
        // test for text asset from streaming assets
        string myFileURI = Path.Combine(Application.streamingAssetsPath, "cat.txt");

        UnityWebRequest www = UnityWebRequest.Get(myFileURI);
        yield return www.SendWebRequest();

        if (!www.isNetworkError && !www.isHttpError)
        {
            // Get text content like this:
            Debug.Log(www.downloadHandler.text);
        }

        // test for audio asset from streaming assets
        string audioFileURI = Path.Combine(Application.streamingAssetsPath, "bird.ogg");

        UnityWebRequest audiowww = UnityWebRequestMultimedia.GetAudioClip(audioFileURI, AudioType.OGGVORBIS);
        yield return audiowww.SendWebRequest();

        if (!audiowww.isNetworkError && !audiowww.isHttpError)
        {
            AudioClip myClip = DownloadHandlerAudioClip.GetContent(audiowww);
            print("audioFileName --------------" + myClip);
        }

        // test for image asset from streaming assets
        string imageFileURI = Path.Combine(Application.streamingAssetsPath, "dog.jpg");

        UnityWebRequest imagewww = UnityWebRequestTexture.GetTexture(imageFileURI);
        yield return imagewww.SendWebRequest();

        if (!imagewww.isNetworkError && !imagewww.isHttpError)
        {
            var texture = DownloadHandlerTexture.GetContent(imagewww);
            print("image --------------" + texture);
        }
    }
}
