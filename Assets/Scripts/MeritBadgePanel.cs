using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MeritBadgePanel : MonoBehaviour
{
    public TextMeshProUGUI meritBadgeName, text;
    public GameObject meritBadgePanel;
    private MeritBadge meritBadge;
    public Button pamphletPDFbtn, pamphletBuybtn, workbookbtn;



    public void DisplayPanel(MeritBadge meritBadge)
    {
        this.meritBadge = meritBadge;
        meritBadgeName.text = meritBadge.name;
        DisplayBulletPoints();
        meritBadgePanel.SetActive(true);
        if(meritBadge.pamphletpdfURL == "none")
        {
            pamphletPDFbtn.interactable = false;
        }
        else
        {
            pamphletPDFbtn.interactable = true;
        }
        if(meritBadge.pamphletbuyURL == "none")
        {
            pamphletBuybtn.interactable = false;
        }
        else
        {
            pamphletBuybtn.interactable = true;
        }
        if(meritBadge.workbookURL == "none")
        {
            workbookbtn.interactable = false;
        }
        else
        {
            workbookbtn.interactable = true;
        }
    }

    public void DisplayBulletPoints()
    {
        string displayText = "";
        //displayText += TPM_MarkupBullets('�' + meritBadge.description);
        displayText += meritBadge.description;
        foreach (string tip in meritBadge.tips)
        {
            displayText += "\n" + TPM_MarkupBullets('�' + tip);
            //use "�"
        }

        text.text = displayText;
    }

    public string TPM_MarkupBullets(string rawText, char findBullet = '�', float indent = 1f, float leftMargin = 1f)
    {
        bool isInBullet = false;
        string markedup = String.Format("<margin-left={0}em>", leftMargin);
        string indentLeftTag = String.Format("<indent={0}em>", indent), indentRightTag = "</indent>";
        foreach (char c in rawText)
        {
            if (c == findBullet) { markedup += findBullet + indentLeftTag; isInBullet = true; }
            else if (isInBullet && c == (char)10) { markedup += indentRightTag + c; isInBullet = false; }
            else { markedup += c; }
        }
        if (isInBullet) markedup += indentRightTag;
        markedup += "</margin>";
        return markedup;
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