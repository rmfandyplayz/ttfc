using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class contains all the data that will be read by MeritBadges.cs
/// </summary>
public class MeritBadge
{
    public string name;
    public bool eagleRequired;
    public string imageName;
    public string pamphletURL;
    public string workbookURL;
    public List<string> tips;
    public bool completed;

    public MeritBadge(string name, bool eagleRequired, string imageName, string pamphletURL, string workbookURL, List<string> tips)
    {
        this.name = name;
        this.eagleRequired = eagleRequired;
        this.imageName = imageName;
        this.pamphletURL = pamphletURL;
        this.workbookURL = workbookURL;
        this.tips = tips;

        completed = PlayerPrefs.GetInt(name, 0) == 1;//0 for false, 1 for true
    }

    public void SetCompleted(bool completed)
    {
        this.completed = completed;
        int value = completed ? 1 : 0;
        PlayerPrefs.SetInt(name, value);
    }
}
