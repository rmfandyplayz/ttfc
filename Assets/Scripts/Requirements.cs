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
    public TextAsset data;
    public List<Requirement> requirements = new List<Requirement>();


    private void Start()
    {
        ReadData();
        FindObjectOfType<RequirementsHandler>().SetUpRequirementButton(this);
    }



    private void ReadData()
    {
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
            if (array[0] != "id")
            {
                Requirement requirement = new Requirement(array[0], array[1], array[2], array[3], array[4], array[5], array[6], array[7], array[8], array[9], array[10]);
                requirements.Add(requirement);
                //Debug.LogError(requirement);
            }
        }
    }
}

/*

list[0][1][x]


 * */

[System.Serializable]
public class Requirement
{
    public string id;
    public string rank;
    public string requirementNumber;
    public string descriptionText;
    public List<string> images = new List<string>();
    public enum AnswerType
    {
        text,
        image,
        video,
        none
    }
    public AnswerType[] answerType;
    public string answer;
    public bool hasImage;
    public string imageURL;
    public bool hasVideo;
    public string videoURL;

    public Requirement(string id, string rank, string requirementNumber, string descriptionText, string answerType, string answer, string hasImage, string imageURL, string hasVideo, string videoURL, string images)
    {
        this.id = id;
        this.rank = rank;
        this.requirementNumber = requirementNumber;
        this.descriptionText = descriptionText;
        this.answerType = GetAnswerType(answerType);
        this.answer = answer;
        this.hasImage = hasImage == "yes" ? true : false;
        this.imageURL = imageURL;
        this.hasVideo = hasVideo == "yes" ? true : false;
        this.videoURL = videoURL;

        if (this.hasImage)
        {
            string[] imageArray = images.Split(' ');
            this.images = new List<string>(imageArray);
        }
    }

    private AnswerType[] GetAnswerType(string answerType)
    {
        List<AnswerType> answerTypes = new List<AnswerType>();

        bool none = false;
        if (answerType.Contains(AnswerType.text.ToString()))
        {
            answerTypes.Add(AnswerType.text);
            none = false;
        }
        if (answerType.Contains(AnswerType.video.ToString()))
        {
            answerTypes.Add(AnswerType.video);
            none = false;
        }
        if (answerType.Contains(AnswerType.image.ToString()))
        {
            answerTypes.Add(AnswerType.image);
            none = false;
        }
        if (none) answerTypes.Add(AnswerType.none);
        return answerTypes.ToArray();
    }

    public override string ToString()
    {
        return $"{id}, {rank}, {requirementNumber}, {descriptionText}, {answerType}, {answer}, {hasImage}, {imageURL}, {hasVideo}, {videoURL}";
    }

}