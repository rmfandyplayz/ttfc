using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RequirementPanel : MonoBehaviour
{
    public TextMeshProUGUI descriptionText, answerText;
    public Image answerImage;
    public Button videoButton; //imageButton answerButton
    public float textBuffer = 50;
    string videoURL;
    RequirementButton requirementButton;
    RequirementsHandler requirementsHandler;
    Requirement requirements;

    public float Setup(RequirementButton requirementButton, Requirement requirement, RequirementsHandler requirementsHandler)
    {
        this.requirementsHandler = requirementsHandler;
        this.requirements = requirement;
        this.requirementButton = requirementButton;

        descriptionText.text = requirement.descriptionText;
        answerText.text = requirement.answer;

        descriptionText.ForceMeshUpdate();
        answerText.ForceMeshUpdate();

        answerText.rectTransform.localPosition = new Vector2(answerText.rectTransform.localPosition.x, -descriptionText.textBounds.extents.y * 2 - textBuffer);

        float dropdownLength = descriptionText.textBounds.extents.y * 2 + answerText.textBounds.extents.y * 2 + textBuffer;

        if (requirement.hasImage)
        {
            answerImage.sprite = requirementsHandler.imageHandler.GetImage(requirement.images[0]);
            if (answerImage.sprite)
            {
                Vector2 imageSize = answerImage.sprite.bounds.size;
                answerImage.rectTransform.sizeDelta = imageSize;
                answerImage.rectTransform.localPosition = new Vector3(0, -dropdownLength, 0);
                dropdownLength += imageSize.y;
            }
        }
        else
        {
            answerImage.enabled = false;
        }

        if (requirement.hasVideo)
        {
            videoURL = requirement.videoURL;
            float videoSize = videoButton.GetComponent<RectTransform>().rect.height;
            videoButton.GetComponent<Transform>().localPosition = new Vector3(0, -dropdownLength - textBuffer, 0);
            dropdownLength += videoSize + textBuffer;
        }
        else
        {
            videoButton.gameObject.SetActive(false);
        }

        string completed = PlayerPrefs.GetString(requirement.id, "no");
        requirementButton.SetCompleted(completed);

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
