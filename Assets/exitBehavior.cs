using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class exitBehavior : MonoBehaviour {
    public string nextLevel = "Menu";
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(nextLevel);

        }
    }
}
