using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


/// <summary>
/// Not to be confused with TextSizeHandler
/// This script handles the logic for exclusively the PH/TB 3_TextSize scene.
/// 
/// Written by Andy (rmfandyplayz)
/// </summary>
public class ChangeTextSize : MonoBehaviour
{
	int textSizeModifier;
	int originalTextSize;
	[SerializeField] Slider textSizeSlider;
	[SerializeField] TMP_InputField textSizeInputField;
	[SerializeField] Image transitionPanel;
	[SerializeField] TextMeshProUGUI exampleText;

	private void Start()
	{
		originalTextSize = (int) exampleText.fontSize;
		LanguageModeHandler.ApplyLanguageChoice();
		StartCoroutine(FadeOutPanel());
	}
	
	//runs when the slider is changed
	public void SetTextSizeModifierFromSlider()
	{
        exampleText.fontSize = originalTextSize + (int) textSizeSlider.value;
		textSizeModifier = (int) textSizeSlider.value;
        //textSizeInputField.text = (originalTextSize + (int)textSizeSlider.value).ToString();
    }

    //runs when the text input is changed
    public void SetTextSizeModifierFromTextField()
	{
		textSizeSlider.value = 0;
        exampleText.fontSize = originalTextSize + int.Parse(textSizeInputField.text.Trim());
		textSizeModifier = int.Parse(textSizeInputField.text.Trim()) - originalTextSize;
    }

	//runs when the user confirms their final choice
	public void ConfirmTextSizeModifier()
	{
		_GLOBALSETTINGS.fontSizeModifier = textSizeModifier;
		_GLOBALSETTINGS.SaveSettings();
		Debug.Log($"Text size modifier set to {textSizeModifier}");
		StartCoroutine(FadeInPanel());
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
