using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;				//Library to change scene

public class ChangeScene : MonoBehaviour {

	public string LevelToLoad;

	void LoadLevel()
	{
		SceneManager.LoadScene(LevelToLoad);
	}
}
