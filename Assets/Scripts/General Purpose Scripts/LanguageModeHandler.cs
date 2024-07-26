using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Not to be confused with SetLanguageMode
/// Similarly to TextSizeHandler, this script is meant to go on every applicable scene where the user's language style of choice is meant to be applied.
/// 
/// Written by Andy (rmfandyplayz)
/// </summary>
public class LanguageModeHandler : MonoBehaviour
{

	static List<TextMeshProUGUI> textList;
	static string divider = "*-=-=-=-=-*"; //the divider that will separate the normal english/skibidi speak

	private void Start()
	{
		TextMeshProUGUI[] tempList = FindObjectsOfType<TextMeshProUGUI>();
		textList = new List<TextMeshProUGUI>(tempList);
	}

	/// <summary>
	/// Run this along with initialization scenes.
	/// Similarly to TextSizeHandler's function, it modifies all the applicable text within the scene to display the correct language type.
	/// </summary>
	public static void ApplyLanguageChoice()
	{
		foreach(TextMeshProUGUI text in textList)
		{
			if(text.text.Contains(divider)) //if the text does not contain the divider, it will be ignored.
			{
				string originalText = text.text;
				string cleanedText = originalText.Replace("\n", "").Replace("\r", ""); //remove newlines
				string[] splitText = cleanedText.Split(divider);

				if (_GLOBALSETTINGS.languageMode == 0) //normal english
				{
					if (splitText.Length > 0)
					{
						text.text = splitText[0];
					}
				}
				else if (_GLOBALSETTINGS.languageMode == 1) //skibidi speak
				{
					if (splitText.Length > 1)
					{
						text.text = splitText[1];
					}
				}
			}
		}
	}
}
