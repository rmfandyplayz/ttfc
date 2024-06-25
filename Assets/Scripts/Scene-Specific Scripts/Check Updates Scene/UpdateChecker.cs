using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;

public class UpdateChecker : MonoBehaviour
{
    string versionURL = "https://version.ttfc.zip";
    bool? isOutdated;

    public UnityEvent<bool> OnVersionCheckComplete = new UnityEvent<bool>();

    private void Start()
    {
        if(InternetAvailability.IsConnectedToInternet() == true)
        {
            StartCoroutine(AccessVersionURL());
        }
        else
        {
            //not connected to internet prompt thing
            //put it here later lol
        }
    }

    /// <summary>
    /// Accesses https://version.ttfc.zip to check what's on there.
    /// </summary>
    /// <returns></returns>
    IEnumerator AccessVersionURL()
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
            string latestVersion = www.downloadHandler.text.Trim();
            Debug.Log($"Latest app version from server: {latestVersion}");
            if (CompareVersions(latestVersion) == true)
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
    /// Returns false if versions do not match.
    /// Returns true otherwise.
    /// </summary>
    /// <param name="latestVersion"></param>
    /// <returns></returns>
    bool CompareVersions(string latestVersion)
    {
        string currentVersion = Application.version;
        return currentVersion.Equals(latestVersion);
    }

    /// <summary>
    /// A getter function - returns true if app is outdated.
    /// </summary>
    /// <returns></returns>
    public bool? IsAppOutdated()
    {
        return isOutdated;
    }
}
