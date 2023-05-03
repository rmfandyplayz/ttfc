using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenuScript : MonoBehaviour
{
    public string welcomeScreen = "Welcome Screen", rankSelectionScreen = "Rank Selection Screen";
    public string websiteURL = "https://www.toewrlefence.com/bsa-app";
    public string formsURL = "https://forms.gle/TcwJZwNFUmNhY2pd9";
    public GameObject phoneCanvas;
    public GameObject ipadCanvas;
    public Button iphoneEnter;
    public Button ipadEnter;
    public DownloadHandler downloadHandler;
    public ChangeSceneUniversalScript changeSceneUniversalScript;


    private void Start()
    {
        int refreshRate = Screen.currentResolution.refreshRate;
        Debug.Log($"Refresh Rate: {refreshRate}Hz");
        Application.targetFrameRate = refreshRate;
        changeSceneUniversalScript = GetComponent<ChangeSceneUniversalScript>();
        Utility.ScreenRatios screenRatio = Utility.GetScreenRatio();
        if (screenRatio == Utility.ScreenRatios.ipadLandScp)
        {
            phoneCanvas.SetActive(false);
            ipadCanvas.SetActive(true);
            downloadHandler.startButton = ipadEnter.gameObject;
        }
        else
        {
            phoneCanvas.SetActive(true);
            ipadCanvas.SetActive(false);
            downloadHandler.startButton = iphoneEnter.gameObject;
        }
        downloadHandler.StartDownloading();
    }


    public void EnterButtonClick()
    {
        //add welcome screen check thingy
        if (!PlayerPrefs.HasKey("returning") && false) //change later??!
        {
            ChangeSceneUniversalScript.SwitchScene(welcomeScreen);
        }
        else
        {
            ChangeSceneUniversalScript.SwitchScene(rankSelectionScreen);
        }
    }

    public void WebsiteButton()
    {
        Application.OpenURL(websiteURL);
    }

    public void FormsButton()
    {
        Application.OpenURL(formsURL);
    }
}
