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
    public string[] rankNames = {"No rank", "Scout", "Tenderfoot", "Second Class", "First Class", "Star", "Life", "Eagle"};

    public float barGrowSpeed = 50; //how fast the bar fills up

    private void Start()
    {
        Utility.RankPercent currentRankPercent = Utility.GetCurrentRankPercent(requirements);
        Utility.RankPercent rankPercent = Utility.GetRankPercent();
        SetLevelBar(currentRankPercent.rank, rankPercent.percentage);
        /*
         * Check if we increased in percentage
         * If decrease in rank or percentage, call SetLevelBar()
         * If increase in percentage
         * - If rank increased,
         * -- Increase percent at speed "barGrowthRate" until rank up
         * -- Trigger rank up animation
         *
         */
    }

    void SetLevelBar(int rank, float percentComplete)
    {
        if(rank == 7)
        {
            fillImage.fillAmount = 1;
        }

        if (rank == 6 && percentComplete >= 1)
        {
            //set to scout
        }
        else
        {
            levelPercentText.text = $"{(percentComplete * 100).ToString("F0")}%";
            fillImage.fillAmount = percentComplete;
        }

        if(rank == 0)
        {
            rankText.text = rankNames[rank];
            nextRankText.text = rankNames[rank + 1];
            nextRankImage.sprite = rankImages[rank + 1];
        }
        else if(rank < 7)
        {
            rankText.text = rankNames[rank];
            nextRankText.text = rankNames[rank + 1];
            rankImage.sprite = rankImages[rank];
            nextRankImage.sprite = rankImages[rank + 1];
        }
        else
        {
            rankImage.sprite = rankImages[rank];
            rankText.text = rankNames[rank];
        }

        Utility.SetRankPercent(rank, percentComplete);
    }
}
