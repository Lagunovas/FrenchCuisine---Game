using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour {
	private Button BackMenu;
	private Button NextLevel;

	private void Start() {
		BackMenu = transform.Find("BackMenu").GetComponent<Button>();
		NextLevel = transform.Find("NextLevel").GetComponent<Button>();
		BackMenu.onClick.AddListener(OnBMClick);
		NextLevel.onClick.AddListener(OnNLClick);

	}

	private void OnBMClick() {
		SceneManager.LoadScene("Menu");
	}

	private void OnNLClick() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

}