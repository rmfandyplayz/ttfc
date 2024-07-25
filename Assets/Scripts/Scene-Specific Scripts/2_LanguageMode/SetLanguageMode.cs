using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Not to be confused with LanguageModeHandler
/// Handles the logic for the scenes 2_LanguageMode (both iPad and iPhone)
/// 
/// Written by Andy (rmfandyplayz)
/// </summary>
public class SetLanguageMode : MonoBehaviour
{
    [SerializeField] Image transitionPanel;

    private void Start()
    {
        StartCoroutine(FadeOutPanel());
    }

    //pretty much used for just that one button in the scene
    public void SetLangMode(int languageMode)
    {
        if(languageMode == 0 || languageMode == 1)
        {
            _GLOBALSETTINGS.languageMode = languageMode;
            _GLOBALSETTINGS.SaveSettings();
            Debug.Log($"Language mode set to {(languageMode == 0 ? "Regular English" : "Skibidi Speak")}");
            StartCoroutine(FadeInPanel());
        }
        else
        {
            Debug.Log("[SetLanguageMode.cs] Invalid input!");
        }
    }

    //alpha goes from 0 to 1 (transparent to opaque)
    IEnumerator FadeInPanel()
    {
        transitionPanel.gameObject.SetActive(true);
        Color tempColor = transitionPanel.color;
        tempColor.a = 0;
        transitionPanel.color = tempColor;

        for (int i = 0; i < 50; i++)
        {
            tempColor.a += 0.02f;
            transitionPanel.color = tempColor;
            yield return new WaitForSeconds(0.01f);
        }
        tempColor.a = 1;
        transitionPanel.color = tempColor;
        SwitchSceneManager.SwitchScenes("3_TextSize");
    }

    //alpha goes from 1 to 0 (opaque to transparent)
    IEnumerator FadeOutPanel()
    {
        Color tempColor = transitionPanel.color;
        tempColor.a = 1;

        transitionPanel.color = tempColor;

        for (int i = 0; i < 50; i++)
        {
            tempColor.a -= 0.02f;
            transitionPanel.color = tempColor;
            yield return new WaitForSeconds(0.01f);
        }
        tempColor.a = 0;
        transitionPanel.color = tempColor;
        transitionPanel.gameObject.SetActive(false);
    }
}
