using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Requirement
{
    public string id;
    public Rank rank;
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

    public enum Rank
    {
        S, //scout
        T, //tenderfoot
        SC, //second class
        F, //first class
        ST, //star
        L, //life
        E //eagle
    };

    public Requirement(string id, string rank, string requirementNumber, string descriptionText, string answerType, string answer, string hasImage, string imageURL, string hasVideo, string videoURL, string images)
    {
        this.id = id;
        this.rank = (Rank)System.Enum.Parse(typeof(Rank), rank);
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
