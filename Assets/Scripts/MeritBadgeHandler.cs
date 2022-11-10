using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generates all the buttons from left to right.
/// </summary>
public class MeritBadgeHandler : MonoBehaviour
{

    public MeritBadges meritBadges;
    public MeritBadgeButton buttonPrefab;
    public Transform buttonCanvas;
    public int columns = 2;
    public float columnWidth = 350;
    public float rowHeight = 350;

    private void Start()
    {
        meritBadges.ReadData();
        SetupButtons();
    }

    void SetupButtons()
    {
        int index = 0;
        foreach(MeritBadge meritBadge in meritBadges.meritBadgesEagle)
        {
            SetupButton(index, meritBadge);
            index++;
        }

        foreach (MeritBadge meritBadge in meritBadges.meritBadges)
        {
            SetupButton(index, meritBadge);
            index++;
        }
        RectTransform content = buttonCanvas.GetComponent<RectTransform>();
        content.sizeDelta = new Vector2(content.sizeDelta.x, rowHeight/4 + rowHeight * index / columns);

    }

    void SetupButton(int index, MeritBadge meritBadge)
    {
        MeritBadgeButton newButton = Instantiate(buttonPrefab);
        newButton.transform.SetParent(buttonCanvas, false);
        int y = index / columns;
        int x = index % columns;
        newButton.GetComponent<RectTransform>().localPosition += new Vector3(x * columnWidth, -y * rowHeight);
        newButton.gameObject.SetActive(true);
        newButton.SetupButton(meritBadge);
    }

}
