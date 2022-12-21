using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenuScript : MonoBehaviour
{
    public string welcomeScreen = "Welcome Screen", rankSelectionScreen = "Rank Selection Screen";

    public void EnterButtonClick()
    {
        //add welcome screen check thingy
        if (!PlayerPrefs.HasKey("returning")) //change later??!
        {
            ChangeSceneUniversalScript.SwitchScene(welcomeScreen);
        }
        else
        {
            ChangeSceneUniversalScript.SwitchScene(rankSelectionScreen);
        }
    }
}
