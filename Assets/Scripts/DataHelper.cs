using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif

public class DataHelper : MonoBehaviour
{
    private CanvasGroup fadeGroup;
    private float LoadTime;
    private float MinimumLogoTime = 3.0f; //Minimum time of that scene
    public static DataHelper Instance { get; set; }
    public BitArray UnlockedLevel { get; set; }
    public int CurrentLevel { get; set; }
    public List<Level> levels { get; set; }
    public TextAsset LevelData;

    void Start()
    {
        if(FindObjectOfType<CanvasGroup>())
        {
            fadeGroup = FindObjectOfType<CanvasGroup>();
            fadeGroup.alpha = 1;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);

        //Load the previous scene
        Load();

        //Reading all levels
        ReadLevelData();

        //Loading all data, appreciating logo
        if (Time.time < MinimumLogoTime)
            LoadTime = MinimumLogoTime;
        else
            LoadTime = Time.time;
    }

    private void Update()
    {
        //fade-in
        if (Time.time < MinimumLogoTime)
            if(fadeGroup)
                fadeGroup.alpha = 1 - Time.time;
        if(Time.time > MinimumLogoTime && LoadTime!=0)
        {
            if (fadeGroup)
            {
                fadeGroup.alpha = Time.time - MinimumLogoTime;
                if (fadeGroup.alpha >= 1)
                {
                    //Load Scene
                    SceneManager.LoadScene("MainMenuScene");
                }
            }
        }
    }

    private void ReadLevelData()
    {
        levels = new List<Level>();
        string[] allLevels = LevelData.text.Split('%');
        foreach (string s in allLevels)
        {
            levels.Add(new Level(s));
        }
    }

    public void Save()
    {
        string saveString = "";
        for (int i = 0; i < UnlockedLevel.Count; i++)
        {
            saveString += UnlockedLevel.Get(i).ToString();
        }
        PlayerPrefs.SetString("saveString", saveString);
    }

    public void Load()
    {
        string loadString = PlayerPrefs.GetString("saveString");
        int i = 0;

        foreach (char c in loadString)
        {
            if (c == 0)
            {
                UnlockedLevel.Set(0, false);
            }
            else
            {
                UnlockedLevel.Set(i, true);
            }
            i++;
        }
    }
}
