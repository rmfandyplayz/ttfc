using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using UnityEditor.Profiling.Memory.Experimental;

public class RequirementPanel : MonoBehaviour
{
    public TextMeshProUGUI descriptionText, answerText;
    public Image answerImage;
    public Button videoButton; //imageButton answerButton
    public float textBuffer = 50;
    public int maxWidth = 500;
    string videoURL;
    RequirementButton requirementButton;
    RequirementsHandler requirementsHandler;
    Requirement requirements;
    public RectTransform content;


    public float minContentLength = 1562.8f;
    /// <summary>
    /// This is used for the iPad only.
    /// </summary>
    /// <param name="length"></param>
    public void SetContentPanelLength(float length)
    {
        if(length < minContentLength)
        {
            length = minContentLength;
        }
        content.sizeDelta = new Vector2(content.sizeDelta.x, length);
    }


    public float Setup(RequirementButton requirementButton, Requirement requirement, RequirementsHandler requirementsHandler)
    {
        this.requirementsHandler = requirementsHandler;
        this.requirements = requirement;
        this.requirementButton = requirementButton;

        descriptionText.text = requirement.descriptionText;
        answerText.text = requirement.answer;

        descriptionText.ForceMeshUpdate();
        answerText.ForceMeshUpdate();

        answerText.rectTransform.anchoredPosition = new Vector2(answerText.rectTransform.localPosition.x, -descriptionText.textBounds.extents.y * 2 - textBuffer);

        float dropdownLength = descriptionText.textBounds.extents.y * 2 + answerText.textBounds.extents.y * 2 + textBuffer * 2;

        if (requirement.hasImage)
        {
            answerImage.sprite = requirementsHandler.imageHandler.GetImage(requirement.images[0]);
            if(answerImage.rectTransform.sizeDelta.x > maxWidth)
            {
                int width = (int)GetComponent<RectTransform>().sizeDelta.x;
                int height = (int)GetComponent<RectTransform>().sizeDelta.y;
                
                answerImage.rectTransform.sizeDelta = new Vector2(maxWidth, maxWidth * height / width);
            }
            if (answerImage.sprite)
            {
                Vector2 imageSize = answerImage.sprite.bounds.size;
                if(imageSize.x > maxWidth)
                {
                    imageSize = new Vector2(maxWidth, maxWidth * imageSize.y / imageSize.x);
                }
                answerImage.rectTransform.sizeDelta = imageSize;
                answerImage.rectTransform.anchoredPosition = new Vector3(0, -dropdownLength, 0);
                dropdownLength += imageSize.y;
                answerImage.enabled = true;
            }
            else answerImage.enabled = false;
        }
        else
        {
            answerImage.enabled = false;
        }

        if (requirement.hasVideo)
        {
            videoURL = requirement.videoURL;
            float videoSize = videoButton.GetComponent<RectTransform>().rect.height;
            videoButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -dropdownLength - textBuffer, 0);
            dropdownLength += videoSize + textBuffer;
            videoButton.gameObject.SetActive(true);
        }
        else
        {
            videoButton.gameObject.SetActive(false);
        }

        string completed = PlayerPrefs.GetString(requirement.id, "no");
        requirementButton.SetCompleted(completed);

        SetCompleted(completed);
        dropdownLength += 87.5f;
        return dropdownLength;
    }

    public void VideoButtonClicked()
    {
        Application.OpenURL(videoURL);
    }


    //bool isComplete;
    public void ToggleClick(bool isOn)
    {
        if (requirementButton && isOn != requirementButton.completed)
        {
            requirementButton.completed = isOn;
            requirementsHandler.RequirementCompleted(requirements.id, isOn);
            if (isOn) SetCompleted("yes");
            else SetCompleted("no");
        }
    }

    public Toggle doneToggle;
    public void SetCompleted(string completed)
    {
        if (completed == "no")
        {
            doneToggle.isOn = false;
        }
        else
        {
            doneToggle.isOn = true;
        }
        requirementButton.SetCompleted(completed);
    }

}
