using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;				//Library to change scene

public class ChangeScene : MonoBehaviour {



	public void LoadLevel(string LevelToLoad)
	{
		SceneManager.LoadScene(LevelToLoad);
	}
}
