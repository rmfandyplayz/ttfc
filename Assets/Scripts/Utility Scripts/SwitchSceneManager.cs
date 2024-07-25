using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchSceneManager : MonoBehaviour
{
    /// <summary>
    /// Dedicated scene switcher for TTFC - switches to the correct scene based on deviceMode from global settings
    /// 
    /// Written by Andy (rmfandyplayz)
    /// </summary>
    /// <param name="sceneName"></param>
    public static void SwitchScenes(string sceneName)
    {
        //don't wanna use ternary operator lmao
        if(_GLOBALSETTINGS.deviceMode == 0)
        {
            SceneManager.LoadScene("PH" + sceneName);
        }
        else if(_GLOBALSETTINGS.deviceMode == 1)
        {
            SceneManager.LoadScene("TB" + sceneName);
        }
    }
}
