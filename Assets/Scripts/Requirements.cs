using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using System.Linq;
using System.Runtime.Serialization.Json;

public class Requirements : MonoBehaviour
{
    public string pathToData = @"Resources/Requirements.tsv";
    //public TextAsset data;
    public List<Requirement> requirements = new List<Requirement>();

    public void ReadData(Requirement.Rank rank)
    {
        requirements.Clear();
        string relativePathToData;
        if(Application.isEditor)
        {
            relativePathToData = Path.Combine("Assets", pathToData);
        }
        else
        {
            relativePathToData = pathToData;
        }
        
        IEnumerable<string> lines = File.ReadLines(relativePathToData);
        List<string[]> arrays = new List<string[]>();
        foreach (string line in lines)
        {
            string[] array = line.Split('\t');
            arrays.Add(array);
            if (array[0] != "id" && array[1] == rank.ToString())
            {
                Requirement requirement = new Requirement(array[0], array[1], array[2], array[3], array[4], array[5], array[6], array[7], array[8], array[9], array[10]);
                requirements.Add(requirement);
                //Debug.LogError(requirement);
            }
        }
    }

    public float RankPercent(Requirement.Rank rank)
    {
        float percentCompleted = 0;
        int completedRequirements = 0;
        ReadData(rank);
        foreach(Requirement requirement in requirements)
        {
            if(PlayerPrefs.GetString(requirement.id, "no") == "yes")
            {
                completedRequirements++;
            }
        }
        percentCompleted = (float)completedRequirements / requirements.Count;
        return percentCompleted;
    }
}

/*

list[0][1][x]


 * */