using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankSelectionScript : MonoBehaviour
{
    public RectTransform scrollView;
    private void Start()
    {
        scrollView.sizeDelta= new Vector2(scrollView.sizeDelta.x, scrollView.sizeDelta.x * Screen.height/ Screen.width);
    }
    public void RankSelected(string rank)
    {
        RequirementsHandler.rank = (Requirement.Rank)System.Enum.Parse(typeof(Requirement.Rank), rank);
        ChangeSceneUniversalScript.SwitchScene("Requirement Select Screen");
    }
}
