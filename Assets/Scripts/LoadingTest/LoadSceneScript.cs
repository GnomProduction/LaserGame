using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneScript : MonoBehaviour
{

    public void LoadSceneNum(int num)
    {
        if (num < 0 || num >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogWarning("Can't load scene num " + num + ", SceneManager only has "+ SceneManager.sceneCountInBuildSettings + " scenes in BuildSettings");
            return;
        }
        LoadingScreenScript.LoadScene(num);
    }
}
