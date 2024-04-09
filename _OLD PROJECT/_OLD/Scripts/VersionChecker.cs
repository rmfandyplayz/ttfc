using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;

/// <summary>
/// The function ur looking for is prob <b>CheckVersion()</b>
/// </summary>
public class VersionChecker : MonoBehaviour
{
    //the url that goes to the version number thing
    string versionURL = "https://version.ttfc.zip";
    bool? isOutdated;

    public UnityEvent<bool> OnVersionCheckComplete = new UnityEvent<bool>();

    private void Start()
    {
        if (InternetAvailability.IsConnectedToInternet() == true)
        {
            StartCoroutine(AccessVersionWebsite());
        }
    }

    /// <summary>
    /// Accesses the website and reads the version or smth idfk
    /// </summary>
    /// <returns></returns>
    IEnumerator AccessVersionWebsite()
    {
        UnityWebRequest www = UnityWebRequest.Get(versionURL);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
            isOutdated = false;
        }
        else
        {
            //in this context we just have a plain text website (like 1 line) so we just trim everything and leave us with some version number
            string latestVersion = www.downloadHandler.text.Trim();
            Debug.Log($"Latest app version from server: {latestVersion}");
            if(CompareVersions(latestVersion) == true)
            {
                isOutdated = false;
            }
            else
            {
                isOutdated = true;
            }
        }
        OnVersionCheckComplete.Invoke(isOutdated.GetValueOrDefault());
        www.Dispose();
    }
    
    /// <summary>
    /// Returns <b>false</b> if <b>versions do not match.</b>
    /// </summary>
    /// <param name="latestVersion"></param>
    /// <returns></returns>
    bool CompareVersions(string latestVersion)
    {
        string currentVersion = Application.version;
        return currentVersion.Equals(latestVersion);
    }

    /// <summary>
    /// Returns true if the app is outdated.
    /// </summary>
    /// <returns></returns>
    public bool? IsAppOutdated()
    {
        return isOutdated;
    }

}
