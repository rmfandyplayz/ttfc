using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class RequirementButton : MonoBehaviour
{
    public RectTransform dropdownMask;
    public TextMeshProUGUI reqNumText, descriptionText, answerText; //Requirement Number
    RequirementsHandler requirementsHandler;
    Requirement requirements;
    public AnimationCurve easeInQuad;
    public AnimationCurve easeOutQuad;
    public float dropdownLength = 800;
    public float dropdownSpeed = 100;
    public int answerLengthThreshold = 50;
    public bool buttonClickable = true;
    private bool buttonOpen = false;

    public Sprite completedIcon, incompleteIcon;
    public Image completedImage;
    public bool completed;

    public Image answerImage;

    public Button answerButton, videoButton, imageButton;
    public float textBuffer = 50;

    private void Start()
    {
        dropdownMask.sizeDelta = new Vector2(dropdownMask.sizeDelta.x, 0);
    }


    public void Setup(Requirement requirement, int reqNum, RequirementsHandler requirementsHandler)
    {
        buttonClickable = true;
        this.requirementsHandler = requirementsHandler;
        this.requirements = requirement;
        reqNumText.text = requirement.requirementNumber;

        descriptionText.text = requirement.descriptionText;
        answerText.text = requirement.answer;

        descriptionText.ForceMeshUpdate();
        answerText.ForceMeshUpdate();

        answerText.rectTransform.localPosition = new Vector2(answerText.rectTransform.localPosition.x, -descriptionText.textBounds.extents.y * 2 - textBuffer);

        dropdownLength = descriptionText.textBounds.extents.y * 2 + answerText.textBounds.extents.y * 2 + textBuffer;

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
        SetCompleted(completed);

        dropdownLength += 87.5f;
    }


    public void ButtonClicked()
    {
        if (buttonClickable == true)
        {
            buttonClickable = false;
            if (!buttonOpen)
            {
                buttonOpen = true;
                StartCoroutine(DropDownMovement(-dropdownLength));
            }
            else
            {
                buttonOpen = false;
                StartCoroutine(DropDownMovement(dropdownLength));
            }


        }

    }

    string videoURL;
    public void VideoButtonClicked()
    {
        Application.OpenURL(videoURL);
    }


    public IEnumerator DropDownMovement(float length)
    {
        float lengthMax = length;
        //x^2
        GetComponent<Button>().interactable = false;
        while (Mathf.Abs(length) > 0)
        {
            //Objective: Use the curve variable name to determine the speed that the function should go in
            float x = Mathf.Abs((-length + lengthMax) / lengthMax); //When moving from left to right, x determines how far you have moved. (Time)
            float speed = 0;
            if (lengthMax < 0)
            {
                speed = -dropdownSpeed * easeOutQuad.Evaluate(x);
            }
            else
            {
                speed = dropdownSpeed * easeInQuad.Evaluate(x);
            }
            float step = speed * Time.deltaTime;
            if (length > 0 && step > length || length < 0 && step < length)
            {
                step = length;
            }
            requirementsHandler.MoveButtons(this, step); //smth wrong here (null reference exception)
            length -= step;
            dropdownMask.sizeDelta -= Vector2.up * step;
            yield return null;
        }
        buttonClickable = true;
        GetComponent<Button>().interactable = true;
    }

    public Toggle doneToggle;
    public void SetCompleted(string completed)
    {
        if(completed == "no")
        {
            completedImage.sprite = incompleteIcon;
            this.completed = false;
            doneToggle.isOn = false;
        }
        else
        {
            completedImage.sprite = completedIcon;
            this.completed = true;
            doneToggle.isOn = true;
        } 
    }

    bool isComplete;
    public void ToggleClick(bool isOn)
    {
        if(isOn != completed)
        {
            completed = isOn;
            requirementsHandler.RequirementCompleted(requirements.id, isOn);
            if (isOn) SetCompleted("yes");
            else SetCompleted("no");
        }
    }
}