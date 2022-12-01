
using UnityEditor.PackageManager;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;

public static class Utility
{
    static float ipadRatioLandscape = 4 / 3f;
    static float ipadRatioPortrait = .75f;
    public enum ScreenRatios
    {
        ipadLandScp, 
        ipadPort, 
        iphoneLandScp, 
        iphonePort
    }

    public static ScreenRatios GetScreenRatio()
    {
        float screenRatio = Camera.main.aspect;
        if(screenRatio + 0.001f > ipadRatioLandscape && screenRatio - 0.001f < ipadRatioLandscape)
        {
            return ScreenRatios.ipadLandScp;
        }
        else if (screenRatio + 0.001f > ipadRatioLandscape && screenRatio - 0.001f < ipadRatioLandscape)
        {
            return ScreenRatios.ipadPort;
        }
        else return ScreenRatios.iphonePort;
    }

    public struct RankPercent
    {//struct is like a class but a container
        public int rank;
        public float percentage;
    }

    public static RankPercent GetCurrentRankPercent(Requirements requirements)
    {
        int rank = 0;
        float percentComplete = 0;
        while (rank < 7)
        {
            percentComplete = requirements.RankPercent((Requirement.Rank)rank);
            if (percentComplete < 1)
            {
                break;
            }
            rank++;
        }
        RankPercent rankPercent = new RankPercent();
        rankPercent.rank = rank;
        rankPercent.percentage = percentComplete;
        return rankPercent;
    }

    /// <summary>
    /// Gets the rank percent from what has been stored.
    /// </summary>
    /// <returns></returns>
    public static RankPercent GetRankPercent()
    {
        int lastRank = PlayerPrefs.GetInt("rank", 0);
        float oldPercent = PlayerPrefs.GetFloat("percentComplete", 0);
        RankPercent rankPercent = new RankPercent();
        rankPercent.percentage = oldPercent;
        rankPercent.rank = lastRank;
        return rankPercent;
    }

    public static void SetRankPercent(int rank, float percentComplete, bool reset)
    {
        int lastRank = PlayerPrefs.GetInt("rank", 0);
        float oldPercent = PlayerPrefs.GetFloat("percentComplete", 0);

        if (reset || rank > lastRank || rank == lastRank && percentComplete > oldPercent)
        {
            PlayerPrefs.SetFloat("percentComplete", percentComplete);
            PlayerPrefs.SetInt("rank", rank);
        }
    }

    /// <summary>
    /// Returns the velocity of an object given the easing, value which can be position, rotation, size, length, etc. and the maximum amount of that value.
    /// </summary>
    /// <param name="animation"></param>
    /// <param name="maxValue"></param>
    /// <param name="value"></param>
    public static float TweenObject(AnimationCurve easing, float maxValue, float value)
    {
        //Calculate the velocity
        float x = Mathf.Abs((-value + maxValue) / maxValue); //When moving from left to right, x determines how far you have moved. (Time)
        float speed = 0;
        speed = easing.Evaluate(x);
        return speed;
    }

}