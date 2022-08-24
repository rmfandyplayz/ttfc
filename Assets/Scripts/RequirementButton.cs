using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RequirementButton : MonoBehaviour
{
    public TextMeshProUGUI reqNumText; //Requirement Number
    RequirementsHandler requirementsHandler;
    public float dropdownLength = 800;
    public float dropdownSpeed = 100;
    public bool buttonClickable = true;

    public void Setup(Requirement requirement, int reqNum, RequirementsHandler requirementsHandler)
    {
        buttonClickable = true;
        this.requirementsHandler = requirementsHandler;
        reqNumText.text = requirement.ToString();
    }
    

    public void ButtonClicked()
    {
        if (buttonClickable == true)
        {
            StartCoroutine(DropDownMovement(-dropdownLength));
            buttonClickable = false;
        }
        else
        {
            StartCoroutine(DropDownMovement(dropdownLength));
            buttonClickable = true;
        }
    }
    
    public IEnumerator DropDownMovement(float length)
    {
        float speed = dropdownSpeed;
        if(length < 0)
        {
            speed = -dropdownSpeed;
        }

        while(Mathf.Abs(length) > 0)
        {
            float step = speed * Time.deltaTime;
            if (length > 0 && speed > length || length < 0 && step < length)
            {
                step = length;
            }
            requirementsHandler.MoveButtons(this, step);
            length -= step;
            yield return null;
        }
        
    }
}
