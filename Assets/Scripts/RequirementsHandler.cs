using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.RestService;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RequirementsHandler : MonoBehaviour
{
    //This script decides which buttons to display

    public Requirements requirements;
    public RequirementButton prefabRequirementButton;
    public static Requirement.Rank rank;
    List<RequirementButton> requirementButtons = new List<RequirementButton>();
    public Transform requirementButtonCanvas;
    public RectTransform content;
    public ImageHandler imageHandler;
    private float initialOffset;
    public float offset;
    public float contentSize;
    public TextMeshProUGUI rankText;

    private void Start()
    {
        initialOffset = prefabRequirementButton.GetComponent<RectTransform>().localPosition.y;
        rankText.text = GetRankText();
        SetUpRequirementButton();
    }

    public string GetRankText()
    {
        switch (rank)
        {
            case Requirement.Rank.S:
                return "Scout";
            case Requirement.Rank.T:
                return "Tenderfoot";
            case Requirement.Rank.SC:
                return "Second Class";
            case Requirement.Rank.F:
                return "First Class";
            case Requirement.Rank.ST:
                return "Star";
            case Requirement.Rank.L:
                return "Life";
            case Requirement.Rank.E:
                return "Eagle";
            default:
                return "Invalid";
        }
    }


    public void SetUpRequirementButton()
    {
        requirements.ReadData(rank);
        SetupRequirementButtons();
    }

    private void SetupRequirementButtons()
    {
        int reqNum = 1;
        float x = prefabRequirementButton.GetComponent<RectTransform>().localPosition.x;
        foreach (Requirement requirement in requirements.requirements)
        {
            RequirementButton newButton = Instantiate(prefabRequirementButton);
            newButton.transform.SetParent(requirementButtonCanvas, false);
            newButton.gameObject.SetActive(true);
            requirementButtons.Add(newButton);
            newButton.GetComponent<RectTransform>().localPosition = new Vector3(x, initialOffset -(offset * (reqNum - 1)), 0);
            newButton.Setup(requirement, reqNum, this);
            reqNum++;
        }
        contentSize = initialOffset - (offset * (reqNum - 1));
        ResizeContent();
    }

    public void MoveButtons(RequirementButton buttonStart, float step) //How much the button is being changed (step)
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
        contentSize += step;
        ResizeContent();
    }

    public void ResizeContent()
    {
        content.sizeDelta = new Vector2(content.sizeDelta.x, -contentSize);
    }

    public void RequirementCompleted(string reqID, bool completed)
    {
        PlayerPrefs.SetString(reqID, completed? "yes" : "no");
    }
}
