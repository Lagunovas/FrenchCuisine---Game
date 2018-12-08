using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

	public void LoadLevel(string LevelToLoad) {
		SceneManager.LoadScene(LevelToLoad);
	}

}