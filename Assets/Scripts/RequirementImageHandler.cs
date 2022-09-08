using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu(fileName = "ImageHandler", menuName = "Image Database")]
public class RequirementImageHandler : ScriptableObject
{
    public Sprite bowline, squareknot;

    public Sprite GetImage(string imageID)
    {
        if (imageID == "Square_knot")
        {
            return squareknot;
        }
        else if (imageID == "bowline")
        {
            return bowline;
        }
        else
        {
            return null;
        }
    }
}
