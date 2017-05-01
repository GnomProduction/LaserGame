using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuManagerScript : MonoBehaviour {

	void Start () {
	
	}
		
	void UnlockedLevels(){
	
	}

	public void ToGame(int levelIndex){
		DataHelper.Instance.CurrentLevel = levelIndex;
		SceneManager.LoadScene ("Game");
	}
}
