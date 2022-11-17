using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MeritBadgePanel : MonoBehaviour
{
    public TextMeshProUGUI meritBadgeName, text;
    public GameObject meritBadgePanel;
    private MeritBadge meritBadge;

    public void DisplayPanel(MeritBadge meritBadge)
    {
        this.meritBadge = meritBadge;
        meritBadgeName.text = meritBadge.name;
        DisplayBulletPoints();
        meritBadgePanel.SetActive(true);
    }

    public void DisplayBulletPoints()
    {
        string displayText = "";
        displayText += meritBadge.description;
        foreach(string tip in meritBadge.tips)
        {
            displayText += "\n" + tip;
            //use "•"
        }

        text.text = displayText;
    }

    public void BackButton()
    {
        meritBadgePanel.SetActive(false);
    }

    public void PamphletPDFButton()
    {
        Application.OpenURL(meritBadge.pamphletpdfURL);
    }

    public void PamphletBuyButton()
    {
        Application.OpenURL(meritBadge.pamphletbuyURL);
    }

    public void WorkbookButton()
    {
        Application.OpenURL(meritBadge.workbookURL);
    }

}
