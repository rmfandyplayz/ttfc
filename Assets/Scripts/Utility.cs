
using UnityEngine;

public static class Utility
{
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

    public static RankPercent GetRankPercent()
    {
        int lastRank = PlayerPrefs.GetInt("rank", 0);
        float oldPercent = PlayerPrefs.GetFloat("percentComplete", 0);
        RankPercent rankPercent = new RankPercent();
        rankPercent.percentage = oldPercent;
        return rankPercent;
    }

    public static void SetRankPercent(int rank, float percentComplete)
    {
        int lastRank = PlayerPrefs.GetInt("rank", 0);
        float oldPercent = PlayerPrefs.GetFloat("percentComplete", 0);

        if (rank >= lastRank && percentComplete > oldPercent)
        {
            PlayerPrefs.SetFloat("percentComplete", percentComplete);
            PlayerPrefs.SetInt("rank", rank);
        }
    }
}