using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

public class DownloadHandler : MonoBehaviour
{
    public string url = "https://docs.google.com/spreadsheets/d/e/2PACX-1vQkvEAYQkQDerOMWoMJozsDV6B4NUCr9SbIoAdenO4uqCdeNZiXgms5InXmFgdqQCmUFvObrDq_heY3/pubhtml?gid=0&single=true";
    public string urlMeritBadge = "";
    string requirementsFileName = "Requirements.tsv";
    string meritBadgesFileName = "MeritBadges.tsv";
    private void Start()
    {
        StartCoroutine(DownloadFile(url, "RequirementsTest.tsv"));
    }

    /*
    IEnumerator StartDownload()
    {
        StartCoroutine(DownloadFile())
    }
    */


    public IEnumerator DownloadFile(string url, string fileName)
    {
        var uwr = new UnityWebRequest(url, UnityWebRequest.kHttpVerbGET);
        string dataPath = "Resources" + fileName;
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
    }

}
