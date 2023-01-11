using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

public class DownloadHandler : MonoBehaviour
{
    public string url = "";
    public string urlMeritBadge = "";
    string requirementsFileName = "Requirements.tsv";
    string meritBadgesFileName = "MeritBadges.tsv";

    //How many seconds should the program wait when downloading the files?
    public int maxDownloadWait = 3;

    int downloadCompleteCounter = 0;


    public GameObject startButton;
    private void Start()
    {
        //Do later: Add loading thingy
        startButton.SetActive(false);
        StartCoroutine(StartDownload());
    }


    IEnumerator StartDownload()
    {
        StartCoroutine(DownloadFile(url, requirementsFileName));
        StartCoroutine(DownloadFile(urlMeritBadge, meritBadgesFileName));
        while(maxDownloadWait > Time.time && downloadCompleteCounter < 2)
        {
            yield return null;
        }
        startButton.SetActive(true);
    }



    public IEnumerator DownloadFile(string url, string fileName)
    {
        var uwr = new UnityWebRequest(url, UnityWebRequest.kHttpVerbGET);
        uwr.timeout = maxDownloadWait;
        string dataPath = "Resources/" + fileName;
        if (Application.isEditor)
        {
            dataPath = Path.Combine("Assets", dataPath);
        }
        uwr.downloadHandler = new DownloadHandlerFile(dataPath);
        yield return uwr.SendWebRequest();
        if (uwr.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(uwr.error);
        }
        else
        {
            Debug.Log($"File successfully accessed. Saved to {dataPath}");
        }
        downloadCompleteCounter += 1;
    }

}
