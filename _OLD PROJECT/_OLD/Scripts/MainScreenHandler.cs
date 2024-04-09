using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScreenHandler : MonoBehaviour
{
    public Image background;
    public Button scoutButton, tenderfootButton, secondClassButton, firstClassButton, starButton, lifeButton, eagleButton;
    public ImageHandler imageHandler;

    /*
    private void Start()
    {
        MainScreen mainScreen = new MainScreen(Utility.GetScreenRatio());
        background.sprite = imageHandler.GetImage(mainScreen.backgroundImageID);

        SetUpButtons(scoutButton, mainScreen.mainScreenButtons[0]);
        SetUpButtons(tenderfootButton, mainScreen.mainScreenButtons[1]);
        SetUpButtons(secondClassButton, mainScreen.mainScreenButtons[2]);
        SetUpButtons(firstClassButton, mainScreen.mainScreenButtons[3]);
        SetUpButtons(starButton, mainScreen.mainScreenButtons[4]);
        SetUpButtons(lifeButton, mainScreen.mainScreenButtons[5]);
        SetUpButtons(eagleButton, mainScreen.mainScreenButtons[6]);
    }
    */

    void SetUpButtons(Button button, MainScreen.MainScreenButton buttonInfo)
    {
        if (imageHandler.GetImage(buttonInfo.spriteID))
        {
            button.image.sprite = imageHandler.GetImage(buttonInfo.spriteID);
            button.GetComponent<RectTransform>().localPosition = buttonInfo.buttonPos;
            button.GetComponent<RectTransform>().sizeDelta = buttonInfo.buttonSize;
        }
    }
}
