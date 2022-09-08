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
    public AnimationCurve easeInQuad;
    public AnimationCurve easeOutQuad;
    public float dropdownLength = 800;
    public float dropdownSpeed = 100;
    public int answerLengthThreshold = 50;
    public bool buttonClickable = true;
    private bool buttonOpen = false;

    public Image answerImage;

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
        GetComponent<Button>().interactable = false;
        while (Mathf.Abs(length) > 0)
        {
            //Objective: Use the curve variable name to determine the speed that the function should go in
            float x = Mathf.Abs((-length + lengthMax) / lengthMax);
            float speed = 0;
            if (lengthMax < 0)
            {
                speed = -dropdownSpeed * easeOutQuad.Evaluate(x);
                Debug.Log($"Speed: {speed} (easeOutQuad)");
            }
            else
            {
                speed = dropdownSpeed * easeInQuad.Evaluate(x);
                Debug.Log($"Speed: {speed} (easeOutQuad)");
            }
            Debug.Log($"X: {x}");
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
}