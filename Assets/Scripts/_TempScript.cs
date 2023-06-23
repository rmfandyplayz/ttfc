using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System.Collections;

public class DownloadImage : MonoBehaviour
{
    public string imageDownloadURL = "https://drive.google.com/uc?id=YOUR_IMAGE_FILE_ID";

    void Start()
    {
        //StartCoroutine(DownloadImageFromURL());
    }

    IEnumerator DownloadImageFromURL()
    {
        UnityWebRequest www = UnityWebRequest.Get(imageDownloadURL);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Image download failed");
        }
        else
        {
            string savePath = Application.persistentDataPath + "/image.png";
            File.WriteAllBytes(savePath, www.downloadHandler.data);
            Debug.Log("Image downloaded to: " + savePath);
        }
    }
}
