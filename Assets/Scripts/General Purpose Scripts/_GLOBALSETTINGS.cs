using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// stores some essential information such as font size, accessibility options, and such
/// also the place to load and save such information
/// THIS SCRIPT SHOULD ALWAYS BE THE FIRST THING TO RUN WHEN THE APP LOADS!
/// 
/// written by Andy (rmfandyplayz)
/// </summary>
public class _GLOBALSETTINGS : MonoBehaviour
{
    public static int fontSize;
    //KEEP IN MIND FOR FUTURE:
    //this value can be adjusted to a non-zero and non-negative number, but do have recommended values!

    public static int languageMode;
    //0 = regular, patient (probably for adults and stuff)
    //1 = skibidi speak (for gen-z and above, or those who have no patience in reading stuff)

    public static int deviceMode;
    //tells the app how to display stuff
    //0 = phone
    //1 = tablet

    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        //uncomment this to delete all stored PlayerPrefs (testing purposes only!)

        LoadSettings();
        PrintSettings();

        if(completedAppIntro == 0)
        {
            SceneManager.LoadScene("1_CheckScreenResolution"); //go to the start of app intro
        }
        else if(completedAppIntro == 1)
        {
            SceneManager.LoadScene("Test Scene"); //replace with the actual name where user is supposed to go
        }
    }

    //loads the playerprefs settings
    void LoadSettings()
    {
        //for these, the second value is the default value it will fallback to!
        fontSize = PlayerPrefs.GetInt("FontSize", 14);
        languageMode = PlayerPrefs.GetInt("LanguageMode", 0);
        deviceMode = PlayerPrefs.GetInt("DeviceMode", 0);
        //add more if necessary

        //put scene specific stuff under here
        completedAppIntro = PlayerPrefs.GetInt("CompletedAppIntro", 0);
    }

    /// <summary>
    /// If at any point you modified the instance fields in this script, <b><u>you should call this function to actually save them so they can be loaded later.</u></b>
    /// </summary>
    public static void SaveSettings()
    {
        PlayerPrefs.SetInt("FontSize", fontSize);
        PlayerPrefs.SetInt("LanguageMode", languageMode);
        PlayerPrefs.SetInt("DeviceMode", deviceMode);
        //add more if necessary

        //put scene specific stuff under here
        PlayerPrefs.SetInt("CompletedAppIntro", completedAppIntro);

        PlayerPrefs.Save();
    }

    //prints all the current settings
    public static void PrintSettings()
    {
        Debug.Log("Settings: ");
        Debug.Log("Finished App Intro: " + (completedAppIntro == 0 ? "no" : "yes"));
        Debug.Log("Font Size: " + fontSize);
        Debug.Log("Language Mode: " + languageMode);
        Debug.Log("DeviceMode: " + deviceMode);
        //add more if necessary
    }


    //variables that are used for very little purposes outside of their respective scenes go here
    #region Scene Specific Settings

    public static int completedAppIntro = 0;
    //basically, skips all the scenes in "_InitizliationScenes" folder
    //because playerprefs is stupid asf and doesn't allow SetBool(), use 0 and 1 instead. 0 = false, 1 = true

    #endregion
}
