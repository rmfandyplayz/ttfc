using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneUniversalScript : MonoBehaviour
{
    public float animationWaitTime = 0.5f; //how long to wait 4 the animation to be over before switching scenes
    public Animator sceneTransition; //the animator storing that scene transition thingy

    public enum SceneNames
    {
        MainMenu,
        WelcomeScreen,
        MainScreen,
        RequirementSelectScreen,
        MeritBadgeList
    }

    private void Start()
    {
        sceneTransition = GetComponentInChildren<Animator>();
        if (sceneTransition == null) //if we can't find the animation within the parents, find it in the children.
        {
            sceneTransition = GetComponentInParent<Animator>();
        }
        if(sceneTransition == null) //if we still can't find it, this will be the last resort.
        {
            sceneTransition = GetComponent<Animator>();
        }
    }

    private void OnEnable() //backup option just in case Start does not work
    {
        sceneTransition = GetComponentInChildren<Animator>();
        if (sceneTransition == null) //if we can't find the animation within the parents, find it in the children.
        {
            sceneTransition = GetComponentInParent<Animator>();
        }
        if (sceneTransition == null) //if we still can't find it, this will be the last resort.
        {
            sceneTransition = GetComponent<Animator>();
        }
    }

    public static void SwitchScene(string targetScene)
    {
        ChangeSceneUniversalScript changeSceneUniversalScript = FindObjectOfType<ChangeSceneUniversalScript>();
        changeSceneUniversalScript.StartCoroutine(changeSceneUniversalScript.StartSceneTransition(targetScene));
    }

    //i think this is unused lmao prob delete later idk
    public static void SwitchScene(SceneNames sceneName)
    {
        SceneManager.LoadScene(GetSceneName(sceneName));
    }

    /// <summary>
    /// This coroutine handles the scene transitions.
    /// </summary>
    /// <returns></returns>
    public IEnumerator StartSceneTransition(string targetScene)
    {
        Debug.Log("Started Scene Transition");
        sceneTransition.SetTrigger("AnimationStart");
        yield return new WaitForSeconds(.5f); //change this to the variable animationWaitTime later on?
        SceneManager.LoadScene(GetSceneName(GetSceneName(targetScene)));
        Debug.Log("Ended Scene Transition");
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
                    return "Requirement Select Screen Phone Version";
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

