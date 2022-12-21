using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankSelectionScript : MonoBehaviour
{
    public void RankSelected(string rank)
    {
        RequirementsHandler.rank = (Requirement.Rank)System.Enum.Parse(typeof(Requirement.Rank), rank);
        ChangeSceneUniversalScript.SwitchScene("Requirement Select Screen");
    }
}
