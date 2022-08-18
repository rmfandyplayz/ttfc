using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequirementsHandler : MonoBehaviour
{
    public Requirements requirements;
    public RequirementButton prefabRequirementButton;
    List<RequirementButton> requirementButtons = new List<RequirementButton>();
    private float initialOffset;
    public float offset;
    public Transform requirementButtonCanvas;

    private void Start()
    {
        initialOffset = prefabRequirementButton.GetComponent<RectTransform>().localPosition.y;
    }


    public void SetUpRequirementButton(Requirements requirements)
    {
        this.requirements = requirements;
        SetupRequirementButtons();
    }

    private void SetupRequirementButtons()
    {
        int reqNum = 1;
        foreach (Requirement requirement in requirements.requirements)
        {
            RequirementButton newButton = Instantiate(prefabRequirementButton);
            newButton.transform.SetParent(requirementButtonCanvas, false);
            newButton.gameObject.SetActive(true);
            requirementButtons.Add(newButton);
            newButton.GetComponent<RectTransform>().localPosition = new Vector3(0, initialOffset -(offset * (reqNum - 1)), 0);
            newButton.Setup(requirement, reqNum, this);
            reqNum++;
        }
    }

    public void MoveButtons(RequirementButton buttonStart, float step)
    {
        bool moving = false;
        for(int i = 0; i < requirements.requirements.Count; i++)
        {
            if(buttonStart == requirementButtons[i])
            {
                moving = true;
            }
            else if (moving)
            {
                requirementButtons[i].GetComponent<RectTransform>().localPosition += step * Vector3.up;
            }
        }
    }
}
