using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Not to be confused with ChangeTextSize. 
/// Similarly to LanguageModeHandler, this script is meant to go on every applicable scene where the user's text size of choice is supposed to be applied.
/// 
/// Written by Andy (rmfandyplayz)
/// </summary>
public class TextSizeHandler : MonoBehaviour
{
    static List<TextMeshProUGUI> textList;

    private void Start()
    {
        TextMeshProUGUI[] tempList = FindObjectsOfType<TextMeshProUGUI>();
        textList = new List<TextMeshProUGUI>(tempList);
    }

    /// <summary>
    /// Run this along with initialization scenes.
    /// This modifies the font size of all applicable fonts in the scene by whatever is in the global settings.
    /// </summary>
    public static void ApplyTextModifier()
    {
        foreach(TextMeshProUGUI text in textList)
        {
            text.fontSize += _GLOBALSETTINGS.fontSizeModifier;
        }
    }
}
