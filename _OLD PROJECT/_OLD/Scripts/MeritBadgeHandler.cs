using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generates all the buttons from left to right.
/// </summary>
public class MeritBadgeHandler : MonoBehaviour
{

    public MeritBadges meritBadges;
    MeritBadgeButton buttonPrefab;
    public MeritBadgeButton buttonIphonePrefab, buttonIpadPrefab;
    public Transform buttonCanvas;
    public int columns = 2, ipadLandColumns = 6, iphoneColumns = 2;
    public float columnWidth = 350;
    public float rowHeight = 350;
    public float bottomBuffer = 45;
    public bool displayEagle = false;

    private void Start()
    {
        SetColumns();
        meritBadges.ReadData();
        SetupButtons();
    }

    void SetColumns()
    {
        Utility.ScreenRatios screenRatio = Utility.GetScreenRatio();
        switch (screenRatio)
        {
            case Utility.ScreenRatios.ipadLandScp:
                columns = ipadLandColumns;
                buttonPrefab = buttonIpadPrefab;
                break;
            case Utility.ScreenRatios.ipadPort:
                columns = ipadLandColumns;
                buttonPrefab = buttonIpadPrefab;
                break;
            case Utility.ScreenRatios.iphonePort:
                columns = iphoneColumns;
                buttonPrefab = buttonIphonePrefab;
                break;
        }
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
        content.sizeDelta = new Vector2(content.sizeDelta.x, bottomBuffer + rowHeight * Mathf.Ceil((float) index / columns));

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
