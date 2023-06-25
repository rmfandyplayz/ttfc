using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu(fileName = "ImageHandler", menuName = "Image Database")]
public class ImageHandler : ScriptableObject
{
    public Sprite bowline, squareknot;
    public List<string> imageIDs;
    public List<Sprite> images;
    
    public Sprite GetImage(string imageID)
    {
        for (int i = 0; i < imageIDs.Count; i++)
        {
            if (imageIDs[i] == imageID)
            {
                return images[i];
            }
        }
        return GetSprite(imageID);
    }

    private Sprite GetSprite(string imageID)
    {
        Sprite sprite = null;
        string imageFilePath = Utility.GetImageFilePath(imageID);
        if (System.IO.File.Exists(imageFilePath))
        {
            byte[] FileData = System.IO.File.ReadAllBytes(imageFilePath);
            Texture2D tex2d = new Texture2D(2, 2);
            tex2d.LoadImage(FileData);


            int width = tex2d.width;
            int height = tex2d.height;
            int maxWidth = 100;
            if (width > maxWidth)
            {
                height = maxWidth * height / width;

                width = maxWidth;
            }


            sprite = Sprite.Create(tex2d, new Rect(0.0f, 0.0f, tex2d.width, tex2d.height), new Vector2(.5f, .5f), 1f);
            sprite.name = imageID;
        }
        return sprite;
    }

    /*
    public void AddImage(string imageID, string imageFilePath)
    {
        Texture2D myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
        byte[] bytes = myTexture.EncodeToPNG();

        //string dataPath = "Resources/Images/" + imageName + ".png";
        //if (Application.isEditor)
        //{
        //    dataPath = Path.Combine("Assets", dataPath);
        //}
        //else
        //{
        //    dataPath = Application.persistentDataPath + "/" + dataPath;
        //}
        //dataPath = Utility.GetFilePath(dataPath);
        string dataPath = Utility.GetImageFilePath(image);
        System.IO.File.WriteAllBytes(dataPath, bytes);
        imageDownloadedSuccessful = true;
    }
    */
}
