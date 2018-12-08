using UnityEngine;

public class Restart : MonoBehaviour {

	public string levelNumber;

	private void Update() {
		if (Input.GetKeyDown(KeyCode.R)) {
			Application.LoadLevel(levelNumber);
		}
	}

}