using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using System;

public class DataHelper : MonoBehaviour 
{
	public static DataHelper Instance{ get; set; }
	public BitArray UnlockedLevel{ get; set; }
	public int CurrentLevel{ get; set; }
	public List<Level> levels { get; set; }
    public TextAsset LevelData;

	void Start () 
	{
		Instance = this;

		DontDestroyOnLoad (gameObject);

		//Load the previous scene
		Load();

        //Reading all levels
        ReadLevelData();

		//Load Scene
		EditorSceneManager.LoadScene("MainMenuScene");
	}

    private void ReadLevelData()
    {
        levels = new List<Level>();
        string[] allLevels = LevelData.text.Split('%');
        foreach(string s in allLevels)
        {
            levels.Add(new Level(s));
        }
    }

    public void Save()
	{
		string saveString = "";
		for (int i = 0; i < UnlockedLevel.Count; i++) 
		{
			saveString += UnlockedLevel.Get (i).ToString();
		}
		PlayerPrefs.SetString ("saveString", saveString);
	}

	public void Load()
	{
		string loadString = PlayerPrefs.GetString ("saveString");
		int i = 0;

		foreach (char c in loadString)
		{
			if (c == 0) {
				UnlockedLevel.Set (0, false);
			} else {
				UnlockedLevel.Set (i, true);
			}
			i++;	
		}
	}
}
