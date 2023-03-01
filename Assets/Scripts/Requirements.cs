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
    public List<Requirement> requirementsList = new List<Requirement>();

    /// <summary>
    /// Reads the data in the Requirements.tsv file, and places them in the "requirements" list, line by line.
    /// </summary>
    /// <param name="rank"></param>
    public void ReadData(Requirement.Rank rank)
    {
        requirementsList.Clear();
        string relativePathToData;
        if(Application.isEditor)
        {
            relativePathToData = Path.Combine("Assets", pathToData);
        }
        else
        {
            relativePathToData = Application.persistentDataPath + "/" + pathToData;
        }
        
        IEnumerable<string> lines = File.ReadLines(relativePathToData); 
        List<string[]> arrays = new List<string[]>();
        //This foreach loop makes it so each line of the TSV file goes into the list "requirements"
        foreach (string line in lines)
        {
            string[] array = line.Split('\t');
            arrays.Add(array);
            if (array[0] != "id" && array[1] == rank.ToString())
            {
                Requirement requirement = new Requirement(array[0], array[1], array[2], array[3], array[4], array[5], array[6], array[7], array[8], array[9], array[10]);
                requirementsList.Add(requirement);
            }
        }
    }

    public float RankPercent(Requirement.Rank rank)
    {
        float percentCompleted = 0;
        int completedRequirements = 0;
        ReadData(rank);
        foreach(Requirement requirement in requirementsList)
        {
            if(PlayerPrefs.GetString(requirement.id, "no") == "yes")
            {
                completedRequirements++;
            }
        }
        percentCompleted = (float)completedRequirements / requirementsList.Count;
        return percentCompleted;
    }
}