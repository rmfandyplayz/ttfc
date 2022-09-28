using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelBar : MonoBehaviour
{
    public Image fillImage, rankImage, nextRankImage;
    public TextMeshProUGUI rankText, nextRankText, levelPercentText;
    public Requirements requirements;
    public Sprite[] rankImages;

    private void Start()
    {
        SetLevelBar();
    }

    void SetLevelBar()
    {
        int rank = 0;
        float percentComplete = 0;
        while (rank < 7)
        {
            percentComplete = requirements.RankPercent((Requirement.Rank)rank);
            if(percentComplete < 1)
            {
                break;
            }
            rank++;
        }

        if (rank == 6 && percentComplete >= 1)
        {
            //set to scout
        }
        else
        {
            levelPercentText.text = $"{(percentComplete * 100).ToString("F0")}%";
            if(rank == 0)
            {
                rankText.text = "No Rank";
                fillImage.fillAmount = percentComplete;
                nextRankText.text = "Scout";
            }
        }
    }
}
