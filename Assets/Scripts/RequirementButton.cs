using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RequirementButton : MonoBehaviour
{
    public RectTransform dropdownMask;
    public TextMeshProUGUI reqNumText, descriptionText, answerText; //Requirement Number
    RequirementsHandler requirementsHandler;
    public float dropdownLength = 800;
    public float dropdownSpeed = 100;
    public int answerLengthThreshold = 50;
    public bool buttonClickable = true;
    private bool buttonOpen = false;

    public Button answerButton, videoButton, imageButton;
    public float textBuffer = 50;
    public void Setup(Requirement requirement, int reqNum, RequirementsHandler requirementsHandler)
    {
        buttonClickable = true;
        this.requirementsHandler = requirementsHandler;
        reqNumText.text = requirement.requirementNumber;

        descriptionText.text = requirement.descriptionText;
        answerText.text = requirement.answer;

        descriptionText.ForceMeshUpdate();
        answerText.ForceMeshUpdate();

        answerText.rectTransform.localPosition = new Vector2(answerText.rectTransform.localPosition.x, -descriptionText.textBounds.extents.y * 2 - textBuffer);

        dropdownLength = descriptionText.textBounds.extents.y * 2 + answerText.textBounds.extents.y * 2 + textBuffer;


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

    public IEnumerator DropDownMovement(float length)
    {
        float lengthMax = length;
        //x^2

        while (Mathf.Abs(length) > 0)
        {
            float x = Mathf.Abs(length / lengthMax) - 1;
            float speed = dropdownSpeed;
            if (length < 0)
            {
                speed = -dropdownSpeed;
            }

            float step = speed * Time.deltaTime;
            if (length > 0 && step > length || length < 0 && step < length)
            {
                step = length;
            }
            requirementsHandler.MoveButtons(this, step);
            length -= step;
            dropdownMask.sizeDelta -= Vector2.up * step;
            yield return null;
        }
        buttonClickable = true;
    }
}