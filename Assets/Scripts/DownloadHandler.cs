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
        //StartCoroutine(GetTexture("https://upload.wikimedia.org/wikipedia/commons/6/6e/Nf_knots.png"));
        //Do later: Add loading thingy
        startButton.SetActive(false);
        StartCoroutine(StartDownload());

    }


    IEnumerator StartDownload()
    {
        StartCoroutine(DownloadFile(url, requirementsFileName));
        StartCoroutine(DownloadFile(urlMeritBadge, meritBadgesFileName));
        while (maxDownloadWait > Time.time && downloadCompleteCounter < 2)
        {
            yield return null;
        }

        StartCoroutine(DownloadImages());
        while (!imagesDownloaded)
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
        else
        {
            dataPath = Application.persistentDataPath + "/" + dataPath;
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

    public ImageHandler imageHandler;
    public Requirements requirements;
    public MeritBadge meritBadge;
    bool imagesDownloaded = false;
    bool imageDownloaded = false;
    bool imageDownloadedSuccessful = false;
    IEnumerator DownloadImages()
    {
        for (int i = 0; i < 7; i += 1)
        {
            //todo initialize requirements by rank
            requirements.ReadData((Requirement.Rank)i);
            foreach (Requirement requirement in requirements.requirementsList)
            {
                if (requirement.hasImage && !imageHandler.GetImage(requirement.images[0]))
                {
                    imageDownloaded = false;
                    imageDownloadedSuccessful = false;
                    StartCoroutine(GetTexture(requirement.imageURL, requirement.images[0]));
                    while (!imageDownloaded) yield return null;
                    //if (imageDownloadedSuccessful)
                    //{
                    //    string imageName = requirement.images[0];
                    //    Debug.Log($"UTILITY GET IMAGE FILE PATH {Utility.GetImageFilePath(imageName)}");
                    //    imageHandler.AddImage(imageName, Utility.GetImageFilePath(imageName));
                    //}
                }
            }
        }
        imagesDownloaded = true;
    }

    IEnumerator GetTexture(string url, string imageName)
    {
        Debug.Log(url);
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            //notify user when download error occurs
            Debug.Log(www.error);
        }
        else
        {
            Texture2D myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            byte[] bytes = myTexture.EncodeToPNG();

            string dataPath = Utility.GetImageFilePath(imageName);
            System.IO.File.WriteAllBytes(dataPath, bytes);
            imageDownloadedSuccessful = true;
        }
        imageDownloaded = true;
    }
}
