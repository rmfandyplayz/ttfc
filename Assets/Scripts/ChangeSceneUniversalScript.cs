using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneUniversalScript : MonoBehaviour
{
    public string targetScene;
    public void SwitchSchene()
    {
        SceneManager.LoadScene(targetScene);
    }
}
