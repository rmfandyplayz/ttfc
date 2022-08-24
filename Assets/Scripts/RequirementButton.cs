using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RequirementButton : MonoBehaviour
{
    public TextMeshProUGUI reqNumText; //Requirement Number
    public RectTransform dropDownMask;
    RequirementsHandler requirementsHandler;
    public TextMeshProUGUI descriptionText, answerText;
    public int answerLengthThreshold; //How much text can be on the answer before it becomes a button redirecting to another page with the answer.
    public Button answerButton; //When the answer length threshold is met, this button will be displayed instead.
    public Button videoButton, imageButton; //These buttons will redirect to another screen
    public float dropdownLength = 800;
    public float dropdownSpeed = 100;
    public bool buttonClickable = true;
    public bool buttonOpen = false;
    public float textBuffer = 50; //Defines the gap between the description text and the answer text


    public void Setup(Requirement requirement, int reqNum, RequirementsHandler requirementsHandler)
    {
        buttonClickable = true;
        this.requirementsHandler = requirementsHandler;
        reqNumText.text = requirement.requirementNumber;
        descriptionText.text = requirement.descriptionText;
        answerText.text = requirement.answer;
        descriptionText.ForceMeshUpdate();
        answerText.ForceMeshUpdate();
        answerText.rectTransform.localPosition = new Vector2(answerText.rectTransform.localPosition.x, -textBuffer -descriptionText.textBounds.extents.y * 2);
        dropdownLength = descriptionText.textBounds.extents.y * 2 + answerText.textBounds.extents.y * 2 + textBuffer;
        //Debug.LogError(dropdownLength);
    }
    

    public void ButtonClicked()
    {
        if (buttonClickable == true)
        {
            buttonClickable = false;
            if (buttonOpen == false)
            {
                StartCoroutine(DropDownMovement(-dropdownLength));
                buttonOpen = true;
            }
            else
            {
                StartCoroutine(DropDownMovement(dropdownLength));
                buttonOpen = false;
            }
        }
    }
    
    public IEnumerator DropDownMovement(float length)
    {
        float speed = dropdownSpeed; //We don't want to modify dropdownSpeed
        if(length < 0)
        {
            speed = -dropdownSpeed;
        }

        while(Mathf.Abs(length) > 0)
        {
            float step = speed * Time.deltaTime; //How much the button will move on a certain frame (dropdown speed)
            if (length > 0 && step > length || length < 0 && step < length)
            {
                step = length;
            }
            requirementsHandler.MoveButtons(this, step);
            length -= step;
            dropDownMask.sizeDelta -= Vector2.up * step;
            //sizeDelta is just the width and height but it's modified constrained to the pivot
            yield return null;
        }
        buttonClickable = true;
    }
}
