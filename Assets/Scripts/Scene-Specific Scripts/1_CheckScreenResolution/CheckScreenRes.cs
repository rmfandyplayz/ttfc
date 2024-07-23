using System.Collections;
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
            Debug.Log("Tablet detected");
            tabletPanel.SetActive(true);
        }
        else //phone
        {
            Debug.Log("Phone detected");
            phonePanel.SetActive(true);
        }


        StartCoroutine(FadeOutPanel());
    }

    //follows the same pattern as global settings - 0 for phone, 1 for tablet
    public void SelectOption(int deviceMode)
    {
        if(deviceMode == 0 || deviceMode == 1)
        {
            _GLOBALSETTINGS.deviceMode = deviceMode;
            _GLOBALSETTINGS.SaveSettings();
            Debug.Log($"Display mode set to {(deviceMode == 0 ? "Phone" : "Tablet")}");
            StartCoroutine(FadeInPanel());
        }
        else
        {
            Debug.LogError("[CheckScreenRes.cs] incorrect input!");
        }
    }

    //alpha goes from 0 to 1 (transparent to opaque)
    IEnumerator FadeInPanel()
    {
        transitionPanel.gameObject.SetActive(true);
        Color tempColor = transitionPanel.color;
        tempColor.a = 0;
        transitionPanel.color = tempColor;

        for (int i = 0; i < 50; i++)
        {
            tempColor.a += 0.02f;
            transitionPanel.color = tempColor;
            yield return new WaitForSeconds(0.01f);
        }
        tempColor.a = 1;
        transitionPanel.color = tempColor;
    }

    //alpha goes from 1 to 0 (opaque to transparent)
    IEnumerator FadeOutPanel()
    {
        Color tempColor = transitionPanel.color;
        tempColor.a = 1;

        transitionPanel.color = tempColor;

        for (int i = 0; i < 50; i++)
        {
            tempColor.a -= 0.02f;
            transitionPanel.color = tempColor;
            yield return new WaitForSeconds(0.01f);
        }
        tempColor.a = 0;
        transitionPanel.color = tempColor;
        transitionPanel.gameObject.SetActive(false);
    }



}
