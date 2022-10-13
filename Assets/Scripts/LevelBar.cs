using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq.Expressions;

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
        CheckForBarAnimation();
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


    /// <summary>
    /// Checks whether or not the we have increased in % or decreased. Then calls SetLevelBar() with the instructions.
    /// </summary>
    void CheckForBarAnimation()
    {
        Utility.RankPercent currentRankPercent = Utility.GetCurrentRankPercent(requirements); //New percent
        Utility.RankPercent oldRankPercent = Utility.GetRankPercent(); //Old percent

        if(currentRankPercent.rank < oldRankPercent.rank || currentRankPercent.percentage < oldRankPercent.percentage) //Decrease in rank
        {
            SetLevelBar(currentRankPercent.rank, currentRankPercent.percentage);
        }
        else if(currentRankPercent.rank > oldRankPercent.rank || currentRankPercent.rank == oldRankPercent.rank && currentRankPercent.percentage > oldRankPercent.percentage) //Increase in rank or percentage within the same rank.
        {
            StartCoroutine(AnimateLevelBar(oldRankPercent, currentRankPercent));
        }
        else //No Change
        {
            SetLevelBar(oldRankPercent.rank, oldRankPercent.percentage);
        }
        
        

    }



    /// <summary>
    /// Displays the level bar according to the percent complete.
    /// </summary>
    /// <param name="rank"></param>
    /// <param name="percentComplete"></param>
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
    }

    public AnimationCurve easingCurve;
    public float speed = 1;
    IEnumerator AnimateLevelBar(Utility.RankPercent oldRankPercent, Utility.RankPercent currentRankPercent)
    {
        int rank = oldRankPercent.rank;
        float value = oldRankPercent.percentage;
        float maxValue = currentRankPercent.percentage;
        if (oldRankPercent.rank < currentRankPercent.rank)
        {
            maxValue = 1;
        }
        while(value < maxValue)
        {
            float fillAmount = speed * Utility.TweenObject(easingCurve, maxValue, value) * Time.deltaTime;
            if(value + fillAmount < maxValue)
            {
                value += fillAmount;
            }
            else
            {
                value = maxValue;
            }
            SetLevelBar(rank, value);

            if(value == 1)
            {
                rank++;
                StartCoroutine(AnimateRankUp(rank));
                while (rankAnimation)
                {
                    yield return null;
                }
                value = 0;
                maxValue = currentRankPercent.percentage;
                if (rank < currentRankPercent.rank)
                {
                    maxValue = 1;
                }
            }

            yield return null;
        }

        Utility.SetRankPercent(rank, currentRankPercent.percentage);
    }

    bool rankAnimation = false;
    IEnumerator AnimateRankUp(int rank)
    {
        SetLevelBar(rank, 0);
        yield return new WaitForSeconds(2);
        rankAnimation = false;
    }
}
