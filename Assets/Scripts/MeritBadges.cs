using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


/// <summary>
/// This class will read Meritbadge.cs and store all the data
/// </summary>
public class MeritBadges : MonoBehaviour
{
    public string pathToData = @"Resources/MeritBadges.tsv";
    public List<MeritBadge> meritBadges = new List<MeritBadge>();
    public List<MeritBadge> meritBadgesEagle = new List<MeritBadge>();

    

    public void ReadData()
    {
        meritBadges.Clear();
        string relativePathToData;
        if (Application.isEditor)
        {
            relativePathToData = Path.Combine("Assets", pathToData);
        }
        else
        {
            relativePathToData = Application.persistentDataPath + "/" + pathToData;
        }

        IEnumerable<string> lines = File.ReadLines(relativePathToData);
        List<string[]> arrays = new List<string[]>();
        foreach (string line in lines)
        {
            string[] array = line.Split('\t');
            arrays.Add(array);
            if (array[0] == "name") continue;
            List<string> tips = new List<string>();

            for(int i = 7; i < array.Length; i++)
            {
                tips.Add(array[i]);
            }
            bool eagleRequired = array[1] == "yes" ? true : false;
            MeritBadge meritBadge = new MeritBadge(array[0], eagleRequired, array[2], array[3], array[4], array[5], array[6], tips);

            if (eagleRequired)
            {
                meritBadgesEagle.Add(meritBadge);
            }
            else
            {
                meritBadges.Add(meritBadge);
            }
            
        }
    }

}
