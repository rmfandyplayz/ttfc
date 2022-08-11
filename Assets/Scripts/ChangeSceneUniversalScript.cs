using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneUniversalScript : MonoBehaviour
{
    public void SwitchSchene(string targetScene)
    {
        SceneManager.LoadScene(targetScene);
    }
}
