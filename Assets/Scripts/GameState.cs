using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour {

	private string currentSceneName;

	private static int score;
	private static int maxScore;

	private void Awake() {
		DontDestroyOnLoad(this);
	}

	private void FixedUpdate() {
		var sceneName = SceneManager.GetActiveScene().name;

		if (!sceneName.Equals("Menu")) {
			if (!sceneName.Equals(currentSceneName)) {
				currentSceneName = sceneName;
				ProgressToNextLevel();
			}
		}
	}

	private void OnGUI() {
		if (maxScore > 0) {
			GUI.Button(new Rect(50, 50, 200, 20), "Lettuce collected: " + score + " / " + maxScore);
		}
	}

	public static void IncrementScore() {
		score++;
	}

	private static void ProgressToNextLevel() {
		score = 0;
		var environment = GameObject.Find("Environment");
		if (environment) {
			var lo = environment.transform.Find("LettuceObjects");
			if (lo) {
				maxScore = lo.childCount;
			}
		}
	}

}