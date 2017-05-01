using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GoToMenuScript : MonoBehaviour {

	public void GoToMenu(){
		SceneManager.LoadScene ("MainMenuScene");
	}
}
