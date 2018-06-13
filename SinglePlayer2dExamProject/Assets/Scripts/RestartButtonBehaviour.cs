using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartButtonBehaviour : MonoBehaviour {

    public Button button;

	// Use this for initialization
	void Start () {
        button.onClick.AddListener(RestartScene);
	}

    private void RestartScene()
    {
        //a button named Button ui (the restart button) will have onclicklistener for loading same scene again. (this happens when robotboy dies, but he has more lifes)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
