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
    //temp
    public bool open = false;

    public void Setup(Requirement requirement, int reqNum, RequirementsHandler requirementsHandler)
    {
        this.requirementsHandler = requirementsHandler;
        reqNumText.text = $"Req. {reqNum}";
    }
    

    public void ButtonClicked()
    {
        if (!open)
        {
            StartCoroutine(DropDownMovement(-dropdownLength));
            open = true;
        }
        else
        {
            StartCoroutine(DropDownMovement(dropdownLength));
            open = false;
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
