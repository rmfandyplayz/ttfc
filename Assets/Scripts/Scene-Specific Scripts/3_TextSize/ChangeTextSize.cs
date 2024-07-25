using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


/// <summary>
/// Not to be confused with TextSizeHandler
/// This script handles the logic for exclusively the PH/TB 3_TextSize scene.
/// 
/// Written by Andy (rmfandyplayz)
/// </summary>
public class ChangeTextSize : MonoBehaviour
{
    [SerializeField] Image transitionPanel;

    private void Start()
    {
        LanguageModeHandler.ApplyLanguageChoice();
        FadeOutPanel();
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
        SceneManager.LoadScene("Test Scene"); //change later to SwitchSceneManager
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