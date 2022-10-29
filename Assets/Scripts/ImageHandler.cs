using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu(fileName = "ImageHandler", menuName = "Image Database")]
public class ImageHandler : ScriptableObject
{
    public Sprite bowline, squareknot;
    public string[] imageIDs;
    public Sprite[] images;

    public Sprite GetImage(string imageID)
    {
        for (int i = 0; i < imageIDs.Length; i++)
        {
            if (imageIDs[i] == imageID)
            {
                return images[i];
            }
        }
        return null;
    }
}
