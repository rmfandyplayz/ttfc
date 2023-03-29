using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.ComponentModel;

public class MainScreen
{
    public string pathToData = @"Resources/MainScreenData.tsv";
    public string backgroundURL;
    public string backgroundImageID;

    List<MainScreenButton> mainScreenButtons = new List<MainScreenButton>();
    public class MainScreenButton
    {
        public string spriteID;
        public string spriteURL;
        public Vector2 buttonPos;
        public Vector2 buttonSize;
    }
    public MainScreen(Utility.ScreenRatios screenRatios)
    {
        ReadData(screenRatios);
    }

    void ReadData(Utility.ScreenRatios screenRatios)
    {
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
        int startIndex = screenRatios == Utility.ScreenRatios.ipadLandScp ? 0 : 8;
        int endIndex = screenRatios == Utility.ScreenRatios.iphoneLandScp ? 7 : 15;

        int index = 0;
        foreach(string line in lines)
        {
            if(index >= startIndex && index <= endIndex)
            {
                string[] array = line.Split('\t');
                if(startIndex == index) //This means we are on the background image
                {
                    backgroundImageID = array[0];
                    backgroundURL = array[1];
                }
                else //This means we are on the buttons
                {
                    MainScreenButton button = new MainScreenButton();
                    button.spriteID = array[0];
                    button.spriteURL = array[1];
                    button.buttonPos = new Vector2(float.Parse(array[2]), float.Parse(array[3]));
                    button.buttonSize = new Vector2(float.Parse(array[4]), float.Parse(array[5]));
                }
            }
            index++;
        }
    }
}
