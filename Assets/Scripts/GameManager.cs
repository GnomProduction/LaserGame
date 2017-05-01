using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    private Level CurrentLevel;
    private float StartingTime;

    public int Gold { get; set; }
    public GameObject turretContainer;
    public GameObject[] enemyContainer;
    public Text currentLevelIndex;
    public Text goldAmountText;
    public bool enemySpawned;

    void Start()
    {
        SceneManager.LoadScene(DataHelper.Instance.CurrentLevel.ToString(), LoadSceneMode.Additive);
        Instance = this;
        CurrentLevel = DataHelper.Instance.levels[DataHelper.Instance.CurrentLevel];
        Gold += CurrentLevel.StartingGold;

        //Game UI
        currentLevelIndex.text = CurrentLevel.levelName;

        UnlockTurrets();
        UpdateGoldText();
        StartingTime = Time.time;
    }

    private void Update()
    {
        float gameDuration = Time.time - StartingTime;
        for (int i = 0; i < CurrentLevel.objects.Count; i++)
        {
            if (CurrentLevel.objects[i].time < gameDuration)
            {
                Instantiate(enemyContainer[1], new Vector3(CurrentLevel.objects[i].positionX + 0.5f, 0f, CurrentLevel.objects[i].positionZ + 0.5f), Quaternion.identity);
                enemySpawned = true;
                CurrentLevel.objects.Remove(CurrentLevel.objects[i]);
            }
        }
    }

    private void UnlockTurrets()
    {
        int i = 0;
        foreach (Transform t in turretContainer.transform)
        {
            bool activeButton = ((CurrentLevel.UnlockedTowers) & (1 << i)) != 0;
            //Debug.Log (activeButton);
            t.GetComponent<Button>().interactable = activeButton;
            i++;
        }
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void UpdateGoldText()
    {
        goldAmountText.text = Gold.ToString();
    }
    /* private void AddEnemies()
     {
         foreach (enemySpawnInformation e in GameManager.Instance.CurrentLevel.objects)
         {
             Instantiate(enemyContainer[1], new Vector3(e.positionX + 0.5f, 0f, e.positionZ + 0.5f), Quaternion.identity);
             CurrentLevel.objects.Remove(e);
         }
     }*/
}
