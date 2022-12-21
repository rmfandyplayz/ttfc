/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneUniversalScript : MonoBehaviour
{
    public enum SceneNames
    {
        mainMenu, welcomeScreen, mainScreen, requirementSelectScreen, meritBadgeList
    }


    public static SceneNames GetSceneName(string sceneName)
    {
        switch (sceneName)
        {
            case "Main Menu":
                return SceneNames.mainMenu;
            case "Welcome Screen":
                return SceneNames.welcomeScreen;
            case "Main Screen":
                return SceneNames.mainScreen;
            case "MeritBadgeList":
                return SceneNames.meritBadgeList;   
            case "Requirement Select Screen":
                return SceneNames.requirementSelectScreen;
            default:
                return (SceneNames)System.Enum.Parse(typeof(SceneNames), sceneName);
        }
    }

    public static void SwitchScene(string targetScene)
    {
        SceneManager.LoadScene(GetSceneName(GetSceneName(targetScene)));
    }

    public static void SwitchScene(SceneNames sceneName)
    {
        SceneManager.LoadScene(GetSceneName(sceneName));
    }

    public static string GetSceneName(SceneNames sceneName)
    {
        switch (sceneName)
        {
            case SceneNames.mainMenu:
                if (Utility.GetScreenRatio() == Utility.ScreenRatios.ipadLandScp)
                {
                    return "Main Menu";
                }
                else
                {
                    return "Main Menu";
                }
            case SceneNames.welcomeScreen:
                if (Utility.GetScreenRatio() == Utility.ScreenRatios.ipadLandScp)
                {
                    return "Welcome Screen";
                }
                else
                {
                    return "Welcome Screen";
                }
            case SceneNames.mainScreen:
                if (Utility.GetScreenRatio() == Utility.ScreenRatios.ipadLandScp)
                {
                    Debug.Log("Pass");
                    return "Main Screen";
                }
                else
                {
                    return "Main Screen Phone Version";
                }
            case SceneNames.requirementSelectScreen:
                if (Utility.GetScreenRatio() == Utility.ScreenRatios.ipadLandScp)
                {
                    return "Requirement Select Screen";
                }
                else
                {
                    return "Requirement Select Screen";
                }
            case SceneNames.meritBadgeList:
                if (Utility.GetScreenRatio() == Utility.ScreenRatios.ipadLandScp)
                {
                    return "MeritBadgeList";
                }
                else
                {
                    return "MeritBadgeList";
                }
            default:
                return "Main Menu";
        }
    }
}
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneUniversalScript : MonoBehaviour
{
    public enum SceneNames
    {
        MainMenu,
        WelcomeScreen,
        MainScreen,
        RequirementSelectScreen,
        MeritBadgeList
    }


    public static void SwitchScene(string targetScene)
    {

        SceneManager.LoadScene(GetSceneName(GetSceneName(targetScene)));
    }

    public static void SwitchScene(SceneNames sceneName)
    {

        SceneManager.LoadScene(GetSceneName(sceneName));
    }

    public static SceneNames GetSceneName(string sceneName)
    {
        switch (sceneName)
        {
            case "Main Menu":
                return SceneNames.MainMenu;
            case "Welcome Screen":
                return SceneNames.WelcomeScreen;
            case "Main Screen":
                return SceneNames.MainScreen;
            case "Requirement Select Screen":
                return SceneNames.RequirementSelectScreen;
            case "MeritBadgeList":
                return SceneNames.MeritBadgeList;
            default:
                return (SceneNames)System.Enum.Parse(typeof(SceneNames), sceneName);
        }
    }

    public static string GetSceneName(SceneNames sceneName)
    {
        switch (sceneName)
        {
            case SceneNames.MainMenu:
                if (Utility.GetScreenRatio() == Utility.ScreenRatios.ipadLandScp)
                {
                    return "Main Menu";
                }
                else
                {
                    return "Main Menu";
                }
            case SceneNames.WelcomeScreen:
                if (Utility.GetScreenRatio() == Utility.ScreenRatios.ipadLandScp)
                {
                    return "Welcome Screen";
                }
                else
                {
                    return "Welcome Screen";
                }
            case SceneNames.MainScreen:
                if (Utility.GetScreenRatio() == Utility.ScreenRatios.ipadLandScp)
                {
                    return "Main Screen";
                }
                else
                {
                    return "Main Screen Phone Version";
                }
            case SceneNames.RequirementSelectScreen:
                if (Utility.GetScreenRatio() == Utility.ScreenRatios.ipadLandScp)
                {
                    return "Requirement Select Screen";
                }
                else
                {
                    return "Requirement Select Screen";
                }
            case SceneNames.MeritBadgeList:
                if (Utility.GetScreenRatio() == Utility.ScreenRatios.ipadLandScp)
                {
                    return "MeritBadgeList";
                }
                else
                {
                    return "MeritBadgeList";
                }
            default:
                return "Main Menu";
        }
    }
}

