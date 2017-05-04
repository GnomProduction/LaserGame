using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuManagerScript : MonoBehaviour
{
    private CanvasGroup FadeGroup;
    private float fadeInSpeed = 0.33f;

    private void Start()
    {
        FadeGroup = FindObjectOfType<CanvasGroup>();

        FadeGroup.alpha = 1;
    }

    private void Update()
    {
        FadeGroup.alpha = 1 - Time.timeSinceLevelLoad * fadeInSpeed;
    }

    public void ToGame(int levelIndex)
    {
        DataHelper.Instance.CurrentLevel = levelIndex;
        SceneManager.LoadScene("Game");
    }
}
