using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuManagerScript : MonoBehaviour
{
    private CanvasGroup FadeGroup;
    private float fadeInSpeed = 0.33f;
    private float time = 0f;

    private void Start()
    {
        FadeGroup = FindObjectOfType<CanvasGroup>();

        FadeGroup.alpha = 1;
    }

    private void Update()
    {
        time += Time.timeSinceLevelLoad;
        FadeGroup.alpha = 0;//1 - time * fadeInSpeed;
        //Debug.Log(time);
    }

    public void ToGame(int levelIndex)
    {
        DataHelper.Instance.CurrentLevel = levelIndex;
        SceneManager.LoadScene("Game");
    }
}
