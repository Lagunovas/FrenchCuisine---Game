using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour {
    private Button BackMenu;
    private Button NextLevel;
    

    // Use this for initialization
    void Start () {
        GetComponent();
        BackMenu.onClick.AddListener(onBMClick);
        NextLevel.onClick.AddListener(onNLClick);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void GetComponent() {
        BackMenu = transform.Find("BackMenu").GetComponent<Button>();
        NextLevel = transform.Find("NextLevel").GetComponent<Button>();

    }
    void onBMClick() {
        SceneManager.LoadScene("Menu");

    }
    void onNLClick() {

        SceneManager.LoadScene(3);

    }
}
