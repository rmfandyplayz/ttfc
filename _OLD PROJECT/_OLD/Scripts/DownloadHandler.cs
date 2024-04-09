using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

public class DownloadHandler : MonoBehaviour
{
    public string url = "";
    public string urlMeritBadge = "";
    public string urlMainScreenData = "https://docs.google.com/spreadsheets/d/e/2PACX-1vRuQ_4qxuy3GLUfgLZd2MWlu4S5aCGhTUYp8Ljbbuq5c9WeKxlV3CpaLed6fzKyeMa5YcG8Qqb7-Ef9/pub?output=tsv";
    public string mainScreenFileName = "MainScreenData.tsv";
    string requirementsFileName = "Requirements.tsv";
    string meritBadgesFileName = "MeritBadges.tsv";

    //How many seconds should the program wait when downloading the files?
    public int maxDownloadWait = 3;
    float downloadStart; //Variable to keep track of when the download started so we can keep track of how much time passed before reading downloaded files. This is useful to prevent the IO conflict error.

    int downloadCompleteCounter = 0;

    public GameObject loadingCircle;
    public GameObject startButton;

    /// <summary>
    /// Checks if there is internet. If there isn't internet, don't download shid. If there is internet, download stuff.
    /// </summary>
    public void StartDownloading()
    {
        //check to see if there's internet. if there is internet, download stuff. if there isn't, don't download.
        if(InternetAvailability.IsConnectedToInternet() == true)
        {
            startButton.SetActive(false);
            loadingCircle.SetActive(true);
            StartCoroutine(StartDownload());
        }
        else
        {
            startButton.SetActive(true);
            loadingCircle.SetActive(false);
        }
        
    }

    /// <summary>
    /// <b>COROUTINE</b>
    /// Downloads the necessary files (aka the requirements)
    /// </summary>
    IEnumerator StartDownload()
    {
        downloadStart = Time.time;
        StartCoroutine(DownloadFile(url, requirementsFileName));
        //StartCoroutine(DownloadFile(urlMeritBadge, meritBadgesFileName));
        //StartCoroutine(DownloadFile(urlMainScreenData, mainScreenFileName));
        while (maxDownloadWait + downloadStart > Time.time && downloadCompleteCounter < 3)
        {
            yield return null;
        }

        
        StartCoroutine(DownloadImages());
        while (!imagesDownloaded)
        {
            yield return null;
        }
        

        loadingCircle.SetActive(false);
        startButton.SetActive(true);
    }

    
    /// <summary>
    /// Downloads a singular file based on the URL given. Saves the file to the desired place.
    /// </summary>
    /// <param name="url"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
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
    /// <summary>
    /// Downloads images (yeah)
    /// </summary>
    /// <returns></returns>
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
                }
            }
        }


        /*
        //download buttons for main screen
        MainScreen mainScreen = new MainScreen(Utility.GetScreenRatio());
        if (!imageHandler.GetImage(mainScreen.backgroundImageID))
        {
            imageDownloaded = false;
            imageDownloadedSuccessful = false;
            StartCoroutine(GetTexture(mainScreen.backgroundURL, mainScreen.backgroundImageID));
            while (!imageDownloaded)
            {
                yield return null;
            }
        }
        foreach(MainScreen.MainScreenButton button in mainScreen.mainScreenButtons)
        {
            if (button.spriteURL != "spriteURL" && !imageHandler.GetImage(button.spriteID))
            {
                imageDownloaded = false;
                imageDownloadedSuccessful = false;
                StartCoroutine(GetTexture(button.spriteURL, button.spriteID));
                while (!imageDownloaded)
                {
                    yield return null;
                }
            }
        }
        */


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
