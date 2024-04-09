using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ScreenSizeDetector : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI resolutionText;

    // Start is called before the first frame update
    void Start()
    {
        resolutionText.text = $"{GetScreenWidth()} x {GetScreenHeight()}";
    }

    private int GetScreenWidth()
    {
        return Screen.width;
    }
    private int GetScreenHeight()
    {
        return Screen.height;
    }
}
