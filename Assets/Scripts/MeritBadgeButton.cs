using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Goes on the button objects and makes changes based on the information given.
/// </summary>
public class MeritBadgeButton : MonoBehaviour
{
    public TextMeshProUGUI meritBadgeName;
    public Image meritBadgeImage;
    public Button button;

    public Sprite eagleImage, nonEagleImage, selectedImage;
    public ImageHandler imageHander;

    MeritBadge meritBadge;


    public void SetupButton(MeritBadge meritBadge)
    {
        this.meritBadge = meritBadge;
        meritBadgeName.text = meritBadge.name;

        meritBadgeImage.sprite = imageHander.GetImage(meritBadge.imageName);

        if (meritBadge.eagleRequired)
        {
            button.image.sprite = eagleImage;
        }
        else
        {
            button.image.sprite = nonEagleImage;
        }

    }

}
