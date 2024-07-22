using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// this script basically takes care of all the logic in the scene "1_CheckScreenResolution"
/// 
/// Written by Andy (rmfandyplayz)
/// </summary>
public class CheckScreenRes : MonoBehaviour
{
    [SerializeField] Image transitionPanel;
    [SerializeField] GameObject phonePanel;
    [SerializeField] GameObject tabletPanel;


    private void Start()
    {
        //set panel activation based on screen ratio
        if(Screen.width > Screen.height) //tablet
        {
            tabletPanel.SetActive(true);
        }
        else //phone
        {
            phonePanel.SetActive(true);
        }


        StartCoroutine(FadeInPanel());
    }

    //alpha goes from 0 to 1
    IEnumerator FadeOutPanel()
    {
        transitionPanel.gameObject.SetActive(true);
        Color tempColor = transitionPanel.color;
        tempColor.a = 0;

        transitionPanel.color = tempColor;

        for (int i = 0; i < 100; i++)
        {
            tempColor.a += 0.01f;
            transitionPanel.color = tempColor;
            yield return new WaitForSeconds(0.01f);
        }
        tempColor.a = 1;
        transitionPanel.color = tempColor;
    }

    //alpha goes from 1 to 0
    IEnumerator FadeInPanel()
    {
        Color tempColor = transitionPanel.color;
        tempColor.a = 1;

        transitionPanel.color = tempColor;

        for (int i = 0; i < 100; i++)
        {
            tempColor.a -= 0.01f;
            transitionPanel.color = tempColor;
            yield return new WaitForSeconds(0.01f);
        }
        tempColor.a = 0;
        transitionPanel.color = tempColor;
        transitionPanel.gameObject.SetActive(false);
    }



}
